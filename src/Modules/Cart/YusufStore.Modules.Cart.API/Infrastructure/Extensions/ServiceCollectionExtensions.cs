using System.Reflection;
namespace YusufStore.Modules.Cart.API.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCarterWithCustomCatalog(this IServiceCollection services)
        {           
            services.AddCarter(new DependencyContextAssemblyCatalogCustom());
            return services;
        }

       
        public static IServiceCollection AddCustomMediatR(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(Program).Assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });
            return services;
        }
    }

    public class DependencyContextAssemblyCatalogCustom : DependencyContextAssemblyCatalog
    {
        public override IReadOnlyCollection<Assembly> GetAssemblies()
        {           
            return new List<Assembly> { Assembly.GetExecutingAssembly() };
        }
    }    
}