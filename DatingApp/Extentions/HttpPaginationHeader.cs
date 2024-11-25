using System.Text.Json;

namespace DatingApp.Extentions
{
    public static class HttpPaginationHeader
    {
        public static void AddPaginationHeader(this HttpResponse response, int currantPage, 
            int pageSize, int totalCount, int totalPages)
        {
            var header = new
            {
                currantPage, pageSize, totalCount, totalPages
            };

            var option= new JsonSerializerOptions { PropertyNamingPolicy=JsonNamingPolicy.CamelCase};

            response.Headers.Add("Pagination",JsonSerializer.Serialize(header,option));

            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");

        }
    }
}
