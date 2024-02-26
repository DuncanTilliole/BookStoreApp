using BookStoreApp.Shared.DTO.Response;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using BookStoreApp.Shared.Services.Books;

namespace BookStoreApp.Web.Client.Components
{
    public partial class BooksTable
    {
        [Inject] protected IBooksService BooksService { get; set; } = null!;
        [Inject] protected IJSRuntime js { get; set; } = null!;
        [Parameter] public List<BookReadOnlyDTO> Books { get; set; } = null!;

        [Parameter] public int TotalSize { get; set; }

        [Parameter] public EventCallback<QueryParameters> OnScroll { get; set; }

        private async ValueTask<ItemsProviderResult<BookReadOnlyDTO>> Load(ItemsProviderRequest request)
        {
            var productNum = Math.Min(request.Count, TotalSize - request.StartIndex);
            await OnScroll.InvokeAsync(new QueryParameters
            {
                StartIndex = request.StartIndex,
                PageSize = productNum == 0 ? request.Count : productNum
            });

            return new ItemsProviderResult<BookReadOnlyDTO>(Books, TotalSize);
        }

        private async void HandleDelete(int Id)
        {
            bool confirm = await js.InvokeAsync<bool>("confirm", "Are you sure to delete this ressource?");
            if (confirm)
            {
                await BooksService.Delete(Id);
                await OnInitializedAsync();
            }

        }
    }
}
