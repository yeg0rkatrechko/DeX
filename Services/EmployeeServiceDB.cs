﻿using DbModels;
using Models;
using Services.Exceptions;
using Services.Filters;

namespace Services
{
    public class EmployeeServiceDB
    {
        public BankContext dbContext;
        public EmployeeServiceDB()
        {
            dbContext = new BankContext();
        }
        public EmployeeDB GetEmployee(Guid employeeID)
        {
            var employee = dbContext.Employees.FirstOrDefault(c => c.Id == employeeID);

            if (employee == null)
            {
                throw new ExistenceException("Этого работника не сущетсвует");
            }
            return dbContext.Employees.FirstOrDefault(c => c.Id == employeeID);
        }
        //public async Task<EmployeeDB> GetEmployeeAsync(Guid id)
        //{

        //    var employeeDb = await dbContext.Employees.FirstOrDefaultAsync(p => p.Id == id);
        //    if (employeeDb == null)
        //    {
        //        throw new ExistenceException("Этого работника не сущетсвует");
        //    }
        //    return dbContext.Employees.FirstOrDefault(c => c.Id == id);
        //}
        public List<Employee> GetEmployees(EmployeesFilter employeeFilter)
        {
            var selection = dbContext.Employees.Select(p => p);

            if (employeeFilter.Name != null)
                selection = selection.
                    Where(p => p.Name == employeeFilter.Name);

            if (employeeFilter.PassportID != null)
                selection = selection.
                   Where(p => p.PassportID == employeeFilter.PassportID);

            if (employeeFilter.StartDate != new DateTime())
                selection = selection.
                   Where(p => p.DateOfBirth == employeeFilter.StartDate);

            if (employeeFilter.EndDate != new DateTime())
                selection = selection.
                   Where(p => p.DateOfBirth == employeeFilter.EndDate);

            if (employeeFilter.Salary != 0)
                selection = selection.
                   Where(p => p.PassportID == employeeFilter.PassportID);

            return selection.Select(employeeDb => new Employee()
            {
                ID = employeeDb.Id,
                Name = employeeDb.Name,
                PassportID = employeeDb.PassportID,
                DateOfBirth = employeeDb.DateOfBirth
            })
            .ToList();
        }
        public void AddEmployee(Employee employee)
        {
            var employeeDb = new EmployeeDB()
            {
                Id = employee.ID,
                Name = employee.Name,
                PassportID = employee.PassportID,
                DateOfBirth = employee.DateOfBirth
            };
            if (employeeDb.PassportID == null)
            {
                throw new NoPassData("Вы не ввели паспортные данные");
            }

            if (DateTime.Now.Year - employeeDb.DateOfBirth.Year < 18)
            {
                throw new Under18("Работник не может быть младше 18-ти лет");
            }
            dbContext.Employees.Add(employeeDb);
            dbContext.SaveChanges();
        }
        public async Task AddEmployeeAsync(Employee employee)
        {
            var employeeDb = new EmployeeDB
            {
                Id = employee.ID,
                Name = employee.Name,
                PassportID = employee.PassportID,
                DateOfBirth = employee.DateOfBirth,
            };

            if ((DateTime.Now - employee.DateOfBirth).Days < 18)
                throw new Under18("Сотрудник должен быть старше 18 лет");

            if (employee.PassportID == null)
                throw new NoPassData("Вы не ввели паспортные данные");
            await dbContext.Employees.AddAsync(employeeDb);
            await dbContext.SaveChangesAsync();

        }
        public void UpdateEmployee(Employee employee)
        {
            var employeeDb = dbContext.Employees.FirstOrDefault(c => c.Id == employee.ID);

            if (employeeDb == null)
            {
                throw new ExistenceException("Данного работника не существует");
            }

            dbContext.Employees.Update(employeeDb);
            dbContext.SaveChanges();
        }
        public void DeleteEmployee(Employee employee)
        {
            var employeeDb = dbContext.Employees.FirstOrDefault(c => c.Id == employee.ID);
            if (employeeDb == null)
            {
                throw new ExistenceException("Работник с данным ID отсутсвтует в базе");
            }
            dbContext.Employees.Remove(employeeDb);
            dbContext.SaveChanges();
        }
    }
}
