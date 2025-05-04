using AutoMapper;
using Data.Models;
using Infrastructure.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Services.Abstract;
using Services.DTO;
using Services.Errors;

namespace Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _repo;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<OneOf<Error, ReadEmployeeDTO>> CreateEmployee(CreateEmployeeDTO request)
        {
            try
            {
                var emp = _mapper.Map<Employee>(request);

                var res = await _repo.AddAsync(emp);

                if (res == null)
                {
                    return new Error("Failed to create employee.", "", StatusCodes.Status400BadRequest);
                }

                var resultDto = _mapper.Map<ReadEmployeeDTO>(res);
                return resultDto;
            }
            catch (Exception ex)
            {
                return new Error($"Exception occurred: {ex.Message}", "", StatusCodes.Status400BadRequest);
            }
        }


        public async Task<OneOf<Error, bool>> DeleteEmployee(int id)
        {
            var res = await _repo.DeleteAsync(id);
            if (res) return true;
            return new Error("Deleted Failed", "failed to delete", StatusCodes.Status400BadRequest);
        }

        public async Task<IEnumerable<ReadEmployeeDTO>> GetAll()
        {
            var res = await _repo.GetTableAsTracking().ToListAsync();
            return _mapper.Map<IEnumerable<ReadEmployeeDTO>>(res);
        }

        public async Task<OneOf<Error, ReadEmployeeDTO>> GetEmployeeById(int id)
        {
            var res = await _repo.GetByIdAsync(id);
            if (res == null) return new Error("Employee Not Found", "Can not find Employee", StatusCodes.Status400BadRequest);
            return _mapper.Map<ReadEmployeeDTO>(res);
        }

        public async Task<OneOf<Error, IEnumerable<ReadEmployeeDTO>>> Paginated(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return new Error("Invalid Pagination", "Page number and size must be greater than zero.", StatusCodes.Status400BadRequest);
            }

            var employees = await _repo.GetTableAsTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (!employees.Any())
            {
                return new Error("No Data", "No employees found for the given page.", StatusCodes.Status404NotFound);
            }

            var res = _mapper.Map<List<ReadEmployeeDTO>>(employees);
            return res;
        }


        public async Task<OneOf<Error, IEnumerable<ReadEmployeeDTO>>> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return new Error("Invalid Keyword", "Search keyword cannot be empty.", StatusCodes.Status400BadRequest);
            }

            var results = await _repo.GetTableAsTracking()
                .Where(e =>
                    EF.Functions.Like(e.FirstName, $"%{keyword}%") ||
                    EF.Functions.Like(e.LastName, $"%{keyword}%") ||
                    EF.Functions.Like(e.Email, $"%{keyword}%"))
                .ToListAsync();

            if (!results.Any())
            {
                return new Error("No Results", $"No employees match the keyword '{keyword}'.", StatusCodes.Status404NotFound);
            }

            return _mapper.Map<List<ReadEmployeeDTO>>(results);
        }

        public async Task<OneOf<Error, ReadEmployeeDTO>> UpdateEmployee(UpdateEmployeeDTO request)
        {
            var emp = await _repo.GetTableAsTracking()
                       .FirstOrDefaultAsync(e => e.Email == request.Email);


            if (emp == null)
            {
                return new Error(
                    "Employee Not Found",
                   "Cannot find employee with the provided email.",
                   StatusCodes.Status400BadRequest
                );
            }


            emp.FirstName = request.FirstName;
            emp.LastName = request.LastName;
            emp.Position = request.Position;


            var res = _mapper.Map<ReadEmployeeDTO>(emp);
            await _repo.SaveChangesAsync();
            return res;
        }

    }
}
