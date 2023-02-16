﻿using Models;
using DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Storage;

namespace Services.Storages
{
    public class EmployeeStorage : IStorage<Employee>
    {
        private List<Employee> data = new List<Employee>();

        public List<Employee> Data => data;

        public void Add(Employee employee)
        {
            Data.Add(employee);
        }

        public void Delete(Employee employee)
        {
            Data.Remove(employee);
        }

        public void Update(Employee employee)
        {
            var oldEmployee = Data.First(p => p.PassportID == employee.PassportID);

            oldEmployee.Name = employee.Name;
            oldEmployee.PassportID = employee.PassportID;
            oldEmployee.DateOfBirth = employee.DateOfBirth;

        }
    }
}
