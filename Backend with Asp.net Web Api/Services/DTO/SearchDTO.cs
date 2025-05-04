using Data.Models;

namespace Services.DTO
{
    public class SearchDTO
    {
        public IEnumerable<Employee> Employees { get; set; }
    }
}
