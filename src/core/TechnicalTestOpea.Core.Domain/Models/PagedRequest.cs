namespace TechnicalTestOpea.Core.Domain.Models
{
    public class PagedRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public string? PropertyName { get; set; }
        public string? PropertyValue { get; set; }
        public string? OrderByColumn { get; set; }
        public bool? IsAcending { get; set; }


        public static implicit operator FilterHelper.BasePagedFilter(PagedRequest request)
        {
            return new FilterHelper.BasePagedFilter
                (
                request.InitialDate,
                request.FinalDate,
                request.PropertyName,
                request.PropertyValue,
                request.OrderByColumn,
                request.IsAcending,
                request.Page,
                request.PageSize);
        }
    }
}
