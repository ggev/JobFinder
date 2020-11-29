using System.Threading.Tasks;
using JobFinder.Application.Dtos.Jobs;
using JobFinder.Application.Dtos.Paging;

namespace JobFinder.Application.Services.Jobs
{
    public interface IJobService
    {
        Task<int> Add(AddJobModel model);
        Task<bool> Edit(EditJobModel model);
        Task<JobDetailsModel> Get(int id, string userId);
        Task<PagingResponseModel<JobListModel>> GetList(JobPagingRequestModel model, string userId);
        Task<bool> Delete(int id);
        Task<bool> Bookmark(int id, bool mark, string userId);
        Task<bool> Apply(int id, string userId);
    }
}