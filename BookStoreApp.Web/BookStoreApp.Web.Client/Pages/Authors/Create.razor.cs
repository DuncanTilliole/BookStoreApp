using BookStoreApp.Shared.DTO.Response;
using BookStoreApp.Shared.Services.Authors;
using Microsoft.AspNetCore.Components;

namespace BookStoreApp.Web.Client.Pages.Authors
{
    public partial class Create
    {
        [Inject] protected IAuthorsService AuthorsService { get; set; } = null!;
        [Inject] protected NavigationManager NavigationManager { get; set; } = null!;

        private AuthorCreateDTO Author = new AuthorCreateDTO();

        private async Task HandleCreate()
        {
            Response<int> response = await AuthorsService.CreateAuthor(Author);

            if (response.Success)
                NavigationManager.NavigateTo("/authors");
        }
    }
}
