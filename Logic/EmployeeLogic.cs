using DataAccess;
using Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class EmployeeLogic
    {
        public Employee GetEmployeeById(int id)
        {
            try
            {
                return EmployeeEngine.GetEmployeeById(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int SaveEmployee(Employee employee)
        {
            try
            {
                return EmployeeEngine.SaveEmployee(employee);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
