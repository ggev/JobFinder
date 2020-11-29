using JobFinder.Application.Services.Categories;
using JobFinder.Application.Services.Companies;
using JobFinder.Application.Services.Files;
using Microsoft.Extensions.DependencyInjection;
using System;
using JobFinder.Application.Services.Jobs;

namespace JobFinder.Application
{
    public static class DependencyRegistrar
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IFilesService, FilesService>()
                .AddScoped(x => new Lazy<IFilesService>(x.GetRequiredService<IFilesService>));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IJobService, JobService>();
        }
    }
}