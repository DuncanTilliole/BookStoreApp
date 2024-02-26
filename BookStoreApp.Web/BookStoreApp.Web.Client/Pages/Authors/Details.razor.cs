using BookStoreApp.Shared.Services.Authors;
using Microsoft.AspNetCore.Components;

namespace BookStoreApp.Web.Client.Pages.Authors
{
    public partial class Details
    {
        [Inject] protected IAuthorsService authorsService { get; set; } = null!;
        [Inject] protected NavigationManager NavigationManager { get; set; } = null!;

        [Parameter] public int id { get; set; }

        private Author Author = new Author();

        protected async override Task OnInitializedAsync()
        {
            var response = await authorsService.GetAuthor(id);
            if (response.Success) Author = response.Datas!;

        }

        private void HandleNavigateToEdit()
        {
            NavigationManager.NavigateTo($"/authors/update/{id}");
        }
    }
}
