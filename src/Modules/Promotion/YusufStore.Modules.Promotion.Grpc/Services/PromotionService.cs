using Promotion.Grpc.Data;
using Promotion.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Promotion.Grpc.Services;

public class PromotionService
    (PromotionContext dbContext, ILogger<PromotionService> logger)
    : PromotionProtoService.PromotionProtoServiceBase
{
    public override async Task<CouponModel> GetPromotion(GetPromotionRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (coupon is null)
            coupon = new Coupon { ProductName = "No Promotion", Amount = 0, Description = "No Promotion Desc" };

        logger.LogInformation("Promotion is retrieved for ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> CreatePromotion(CreatePromotionRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Promotion is successfully created. ProductName : {ProductName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }


    public override async Task<CouponModel> UpdatePromotion(UpdatePromotionRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Promotion is successfully updated. ProductName : {ProductName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeletePromotionResponse> DeletePromotion(DeletePromotionRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Promotion with ProductId={request.Id} is not found."));

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Promotion is successfully deleted. ProductId : {ProductId}", request.Id);

        return new DeletePromotionResponse { Success = true };
    }

    public override async Task<PromotionListResponse> GetAllPromotions(Empty request, ServerCallContext context)
    {
        var coupons = await dbContext.Coupons.ToListAsync();       
        var promotionList = coupons.Select(coupon => coupon.Adapt<CouponModel>()).ToList();
        var response = new PromotionListResponse();
        response.Promotions.AddRange(promotionList);
        logger.LogInformation("Successfully retrieved all promotions.");

        return response;
    }
}
