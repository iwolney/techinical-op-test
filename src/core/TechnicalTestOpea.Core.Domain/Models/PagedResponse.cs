namespace TechnicalTestOpea.Core.Domain.Models
{
    public class PagedResponse<T> where T: class
    {
        public IEnumerable<T> Items { get; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);

        public PagedResponse(IEnumerable<T> items, int totalItems, int page, int pageSize)
        {
            Items = items;
            TotalItems = totalItems;
            Page = page;
            PageSize = pageSize;
        }


        public PagedResponse<TDest> MapTo<TDest>(Func<T, TDest> converter) where TDest : class 
        {
            var convertedItems = Items.Select(converter).ToList();
            return new PagedResponse<TDest>(convertedItems, TotalItems, Page, PageSize);
        }
    }
}
