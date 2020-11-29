using JobFinder.Application.Dtos.Companies;
using JobFinder.Application.Dtos.Paging;
using JobFinder.Application.Services.Companies;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobFinder.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<int> Add([FromForm] AddCompanyModel model)
        {
            return await _service.Add(model);
        }

        [HttpPut]
        public async Task<bool> Edit([FromForm] EditCompanyModel model)
        {
            return await _service.Edit(model);
        }

        [HttpGet]
        public async Task<CompanyDetailsModel> Get([FromRoute] int id)
        {
            return await _service.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<PagingResponseModel<CompanyListModel>> GetList([FromBody] CompanyPagingRequestModel model)
        {
            return await _service.GetList(model);
        }

        [HttpDelete]
        public async Task<bool> Delete([FromRoute] int id)
        {
            return await _service.Delete(id);
        }
    }
}
