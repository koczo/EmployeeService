﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Models.Objects;
using Logic;

namespace EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private EmployeeLogic logic = null;
        public EmployeeService()
        {
            logic = new EmployeeLogic();
        }

        public Employee GetEmployeeById(int id)
        {
            try
            {
                return logic.GetEmployeeById(id);
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
                return logic.SaveEmployee(employee);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
