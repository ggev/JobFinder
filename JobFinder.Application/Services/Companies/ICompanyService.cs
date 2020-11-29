using System.Threading.Tasks;
using JobFinder.Application.Dtos.Companies;
using JobFinder.Application.Dtos.Paging;

namespace JobFinder.Application.Services.Companies
{
    public interface ICompanyService
    {
        Task<int> Add(AddCompanyModel model);
        Task<bool> Edit(EditCompanyModel model);
        Task<CompanyDetailsModel> Get(int id);
        Task<PagingResponseModel<CompanyListModel>> GetList(CompanyPagingRequestModel model);
        Task<bool> Delete(int id);
    }
}