using JobFinder.Application.Dtos.Jobs;
using JobFinder.Application.Dtos.Paging;
using JobFinder.Application.Services.Jobs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers
{
    public class JobController : BaseController
    {
        private readonly IJobService _service;

        public JobController(IJobService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<int> Add([FromBody] AddJobModel model)
        {
            return await _service.Add(model);
        }

        [HttpPut]
        public async Task<bool> Edit([FromBody] EditJobModel model)
        {
            return await _service.Edit(model);
        }

        [HttpGet]
        public async Task<JobDetailsModel> Get([FromRoute] int id, [FromHeader] string userId)
        {
            return await _service.Get(id, userId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<PagingResponseModel<JobListModel>> GetList([FromBody]JobPagingRequestModel model, [FromHeader] string userId)
        {
            return await _service.GetList(model, userId);
        }

        [HttpDelete]
        public async Task<bool> Delete([FromRoute] int id)
        {
            return await _service.Delete(id);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<bool> Bookmark(int id, bool mark, [FromHeader] string userId)
        {
            return await _service.Bookmark(id, mark, userId);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<bool> Apply(int id, [FromHeader] string userId)
        {
            return await _service.Apply(id, userId);
        }
    }
}