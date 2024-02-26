using System.Reflection;
using System.Text;

namespace BookStoreApp.Shared.DTO.Response
{
    public class QueryParameters
    {
        public int _pageSize = 15;

        public int StartIndex { get; set; }

        public int PageSize { get { return _pageSize; } set { _pageSize = value; } }

        public string ToQueryString()
        {
            var queryString = new StringBuilder();
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                var value = prop.GetValue(this);
                if (value != null)
                {
                    if (queryString.Length > 0)
                        queryString.Append('&');
                    queryString.Append(Uri.EscapeDataString(prop.Name));
                    queryString.Append('=');
                    queryString.Append(Uri.EscapeDataString(value.ToString()));
                }
            }
            return queryString.ToString();
        }
    }
}
