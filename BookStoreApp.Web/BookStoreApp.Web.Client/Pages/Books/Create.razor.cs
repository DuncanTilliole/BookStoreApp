using AutoMapper;
using BookStoreApp.Shared.DTO.Response;
using BookStoreApp.Shared.Services.Authors;
using BookStoreApp.Shared.Services.Books;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BookStoreApp.Web.Client.Pages.Books
{
    public partial class Create
    {
        [Inject] protected IBooksService BooksService { get; set; } = null!;
        [Inject] protected IAuthorsService AuthorsService { get; set; } = null!;
        [Inject] protected IMapper Mapper { get; set; } = null!;
        [Inject] protected NavigationManager NavigationManager { get; set; } = null!;

        private BookCreateDTO book = new BookCreateDTO();
        private List<AuthorReadOnlyDTO>? authors;
        private string UploadFileWarning = string.Empty;
        private string img = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            var response = await AuthorsService.GetAuthors(new QueryParameters { StartIndex = 0 });
            if (response.Success) authors = response.Datas.Items;
        }

        private async Task HandleCreate()
        {
            Response<int> response = await BooksService.Create(book);

            if (response.Success)
                NavigationManager.NavigateTo("/books");
        }

        private async Task HandleFileSelection(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if (file != null)
            {
                var ext = Path.GetExtension(file.Name);
                if (ext.ToLower().Contains("jpg") || ext.ToLower().Contains("png") || ext.ToLower().Contains("jpeg"))
                {
                    var byteArray = new byte[file.Size];
                    await file.OpenReadStream().ReadAsync(byteArray);
                    string imageType = file.ContentType;
                    string base64string = Convert.ToBase64String(byteArray);

                    book.ImageData = base64string;
                    book.OriginalImageName = file.Name;
                    img = $"data:{imageType}; base64, {base64string}";
                }
                else UploadFileWarning = "Please select a valid image file (*.jpeg | *.jpg | *.png)";
            }

        }
    }
}
