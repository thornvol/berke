using BerkeGaming.Application.Common.Interfaces;
using BerkeGaming.Infrastructure.Identity;
using BerkeGaming.Infrastructure.Persistence;
using BerkeGaming.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BerkeGaming.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            // handle in memory db
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            // Add Application Db Context to service container
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            // Add repository to service container
            services.AddScoped<IApplicationDbRepository, ApplicationDbRepository>();

            // Add UoW
            services.AddScoped<IUnityOfWork, UnitOfWork>();

            services.AddScoped<IDomainEventService, DomainEventService>();

            // Not using Asp.Net Identity/Identity serverS
            //services
            //    .AddDefaultIdentity<ApplicationUser>()
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddTransient<IDateTime, DateTimeService>();
            //services.AddTransient<IIdentityService, IdentityService>();

            // Add authentication
            services.AddAuthentication();

            // Add Authorization policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });

            return services;
        }
    }
}