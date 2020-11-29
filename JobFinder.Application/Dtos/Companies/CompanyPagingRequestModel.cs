using JobFinder.Application.Dtos.Paging;
using JobFinder.Application.Enums.OrderCriteria;
using JobFinder.Domain.Enums;

namespace JobFinder.Application.Dtos.Companies
{
    public class CompanyPagingRequestModel : PagingRequestModel
    {
        public City? Location { get; set; }
        public string Address { get; set; }
        public CompanyOrderBy OrderBy { get; set; }
    }
}