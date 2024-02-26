using BookStoreApp.Shared.DTO.Response;
using BookStoreApp.Shared.Services.Authors;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BookStoreApp.Web.Client.Pages.Authors
{
    public partial class Index
    {
        [Inject] IAuthorsService AuthorsService { get; set; } = null!;

        private List<AuthorReadOnlyDTO>? authors;
        private Response<VirtualizedResponse<AuthorReadOnlyDTO>> response = new Response<VirtualizedResponse<AuthorReadOnlyDTO>> { Success = true };
        private int totalSize { get; set; }

        protected override async Task OnInitializedAsync()
        {
            response = await AuthorsService.GetAuthors(new QueryParameters { StartIndex = 0, PageSize = 5 });

            if (response.Success)
            {
                authors = response.Datas!.Items;
                totalSize = response.Datas.TotalSize;
            }
        }

        private async Task LoadAuthors(QueryParameters queryParams)
        {
            response = await AuthorsService.GetAuthors(queryParams);

            if (response.Success)
            {
                authors = response.Datas!.Items;
                totalSize = response.Datas.TotalSize;
            }
        }
    }
}
