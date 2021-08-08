using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace PayRollAdo.net
{
    public class PayrollTransactions
    {
        //Connection String
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog =Payroll_Services ; Integrated Security = True;";

        //SqlConnection
        SqlConnection connection = new SqlConnection(connectionString);

        /// <summary>
        /// Method Insert Into Table Using Transaction
        /// </summary>
        /// <returns></returns>
        public string InsertIntoTableUsingTransaction()
        {
            string output = string.Empty;
            using (connection)
            {
                //open the connection
                connection.Open();
                //Begin the transactions
                SqlTransaction transaction = connection.BeginTransaction();
                //Create the commit
                SqlCommand command = connection.CreateCommand();
                //Set command to transaction
                command.Transaction = transaction;

                try
                {
                    //set command text to command object
                    command.CommandText = @"INSERT INTO Employee VALUES (1,'Ashwin',9876543210,'kochin','2021-04-02','M');";
                    //Execute command
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO Payroll(EmpId,BasicPay) VALUES (5,82000);";
                    command.ExecuteNonQuery();
                    command.CommandText = @"UPDATE Payroll SET Deductions =(BasicPay*20)/100 WHERE EmpId=5;";
                    command.ExecuteNonQuery();
                    command.CommandText = @"UPDATE Payroll SET TaxablePay =(BasicPay-Deductions) WHERE EmpId=5;";
                    command.ExecuteNonQuery();
                    command.CommandText = @"UPDATE Payroll SET IncomeTax =(TaxablePay*10)/100 WHERE EmpId=5;";
                    command.ExecuteNonQuery();
                    command.CommandText = @"UPDATE Payroll SET NetPay =(BasicPay-IncomeTax) WHERE EmpId=5;";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO EmpDept VALUES(5,2);";
                    command.ExecuteNonQuery();
                    //if all executes are success commit the transaction
                    transaction.Commit();
                    output = "Success";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //If any error or exception occurs rollback the transaction
                    transaction.Rollback();
                    output = "Unsuccessfull";
                }
                finally
                {
                    //close the connection
                    if (connection != null)
                        connection.Close();
                }
                return output;
            }
        }
        /// <summary>
        /// Delete cascade
        /// </summary>
        /// <returns></returns>
        public string DeleteCascade()
        {
            string output = string.Empty;
            using (connection)
            {
                //open the connection
                connection.Open();
                //Begin the transactions
                SqlTransaction transaction = connection.BeginTransaction();
                //Create the commit
                SqlCommand command = connection.CreateCommand();
                //Set command to transaction
                command.Transaction = transaction;

                try
                {
                    //set command text to command object
                    command.CommandText = @"DELETE FROM Employee WHERE EmpId=5";
                    //Execute command
                    int x = command.ExecuteNonQuery();
                   
                    //if all executes are success commit the transaction
                    transaction.Commit();
                    output = $"Update {x} rows";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //If any error or exception occurs rollback the transaction
                    transaction.Rollback();
                    output = "Unsuccessfull";
                }
                finally
                {
                    //close the connection
                    if (connection != null)
                        connection.Close();
                }
                return output;
            }


        }

        /// <summary>
        /// Add A column in table
        /// </summary>
        /// <returns></returns>
        public string AddIsActive()
        {
            string output = string.Empty;
            using (connection)
            {
                //open the connection
                connection.Open();
                //Begin the transactions
                SqlTransaction transaction = connection.BeginTransaction();
                //Create the commit
                SqlCommand command = connection.CreateCommand();
                //Set command to transaction
                command.Transaction = transaction;

                try
                {
                    //set command text to command object
                    command.CommandText = @"ALTER TABLE Employee ADD IsActive int NOT NULL default 1;";
                    //Execute command
                    int x = command.ExecuteNonQuery();

                    //if all executes are success commit the transaction
                    transaction.Commit();
                    output = $"Success";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //If any error or exception occurs rollback the transaction
                    transaction.Rollback();
                    output = "Unsuccessfull";
                }
                finally
                {
                    //close the connection
                    if (connection != null)
                        connection.Close();
                }
                return output;
            }
        }

        /// <summary>
        /// Update is active field
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ListForAudit(int id)
        {
            string output = string.Empty;
            using (connection)
            {
                //open the connection
                connection.Open();
                //Begin the transactions
                SqlTransaction transaction = connection.BeginTransaction();
                //Create the commit
                SqlCommand command = connection.CreateCommand();
                //Set command to transaction
                command.Transaction = transaction;

                try
                {
                    //set command text to command object
                    command.CommandText = @$"UPDATE Employee SET IsActive=0 WHERE EmpId={id}";
                    //Execute command
                    int x = command.ExecuteNonQuery();

                    //if all executes are success commit the transaction
                    transaction.Commit();
                    output = $"Success";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //If any error or exception occurs rollback the transaction
                    transaction.Rollback();
                    output = "Unsuccessfull";
                }
                finally
                {
                    //close the connection
                    if (connection != null)
                        connection.Close();
                }
                return output;
            }
        }

        /// <summary>
        /// Retrive All data from table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string RetriveAllData()
        {
            string output = string.Empty;
            try
            {
                //Qurey to retreive data
                string query = @"SELECT c.CompanyId,c.CompanyName,emp.IsActive,emp.EmpId,emp.EmpName,emp.PhoneNumber,emp.StartDate,emp.Gender,emp.EmpAddress,
                                p.BasicPay,p.TaxablePay,p.IncomeTax,p.NetPay,p.Deductions,d.DeptName
                                FROM Company AS c
                                INNER JOIN Employee AS emp ON c.CompanyId=emp.CompanyId AND emp.IsActive=1
                                INNER JOIN Payroll AS p ON p.EmpId = emp.EmpId
                                INNER JOIN EmpDept ON EmpDept.EmpId = emp.EmpId
                                INNER JOIN Department as d ON d.DeptId = EmpDept.DeptId;";
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
                        //Print deatials that are retrived
                        PrintDetails(result);

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
            catch (Exception ex)
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
        /// <summary>
        /// method to record time to retreive data without using thread
        /// </summary>
        /// <returns></returns>
        public string RetreiveWithoutUsingThread()
        {
            string output = string.Empty;
            try
            {
                //object for stopwatch
                Stopwatch stopWatch = new Stopwatch();
                //start the stopwatch
                stopWatch.Start();
                RetriveAllData();
                //stop stopwatch
                stopWatch.Stop();
                Console.WriteLine($"Duration : {stopWatch.ElapsedMilliseconds} milliseconds");
                output = "success";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                output = "unsuccessfull";
            }
            return output;
        }
        /// <summary>
        /// Method to retrreive data using threads ==> tasks
        /// </summary>
        /// <returns></returns>
        public string RetreiveUsingThread()
        {
            string output = string.Empty;
            try
            {
                //object for stopwatch
                Stopwatch stopWatch = new Stopwatch();
                //start the stopwatch
                stopWatch.Start();
                RetriveAllData();
                //stop stopwatch
                stopWatch.Stop();
                Console.WriteLine($"Duration : {stopWatch.ElapsedMilliseconds} milliseconds");
                output = "success";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                output = "unsuccessfull";
            }
            return output;
        }
        /// <summary>
        /// Print details
        /// </summary>
        /// <param name="result"></param>
        /// <param name="model"></param>
        public void PrintDetails(SqlDataReader result)
        {
            List<EmployeeModel> listOfEmployees = new List<EmployeeModel>();
            try
            {
                EmployeeModel model = new EmployeeModel();
                //reatreive adata and print details
                model.empId = Convert.ToInt32(result["EmpId"]);
                model.name = Convert.ToString(result["EmpName"]);
                model.basicPay = Convert.ToDouble(result["BasicPay"]);
                model.startDate = (DateTime)result["StartDate"];
                model.gender = Convert.ToChar(result["Gender"]);
                model.department = Convert.ToString(result["DeptName"]);
                model.phoneNumber = Convert.ToInt64(result["PhoneNumber"]);
                model.address = Convert.ToString(result["EmpAddress"]);
                model.deductions = Convert.ToDouble(result["Deductions"]);
                model.taxablePay = Convert.ToDouble(result["TaxablePay"]);
                model.incomeTax = Convert.ToDouble(result["IncomeTax"]);
                model.netPay = Convert.ToDouble(result["NetPay"]);
                model.companyId = Convert.ToInt32(result["CompanyId"]);
                model.companyName = Convert.ToString(result["CompanyName"]);
                model.isActive = Convert.ToInt32(result["IsActive"]);
                //creating the new tasks to add to list
                Task task = new Task(() =>
                {
                    listOfEmployees.Add(model);
                    Console.WriteLine($"{model.isActive},{model.empId},{model.name},{model.basicPay},{model.startDate},{model.gender},{model.department},{model.phoneNumber},{model.address},{model.deductions},{model.taxablePay},{model.incomeTax},{model.netPay},{model.companyId},{model.companyName}\n");
                });
                //start the task\
                task.Start();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
