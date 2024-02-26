using BookStoreApp.Shared.DTO.Response;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using BookStoreApp.Shared.Services.Authors;

namespace BookStoreApp.Web.Client.Components
{
    public partial class AuthorsTable
    {
        [Inject] protected IAuthorsService AuthorsService { get; set; } = null!;
        [Inject] protected IJSRuntime js { get; set; } = null!;
        [Parameter] public List<AuthorReadOnlyDTO> Authors { get; set; } = null!;
        [Parameter] public int TotalSize { get; set; }
        [Parameter] public EventCallback<QueryParameters> OnScroll { get; set; }

        private async ValueTask<ItemsProviderResult<AuthorReadOnlyDTO>> LoadAuthors(ItemsProviderRequest request)
        {
            var productNum = Math.Min(request.Count, TotalSize - request.StartIndex);
            await OnScroll.InvokeAsync(new QueryParameters
            {
                StartIndex = request.StartIndex,
                PageSize = productNum == 0 ? request.Count : productNum
            });

            return new ItemsProviderResult<AuthorReadOnlyDTO>(Authors, TotalSize);
        }

        private async void HandleDeleteAuthor(int Id)
        {
            bool confirm = await js.InvokeAsync<bool>("confirm", "Are you sure to delete this ressource?");
            if (confirm)
            {
                await AuthorsService.DeleteAuthor(Id);
                await OnInitializedAsync();
            }

        }
    }
}
