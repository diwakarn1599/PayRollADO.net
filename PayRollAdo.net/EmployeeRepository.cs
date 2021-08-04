using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PayRollAdo.net
{
    public class EmployeeRepository
    {
        //Connection String
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog =Payroll_Services ; Integrated Security = True;";

        //SqlConnection
        SqlConnection connection = new SqlConnection(connectionString);

        //RetriveAllData
        public string RetriveAllEmployeeData(EmployeeModel model)
        {
            string output = string.Empty;
            try
            {
               //Qurey to retreive data
                string query = "SELECT * FROM employee_payroll";
                SqlCommand command = new SqlCommand(query, connection);
                //Open Connection
                this.connection.Open();
                //Returns object of result set
                SqlDataReader result = command.ExecuteReader();
                //Check Result set has rows or not
                if (result.HasRows)
                {
                    //Parse untill  rows are null
                    while (result.Read())
                    {
                        model.empId = Convert.ToInt32(result["empId"]);
                        model.name = Convert.ToString(result["name"]);
                        model.basicPay = Convert.ToDouble(result["BasicPay"]);
                        model.startDate = (DateTime)result["startDate"];
                        model.emailId = Convert.ToString(result["emailId"]);
                        model.gender = Convert.ToChar(result["Gender"]);
                        model.department = Convert.ToString(result["Department"]);
                        model.phoneNumber = Convert.ToInt64(result["PhoneNumber"]);
                        model.address = Convert.ToString(result["Address"]);
                        model.deductions = Convert.ToDouble(result["Deductions"]);
                        model.taxablePay = Convert.ToDouble(result["TaxablePay"]);
                        model.incomeTax = Convert.ToDouble(result["IncomeTax"]);
                        model.netPay = Convert.ToDouble(result["NetPay"]);
                        Console.WriteLine($"{model.empId},{model.name},{model.basicPay},{model.startDate},{model.emailId},{model.gender},{model.department},{model.phoneNumber},{model.address},{model.deductions},{model.taxablePay},{model.incomeTax},{model.netPay}");

                    }
                    output = "Success";
                }
                else
                {
                    //Console.WriteLine("No Records in the table");
                    output = "Success";
                }
                //close result set
                result.Close();
            }
            catch(Exception ex)
            {
                //handle exception
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //close connection
                connection.Close();
            }
            return output;

        }

        public string UpdateSalary(EmployeeModel model)
        {
            string output = string.Empty;
            try
            {
                //Qurey to retreive data
                string query = "UPDATE employee_payroll set BasicPay=30000 WHERE name='Diwakar';";
                SqlCommand command = new SqlCommand(query, connection);
                //Open Connection
                this.connection.Open();
                //Returns numbers of rows updated
                int result = command.ExecuteNonQuery();
                //Check Result set is greater or equal to 1
                if (result>=1)
                {
                    output="Updated";
                   
                }
                else
                {
                    output="Not Updated";
                }
                
            }
            catch (Exception ex)
            {
                //handle exception
                //Console.WriteLine(ex.Message);
                output = ex.Message;
            }
            finally
            {
                //close connection
                connection.Close();
            }
            return output;

        }


        public string UpdateSalaryUsingStoredProcedure(EmployeeModel model)
        {
            string output = string.Empty;
            try
            {
                using(this.connection)
                {
                    //sqlcommand object with stored procedure - dbo.UpdateDetails
                    SqlCommand command = new SqlCommand("dbo.UpdateDetails", connection);
                    //Setting command type
                    command.CommandType = CommandType.StoredProcedure;
                    //Adding values to stored procedures parameters
                    command.Parameters.AddWithValue("@id", model.empId);
                    command.Parameters.AddWithValue("@name", model.name);
                    command.Parameters.AddWithValue("@Base_pay", model.basicPay);
                    // Opening connection 
                    connection.Open();
                    //Executing using non query returns number of rows affected
                    int res = command.ExecuteNonQuery();

                    if (res >= 1)
                    {
                        output = $"Updated {res} rows";

                    }
                    else
                    {
                        output = "Not Updated";
                    }

                }
                return output;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return output;
            }
            finally
            {
                //closing the connection
                connection.Close();
            }
        }
    }
}
