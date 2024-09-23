namespace CityInfo.API.Models.Services
{
    public class PaginationMetadata
    {
        // độ dài của List.lenght()
        public int TotalItemCount { get; set; }

        // số trang được chia bằng cách Lấy độ dài mảng chia cho pageSize
        public int TotalPageCount { get; set; }

        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

        public PaginationMetadata(int totalItemCount, int pageSize, int currentPage) {
            TotalItemCount = totalItemCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
        }
    }
}
