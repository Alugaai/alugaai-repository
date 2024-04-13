using ApiCatalogo.Pagination;
using BackEndASP.DTOs.StudentDTOs;
using System.Text.Json;

namespace BackEndASP.Utils
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, PageStudentQueryParams queryParams, IEnumerable<StudentFindAllFilterDTO> data)
        {
            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var paginationHeader = new
            {
                totalCount = data.Count(),
                pageSize = queryParams.PageSize,
                currentPage = queryParams.PageNumber,
                totalPages = (int)Math.Ceiling(data.Count() / (double)queryParams.PageSize)
            };

            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, jsonOptions));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
