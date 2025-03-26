using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindSystem.BLL;
using WestWindSystem.DAL;

namespace WestWindSystem
{
    /// <summary>
    /// Public so that it can be accessed from our other applications.
    /// 
    /// This class is going to hold all of our extension methods.
    /// 
    /// It will also be where we're adding them to IServiceCollection.
    /// 
    /// Any services added to this class can be used anywhere outside of our System.
    /// </summary>
    public static class WestWindExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services">The collection to register to.</param>
        /// <param name="options">Our DB connection string.</param>
        public static void WestWindExtensionsServices(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            //Adds our Database context to our service list with the connection string.
            services.AddDbContext<WestWindContext>(options);

            //Add every service we will be using with .AddTransient<T>
            #region Transient Services

            services.AddTransient<BuildVersionServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();

                return new BuildVersionServices(context);
            });

            services.AddTransient<RegionServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();

                return new RegionServices(context);
            });

            services.AddTransient<ShipmentServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();

                return new ShipmentServices(context);
            });

            services.AddTransient<ShipperServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();

                return new ShipperServices(context);
            });

            services.AddTransient<CategoryServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();

                return new CategoryServices(context);
            });

            services.AddTransient<ProductServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();

                return new ProductServices(context);
            });

            services.AddTransient<SupplierServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();

                return new SupplierServices(context);
            });
            #endregion
        }
    }
}
