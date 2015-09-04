using Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.Factories
{
    public static class FactoryEmployee
    {
        private static Lazy<Dictionary<EmployeeType, Employee>> employees = new Lazy<Dictionary<EmployeeType, Employee>>();
        public static Dictionary<EmployeeType, Employee> Employees
        {
            get { return employees.Value; }
        }

        public static Employee Create(EmployeeType employeeType)
        {
            employees = new Lazy<Dictionary<EmployeeType, Employee>>(LoadEmployees);
            return Employees[employeeType];
        }

        public static Dictionary<EmployeeType, Employee> LoadEmployees()
        {
            Dictionary<EmployeeType, Employee> cus = new Dictionary<EmployeeType, Employee>();
            cus.Add(EmployeeType.FullTimeEmployee, new FullTimeEmployee());
            cus.Add(EmployeeType.PartTimeEmployee, new PartTimeEmployee());
            return cus;
        }
    }
}
