using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

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
    }
}
