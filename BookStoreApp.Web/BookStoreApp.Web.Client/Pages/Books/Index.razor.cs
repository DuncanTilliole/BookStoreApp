using BookStoreApp.Shared.DTO.Response;
using BookStoreApp.Shared.Services.Books;
using Microsoft.AspNetCore.Components;

namespace BookStoreApp.Web.Client.Pages.Books
{
    public partial class Index
    {
        [Inject] protected IBooksService BooksService { get; set; } = null!;

        private List<BookReadOnlyDTO>? books;
        private Response<VirtualizedResponse<BookReadOnlyDTO>> response = new Response<VirtualizedResponse<BookReadOnlyDTO>> { Success = true };
        private int totalSize { get; set; }

        protected override async Task OnInitializedAsync()
        {
            response = await BooksService.Get(new QueryParameters { StartIndex = 0 });

            if (response.Success)
            {
                books = response.Datas!.Items;
                totalSize = response.Datas.TotalSize;
            }

        }

        private async Task Load(QueryParameters queryParams)
        {
            response = await BooksService.Get(queryParams);

            if (response.Success)
            {
                books = response.Datas!.Items;
                totalSize = response.Datas.TotalSize;
            }
        }
    }
}
