using System.Collections.Generic;
using System.Threading.Tasks;
using JobFinder.Application.Dtos.Categories;
using JobFinder.Application.Services.Categories;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<int> Add([FromBody] AddCategoryModel model)
        {
            return await _service.Add(model);
        }

        [HttpPut]
        public async Task<bool> Edit([FromBody] EditCategoryModel model)
        {
            return await _service.Edit(model);
        }

        [HttpGet]
        public async Task<CategoryDetailsModel> Get([FromRoute] int id)
        {
            return await _service.Get(id);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IEnumerable<CategoryListModel>> GetList()
        {
            return await _service.GetList();
        }

        [HttpDelete]
        public async Task<bool> Delete([FromRoute] int id)
        {
            return await _service.Delete(id);
        }
    }
}