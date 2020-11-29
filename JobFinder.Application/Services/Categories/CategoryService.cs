using JobFinder.Application.Dtos.Categories;
using JobFinder.Domain.Entities;
using JobFinder.Domain.Exceptions;
using JobFinder.Domain.Extensions;
using JobFinder.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Application.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository _repo;

        public CategoryService(IRepository repo)
        {
            _repo = repo;
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
            var category = await _repo.FilterAsNoTracking<Category>(x => x.Id == id).Select(x => new CategoryDetailsModel
            {
                Id = x.Id,
                Name = x.Name,
                JobsCount = x.JobCategories.Count
            }).FirstOrDefaultAsync();
            Guard.NotNull(category, nameof(category));
            return category;
        }

        public async Task<IEnumerable<CategoryListModel>> GetList()
        {
            return await _repo.GetAllAsNoTracking<Category>().Select(x => new CategoryListModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
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