using CarForum.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Infrastructure.Persistence.Extension
{
    public static class Registration
    { 
       
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarForumContext>(conf =>
            {
                var conStr = configuration.GetConnectionString("CarForumConnectionString");
                conf.UseSqlServer("conStr",opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            });
            //var seedData = new SeedData();
            //seedData.SeedAsync(configuration).GetAwaiter().GetResult();

            return services;
        }
     }
    
}
