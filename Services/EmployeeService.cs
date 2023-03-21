using AutoMapper;
using DbModels;
using DbModels.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.Exceptions;
using Services.Filters;

namespace Services
{
    public class EmployeeService
    {
        public readonly IMapper _mapper;
        public readonly BankContext _dbContext;
        public EmployeeService(IMapper mapper, BankContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<EmployeeDto> GetEmployeeAsync(Guid id)
        {

            var employee = await _dbContext.Employees.FirstOrDefaultAsync(p => p.Id == id);

            var response = _mapper.Map<EmployeeDto>(employee);
            return response;
        }
        //public async Task<List<EmployeeDto>> GetEmployeesAsync(EmployeesFilter employeeFilter)
        //{
        //    var selection = _dbContext.Employees.Select(p => p);

        //    if (employeeFilter.Name != null)
        //        selection = selection.
        //            Where(p => p.Name == employeeFilter.Name);

        //    if (employeeFilter.PassportID != null)
        //        selection = selection.
        //           Where(p => p.PassportID == employeeFilter.PassportID);

        //    if (employeeFilter.StartDate != new DateTime())
        //        selection = selection.
        //           Where(p => p.DateOfBirth == employeeFilter.StartDate);

        //    if (employeeFilter.EndDate != new DateTime())
        //        selection = selection.
        //           Where(p => p.DateOfBirth == employeeFilter.EndDate);

        //    if (employeeFilter.Salary != 0)
        //        selection = selection.
        //           Where(p => p.PassportID == employeeFilter.PassportID);

        //    var response = selection.Select(e => _mapper.Map<EmployeeDto>(e)).ToList();

        //    return response;
        //}        
        public async Task AddEmployeeAsync(EmployeeDto employeeDto)
        {
            var employeeDb = new EmployeeDB
            {
                Name = employeeDto.Name,
                PassportID = employeeDto.PassportID,
                DateOfBirth = employeeDto.DateOfBirth,
            };

            if ((DateTime.Now - employeeDto.DateOfBirth).Days < 18)
                throw new Limit18YearsException("Сотрудник должен быть старше 18 лет");

            if (employeeDto.PassportID == null)
                throw new NoPassportDataException("Вы не ввели паспортные данные");
            await _dbContext.Employees.AddAsync(employeeDb);
            await _dbContext.SaveChangesAsync();

        }
        public async Task DeleteEmployeeAsync(Guid EmployeeId)
        {
            var employeeDb = _dbContext.Employees.FirstOrDefault(p => p.Id == EmployeeId);

            if (employeeDb == null)
                throw new KeyNotFoundException("В базе нет такого сотрудника");

            _dbContext.Employees.Remove(employeeDb);
            await _dbContext.SaveChangesAsync();

        }
        public async Task UpdateEmployeeAsync(EmployeeDto employeeDto, Guid EmployeeId)
        {
            var employeeDb = _dbContext.Employees.FirstOrDefault(p => p.Id == EmployeeId);

            if (employeeDb == null)
                throw new KeyNotFoundException("В базе нет такого сотрудника");

            employeeDb.Name = employeeDto.Name;
            employeeDb.PassportID = employeeDto.PassportID;
            employeeDb.DateOfBirth = employeeDto.DateOfBirth;
            employeeDb.Salary = employeeDto.Salary;

            _dbContext.Employees.Update(employeeDb);
            await _dbContext.SaveChangesAsync();

        }
    }
}
