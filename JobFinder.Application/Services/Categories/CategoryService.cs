using System;
using JobFinder.Application.Dtos.Categories;
using JobFinder.Domain.Entities;
using JobFinder.Domain.Exceptions;
using JobFinder.Domain.Extensions;
using JobFinder.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace JobFinder.Application.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository _repo;
        private readonly IMemoryCache _memoryCache;

        public CategoryService(IRepository repo,
            IMemoryCache memoryCache)
        {
            _repo = repo;
            _memoryCache = memoryCache;
        }

        public async Task<int> Add(AddCategoryModel model)
        {
            await ValidateCategoryName(model.Name);
            var category = await _repo.CreateAsync(new Category { Name = model.Name });
            await _repo.SaveChanges();
            return category.Id;
        }

        public async Task<bool> Edit(EditCategoryModel model)
        {
            var category = await _repo.GetByIdAsync<Category>(model.Id);
            category.CheckIfExist();

            if (!string.IsNullOrEmpty(model.Name))
            {
                await ValidateCategoryName(model.Name);
                category.Name = model.Name;
            }

            await _repo.SaveChanges();
            return true;
        }

        public async Task<CategoryDetailsModel> Get(int id)
        {
            var alreadyExist = _memoryCache.TryGetValue($"category{id}", out CategoryDetailsModel category);
            if (!alreadyExist)
            {
                category = await _repo.FilterAsNoTracking<Category>(x => x.Id == id).Select(x =>
                new CategoryDetailsModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    JobsCount = x.JobCategories.Count
                }).FirstOrDefaultAsync();
                Guard.NotNull(category, nameof(category));
                _memoryCache.Set($"category{id}", category, DateTimeOffset.UtcNow.AddMinutes(1));
            }

            return category;
        }

        public async Task<IEnumerable<CategoryListModel>> GetList()
        {
            var alreadyExist = _memoryCache.TryGetValue("categoryList", out List<CategoryListModel> categoryList);
            if (!alreadyExist)
            {
                categoryList = await _repo.GetAllAsNoTracking<Category>().Select(x => new CategoryListModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();
                _memoryCache.Set("categoryList", categoryList, DateTimeOffset.UtcNow.AddMinutes(1));
            }

            return categoryList;
        }

        public async Task<bool> Delete(int id)
        {
            await _repo.Remove<Category>(id);
            await _repo.SaveChanges();
            return true;
        }

        private async Task ValidateCategoryName(string name)
        {
            if (await _repo.FilterAsNoTracking<Category>(x => string.Equals(x.Name.ToUpper(), name.ToUpper())).AnyAsync())
                throw new SmartException("Category with the same name already exists");
        }
    }
}