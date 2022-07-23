using CarForum.Api.Application.Interfaces.Repositories;
using CarForum.Infrastructure.Persistence.Context;
using CarForum.Infrastructure.Persistence.Repositories;
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
                conf.UseSqlServer(conStr,opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            });
            //var seedData = new SeedData();
            //seedData.SeedAsync(configuration).GetAwaiter().GetResult();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
            services.AddScoped<IEntryCommentRepository,EntryCommentRepository>();

            
            return services;
        }
     }
    
}
