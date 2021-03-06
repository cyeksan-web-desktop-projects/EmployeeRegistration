using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeRegistration.Models
{
    public class EmployeeDAL
    {
        string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=EMPLOYEEDB;Persist Security Info=False;integrated security=SSPI";

        //Get All Employees
        public IEnumerable<Employee> GetAllEmployee()
        {
            List<Employee> empList = new List<Employee>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SP_GetAllEmployee", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlConnection.Open();
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    Employee employee = new Employee();
                    employee.ID = Convert.ToInt32(dataReader["ID"].ToString());
                    employee.Name = dataReader["Name"].ToString();
                    employee.Gender = dataReader["Gender"].ToString();
                    employee.Company = dataReader["Company"].ToString();
                    employee.Department = dataReader["Department"].ToString();

                    empList.Add(employee);
                }

                sqlConnection.Close();

            }

            return empList;
        }

        //To insert employee
        public void AddEmployee(Employee emp)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SP_InsertEmployee", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@Name", emp.Name);
                sqlCommand.Parameters.AddWithValue("@Gender", emp.Gender);
                sqlCommand.Parameters.AddWithValue("@Company", emp.Company);
                sqlCommand.Parameters.AddWithValue("@Department", emp.Department);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

            }

        }

        //To update employee
        public void UpdateEmployee(Employee emp)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SP_UpdateEmployee", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@EmpId", emp.ID);
                sqlCommand.Parameters.AddWithValue("@Name", emp.Name);
                sqlCommand.Parameters.AddWithValue("@Gender", emp.Gender);
                sqlCommand.Parameters.AddWithValue("@Company", emp.Company);
                sqlCommand.Parameters.AddWithValue("@Department", emp.Department);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

            }

        }

        //To delete employee
        public void DeleteEmployee(int? empId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SP_DeleteEmployee", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@EmpId", empId);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

            }

        }

        //Get Employee By ID
        public Employee GetEmployeeById(int? empId)
        {
            Employee employee = new Employee();


            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SP_GetEmployeeById", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EmpId", empId);
                sqlConnection.Open();
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    employee.ID = Convert.ToInt32(dataReader["ID"].ToString());
                    employee.Name = dataReader["Name"].ToString();
                    employee.Gender = dataReader["Gender"].ToString();
                    employee.Company = dataReader["Company"].ToString();
                    employee.Department = dataReader["Department"].ToString();
                }

                sqlConnection.Close();

            }

            return employee;
        }
    }

}
