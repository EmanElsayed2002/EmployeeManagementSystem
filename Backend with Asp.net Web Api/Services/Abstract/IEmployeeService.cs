using OneOf;
using Services.DTO;
using Services.Errors;
using Services.Implementation;

namespace Services.Abstract
{
    public interface IEmployeeService
    {
        Task<IEnumerable<ReadEmployeeDTO>> GetAll();
        Task<OneOf<Error, ReadEmployeeDTO>> GetEmployeeById(int id);

        Task<OneOf<Error, ReadEmployeeDTO>> CreateEmployee(CreateEmployeeDTO request);
        Task<OneOf<Error, ReadEmployeeDTO>> UpdateEmployee(UpdateEmployeeDTO request);
        Task<OneOf<Error, bool>> DeleteEmployee(int id);

        Task<OneOf<Error, PaginatedResponse>> Search(string keyword, int pageSize, int pageNumber);
        Task<OneOf<Error, PaginatedResponse>> Paginated(int pageNumber, int pageSize);
    }
}
