using System.ComponentModel.DataAnnotations;

namespace JobFinder.Application.Dtos.Paging
{
    public class PagingRequestModel
    {
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool Descending { get; set; }
        public string Search { get; set; }
    }
}