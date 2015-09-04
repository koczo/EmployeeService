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
        EmployeeEngine engine = null;
        public EmployeeLogic()
        {
            EmployeeEngine engine = new EmployeeEngine();
        }

        public Employee GetEmployeeById(int id)
        {
            try
            {
               return  engine.GetEmployeeById(id);
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
                return engine.SaveEmployee(employee);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
