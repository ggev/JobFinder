using JobFinder.Application.Dtos.Paging;
using JobFinder.Application.Enums.OrderCriteria;
using JobFinder.Domain.Enums;
using System.Collections.Generic;

namespace JobFinder.Application.Dtos.Jobs
{
    public class JobPagingRequestModel : PagingRequestModel
    {
        public IEnumerable<int> CategoryIds { get; set; }
        public IEnumerable<City> Locations { get; set; }
        public IEnumerable<EmploymentType> EmploymentTypes { get; set; }
        public bool? Bookmarked { get; set; }
        public bool? AppliedForJob { get; set; }
        public JobOrderBy OrderBy { get; set; }
    }
}