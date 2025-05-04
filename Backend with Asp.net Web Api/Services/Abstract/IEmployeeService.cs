using OneOf;
using Services.DTO;
using Services.Errors;

namespace Services.Abstract
{
    public interface IEmployeeService
    {
        Task<IEnumerable<ReadEmployeeDTO>> GetAll();
        Task<OneOf<Error, ReadEmployeeDTO>> GetEmployeeById(int id);

        Task<OneOf<Error, ReadEmployeeDTO>> CreateEmployee(CreateEmployeeDTO request);
        Task<OneOf<Error, ReadEmployeeDTO>> UpdateEmployee(UpdateEmployeeDTO request);
        Task<OneOf<Error, bool>> DeleteEmployee(int id);

        Task<OneOf<Error, IEnumerable<ReadEmployeeDTO>>> Search(string keyword);
        Task<OneOf<Error, IEnumerable<ReadEmployeeDTO>>> Paginated(int pageNumber, int pageSize);
    }
}
