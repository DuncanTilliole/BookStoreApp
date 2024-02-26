using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Shared.DTO.Response
{
    public class Response<T>
    {
        public string? Message { get; set; }

        public string? ValidationErrors { get; set; }

        public required bool Success { get; set; }

        public T? Datas { get; set; }
    }
}
