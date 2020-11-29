using System.Collections.Generic;

namespace JobFinder.Application.Dtos.Paging
{
    public sealed class PagingResponseModel<T>
    {
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
    }
}