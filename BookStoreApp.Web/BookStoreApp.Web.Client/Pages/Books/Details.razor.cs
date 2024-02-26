using BookStoreApp.Shared.Services.Authors;
using BookStoreApp.Shared.Services.Books;
using Microsoft.AspNetCore.Components;

namespace BookStoreApp.Web.Client.Pages.Books
{
    public partial class Details
    {
        [Inject] protected IBooksService BooksService { get; set; } = null!;
        [Inject] protected NavigationManager NavigationManager { get; set; } = null!;

        [Parameter] public int id { get; set; }

        private BookReadOnlyDTO book = new BookReadOnlyDTO();

        protected async override Task OnInitializedAsync()
        {
            var response = await BooksService.Get(id);
            if (response.Success) book = response.Datas!;
        }

        private void HandleNavigateToEdit()
        {
            NavigationManager.NavigateTo($"/books/update/{id}");
        }
    }
}
