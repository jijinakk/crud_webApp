using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using crud_webApp.Models;
using System.Data;

namespace crud_webApp.DAL
{
    public class employee_DAL
    {
        string con = ConfigurationManager.ConnectionStrings["sspConnectionString"].ToString();
        public List<Employee> GetAllemployee()
        {
            List<Employee> EmployeeList = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ssp_GetAllEmployee";
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                connection.Open();
                sqlDA.Fill(dt);
                connection.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeList.Add(new Employee
                    {
                        employeeID = Convert.ToInt32(dr["employeeID"]),
                        employeeName = Convert.ToString(dr["employeeName"]),
                        dateOfBirth = Convert.ToDateTime(dr["dateOfBirth"]).Date,
                        phoneNumber = Convert.ToString(dr["phoneNumber"]),
                        email = Convert.ToString(dr["email"]),
                    });
                }

            }

            return EmployeeList;

        }


        public List<Employee> GetemployeeByID(int EmployeeID)
        {
            List<Employee> EmployeeList = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ssp_GetEmployeeByID";
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                connection.Open();
                sqlDA.Fill(dt);
                connection.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeList.Add(new Employee
                    {
                        employeeID = Convert.ToInt32(dr["employeeID"]),
                        employeeName = Convert.ToString(dr["employeeName"]),
                        dateOfBirth = Convert.ToDateTime(dr["dateOfBirth"]).Date,
                        phoneNumber = Convert.ToString(dr["phoneNumber"]),
                        email = Convert.ToString(dr["email"]),
                    });
                }

            }

            return EmployeeList;

        }


        public bool InsertEmployee(Employee employee)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(con))

            {
                SqlCommand cmd = new SqlCommand("[dbo].[ssp_InsertEmployee]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeName", employee.employeeName);
                cmd.Parameters.AddWithValue("@DateOfBirth", employee.dateOfBirth);
                cmd.Parameters.AddWithValue("@PhoneNumber", employee.phoneNumber);
                cmd.Parameters.AddWithValue("@Email", employee.email);
                connection.Open();
                id = cmd.ExecuteNonQuery();
                connection.Close();


            }
            if (id > 0)
            {
                return true;


            }
            else
            {
                return false;
            }
        }



        public bool UpdateEmployee(Employee employee)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(con))

            {
                SqlCommand cmd = new SqlCommand("[dbo].[ssp_UpdateEmployee]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@employeeID", employee.employeeID);

                cmd.Parameters.AddWithValue("@EmployeeName", employee.employeeName);
                cmd.Parameters.AddWithValue("@DateOfBirth", employee.dateOfBirth);
                cmd.Parameters.AddWithValue("@PhoneNumber", employee.phoneNumber);
                cmd.Parameters.AddWithValue("@Email", employee.email);
                connection.Open();
                i = cmd.ExecuteNonQuery();
                connection.Close();


            }
            if (i > 0)
            {
                return true;


            }
            else
            {
                return false;
            }
        }
        public string DeleteEmployee(int EmployeeID)
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection(con))

            {
                SqlCommand cmd = new SqlCommand("ssp_deleteEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@employeeID", EmployeeID);
                cmd.Parameters.Add("@Returnmessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                cmd.ExecuteNonQuery();
                result = cmd.Parameters["@Returnmessage"].Value.ToString();
                connection.Close();

            }
            return result;
        }



    }
}