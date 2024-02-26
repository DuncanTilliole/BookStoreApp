using AutoMapper;
using BookStoreApp.Shared.DTO.Response;
using BookStoreApp.Shared.Services.Authors;
using Microsoft.AspNetCore.Components;

namespace BookStoreApp.Web.Client.Pages.Authors
{
    public partial class Update
    {
        [Inject] protected IAuthorsService AuthorsService { get; set; } = null!;
        [Inject] protected NavigationManager NavigationManager { get; set; } = null!;
        [Inject] protected IMapper Mapper { get; set; } = null!;

        [Parameter]
        public int id { get; set; }

        private AuthorUpdateDTO Author = new AuthorUpdateDTO();

        protected async override Task OnInitializedAsync()
        {
            var response = await AuthorsService.GetAuthor(id);
            if (response.Success) Author = Mapper.Map<AuthorUpdateDTO>(response.Datas);

        }

        private async Task HandleUpdateAuthor()
        {
            Response<int> response = await AuthorsService.UpdateAuthor(id, Author);

            if (response.Success)
                NavigationManager.NavigateTo("/authors");
        }
    }
}
