using JobFinder.Application.Dtos.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobFinder.Application.Services.Categories
{
    public interface ICategoryService
    {
        Task<int> Add(AddCategoryModel model);
        Task<bool> Edit(EditCategoryModel model);
        Task<CategoryDetailsModel> Get(int id);
        Task<IEnumerable<CategoryListModel>> GetList();
        Task<bool> Delete(int id);
    }
}