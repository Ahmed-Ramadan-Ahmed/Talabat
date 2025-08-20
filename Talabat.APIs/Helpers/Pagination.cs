namespace Talabat.APIs.Helpers
{
    public class Pagination<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public int CurrentPageCount => Data?.Count ?? 0;
        public int TotalPages => (int)Math.Ceiling((double)Count / PageSize);
        public IReadOnlyList<T> Data { get; set; }
        public Pagination() { }
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}
