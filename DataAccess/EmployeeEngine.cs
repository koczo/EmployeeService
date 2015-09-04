using Models.Factories;
using Models.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DataAccess
{
    public class EmployeeEngine
    {
        public SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            connection.Open();
            return connection;
        }

        public Employee GetEmployeeById(int id)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    SqlCommand command = new SqlCommand("spGetEmployeeById", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.Add(new SqlParameter("@Id", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Employee employee = null;
                        if (reader.Read())
                        {
                            EmployeeType employeeType = (EmployeeType)Convert.ToInt32(reader["EmployeeType"]);
                            employee = FactoryEmployee.Create(employeeType);
                            employee.Id = Convert.ToInt32(reader["Id"]);
                            employee.Name = reader["Name"].ToString();
                            employee.Gender = reader["Gender"].ToString();
                            employee.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                            employee.EmployeeType = employeeType;

                            if (employee.EmployeeType == EmployeeType.FullTimeEmployee)
                            {
                                ((FullTimeEmployee)employee).AnnualySalary = Convert.ToInt32(reader["AnnualySalary"]);
                            }
                            else if (employee.EmployeeType == EmployeeType.PartTimeEmployee)
                            {
                                ((PartTimeEmployee)employee).HourlyPay = Convert.ToInt32(reader["HourlyPay"]);
                                ((PartTimeEmployee)employee).HoursWorked = Convert.ToInt32(reader["HoursWorked"]);
                            }
                        }
                        return employee;
                    }
                }
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
                if (employee == null)
                    throw new ArgumentNullException("employee");

                using (SqlConnection connection = GetConnection())
                {
                    SqlCommand command = new SqlCommand("spSaveEmployee", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.Add(new SqlParameter("@Name", employee.Name));
                    command.Parameters.Add(new SqlParameter("@Gender", employee.Gender));
                    command.Parameters.Add(new SqlParameter("@DateOfBirth", employee.DateOfBirth));
                    command.Parameters.Add(new SqlParameter("@EmployeeType", employee.EmployeeType));
                    if (employee.EmployeeType == EmployeeType.FullTimeEmployee)
                    {
                        command.Parameters.Add(new SqlParameter("@AnnualySalary", ((FullTimeEmployee)employee).AnnualySalary));
                    }
                    else if (employee.EmployeeType == EmployeeType.PartTimeEmployee)
                    {
                        command.Parameters.Add(new SqlParameter("@HourlyPay", ((PartTimeEmployee)employee).HourlyPay));
                        command.Parameters.Add(new SqlParameter("@HoursWorked", ((PartTimeEmployee)employee).HoursWorked));
                    }

                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
