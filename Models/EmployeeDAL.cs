using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
namespace MVC_CRUD_Operation_Practice.Models
{
    public class EmployeeDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public EmployeeDAL()
        {
            con = new SqlConnection(Startup.ConnectionString);
        }

        // public List<Employee> Employee { get; private set; }

        public List<Employee> GetAllEmployee()
        {
            List<Employee> list = new List<Employee>();
            cmd = new SqlCommand("select * from Employee", con);
            con.Open();
            dr = cmd.ExecuteReader();
            list = ArrangeList(dr);
            con.Close();
            return list;
        }
        public int Save(Employee emp)
        {
            cmd = new SqlCommand("insert into Employee values(@EmployeeName,@EmployeeSalary,@EmployeeAddress,@EmployeeRole,@EmployeeCity)",con);
            cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
            cmd.Parameters.AddWithValue("@EmployeeSalary", emp.EmployeeSalary);
            cmd.Parameters.AddWithValue("@EmployeeAddress", emp.EmployeeAddress);
            cmd.Parameters.AddWithValue("@EmployeeRole", emp.EmployeeRole);
            cmd.Parameters.AddWithValue("@EmployeeCity", emp.EmployeeCity);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public List<Employee> ArrangeList(SqlDataReader dr)
        {
            List<Employee> list = new List<Employee>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Employee emp = new Employee();
                    emp.EmployeeId = Convert.ToInt32(dr["EmployeeId"]);
                    emp.EmployeeName = dr["EmployeeName"].ToString();
                    emp.EmployeeSalary = Convert.ToDecimal(dr["EmployeeSalary"]);
                    emp.EmployeeAddress = dr["EmployeeAddress"].ToString();
                    emp.EmployeeRole = dr["EmployeeRole"].ToString();
                    emp.EmployeeCity = dr["EmployeeCity"].ToString();
                    list.Add(emp);
                }
                return list;
            }
            else
            {
                return null;
            }
        }
        public Employee GetEmployeeByid(int id)
        {
            Employee employee= new Employee();
            cmd = new SqlCommand("select * from Employee where EmployeeId=@EmployeeId", con);
            cmd.Parameters.AddWithValue("@Employeeid", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    employee.EmployeeId = Convert.ToInt32(dr["EmployeeId"]);
                    employee.EmployeeName = dr["EmployeeName"].ToString();
                    employee.EmployeeSalary = Convert.ToDecimal(dr["EmployeeSalary"]);
                    employee.EmployeeAddress = dr["EmployeeAddress"].ToString();
                    employee.EmployeeRole = dr["EmployeeRole"].ToString();
                    employee.EmployeeCity = dr["EmployeeCity"].ToString();



                }

            }
            con.Close();
            return employee;
        }

        public int Update(Employee emp)
        {
            cmd = new SqlCommand("update Employee set EmployeeName=@EmployeeName,EmployeeSalary=@EmployeeSalary,EmployeeAddress=@EmployeeAddress,EmployeeRole=@EmployeeRole,EmployeeCity=@EmployeeCity where EmployeeId=@EmployeeId", con);
            cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
            cmd.Parameters.AddWithValue("@EmployeeSalary", emp.EmployeeSalary);
            cmd.Parameters.AddWithValue("@EmployeeAddress", emp.EmployeeAddress);
            cmd.Parameters.AddWithValue("@EmployeeRole", emp.EmployeeRole);
            cmd.Parameters.AddWithValue("@EmployeeCity", emp.EmployeeCity);
            cmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public int Delete(int id)
        {
            cmd = new SqlCommand("delete from Employee where EmployeeId=@EmployeeId", con);
            cmd.Parameters.AddWithValue("@EmployeeId", id);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
    }
}
