using JobFinder.Domain.Interfaces;
using JobFinder.Infrastructure.Constants;
using JobFinder.Infrastructure.Data.DbContexts;
using JobFinder.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace JobFinder.Infrastructure
{
    public static class DependencyRegistrar
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var conString = configuration.GetConnectionString("Default");
            services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(conString));
            ConstValues.ConnectionString = conString;

            #region Context
            services.AddScoped<IDbContext, SqlDbContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            #region Repository
            services.AddScoped<IRepository, EntityRepository>();
            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobFinder API", Version = "v1" });
            });
        }
    }
}