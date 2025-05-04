using Data.Models;

namespace Services.DTO
{
    public class PaginatedSearchDTO
    {
        public IEnumerable<Employee> Items { get; set; }

    }
}
