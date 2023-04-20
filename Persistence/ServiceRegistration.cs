using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;


namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistence(this IServiceCollection services)
        {
            #region Repositories
            services.AddTransient<CatalogContext>();
            services.AddTransient<ICatalogContext, CatalogContext>();
            //services.AddTransient(typeof(IGenericRepositoryAsync<,>), typeof(GenericRepositoryAsync<,>));
            //services.AddTransient<IProductsRepositoryAsync, ProductRepositoryAsync>();
            #endregion

        }
    }
}
