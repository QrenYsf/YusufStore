using Microsoft.EntityFrameworkCore;
namespace Promotion.Grpc.Data;
public static class Extensions
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<PromotionContext>();
        dbContext.Database.MigrateAsync();

        return app;
    }
}
