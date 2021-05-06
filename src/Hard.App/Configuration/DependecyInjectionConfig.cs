using Hard.App.Extensions;
using Hard.Business.Interfaces;
using Hard.Business.Notifications;
using Hard.Business.Services;
using Hard.Data.Context;
using Hard.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hard.App.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<HardDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();            
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IProductService, ProductService>();            
            services.AddSingleton<IValidationAttributeAdapterProvider, CurrencyAttributeAdapterProvider>();            

            return services;
        }
    }
}
