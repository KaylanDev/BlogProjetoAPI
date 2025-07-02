namespace Blog.API.PaginationHandler
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50; // Limite máximo para o tamanho da página
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
