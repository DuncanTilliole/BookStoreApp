using AutoMapper;
using BookStoreApp.Shared.DTO.Response;
using BookStoreApp.Shared.Services.Books;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BookStoreApp.Web.Client.Pages.Books
{
    public partial class Update
    {
        [Inject] protected IBooksService BooksService { get; set; } = null!;
        [Inject] protected NavigationManager NavigationManager { get; set; } = null!;
        [Inject] protected IMapper Mapper { get; set; } = null!;
        [Parameter] public int id { get; set; }

        private BookUpdateDTO book = new BookUpdateDTO();
        private string uploadFileWarning = string.Empty;
        private string img = string.Empty;
        private long maxFileSize = 1024 * 1024 * 5;

        protected async override Task OnInitializedAsync()
        {
            var response = await BooksService.Get(id);
            if (response.Success)
            {
                book = Mapper.Map<BookUpdateDTO>(response.Datas);
                img = book.Image;
            }

        }

        private async Task HandleUpdate()
        {
            Response<int> response = await BooksService.Update(id, book);

            if (response.Success)
                NavigationManager.NavigateTo("/books");
        }

        private async Task HandleFileSelection(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if (file != null)
            {
                var ext = Path.GetExtension(file.Name);

                if (file.Size > maxFileSize) uploadFileWarning = "File too big";

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
                else uploadFileWarning = "Please select a valid image file (*.jpeg | *.jpg | *.png)";
            }

        }
    }
}
