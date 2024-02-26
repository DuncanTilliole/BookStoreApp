namespace BookStoreApp.Shared.DTO.Response
{
    public class VirtualizedResponse<T>
    {
        public List<T> Items { get; set; }

        public int TotalSize { get; set; }
    }
}
