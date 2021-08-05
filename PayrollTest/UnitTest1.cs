using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayRollAdo.net;
using System;

namespace PayrollTest
{
    [TestClass]
    public class UnitTest1
    {
        EmployeeRepository repository;
        EmployeeModel model;
        EmployeeRepoER erRepo;
        PayrollTransactions transations;

        /// <summary>
        /// Method to initialize objects
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            repository = new EmployeeRepository();
            model = new EmployeeModel();
            erRepo = new EmployeeRepoER();
            transations = new PayrollTransactions();
        }
        /// <summary>
        /// Methods to test stored procedure update
        /// </summary>
        [TestMethod]
        public void UpdateDataUsingStoredProcedure()
        {
            try
            {
                string actual, expected;
                //Setting values to model object
                model.empId = 1;
                model.name = "Diwakar";
                model.basicPay = 75000;
                //Expected
                expected = "Updated 1 rows";
                actual = repository.UpdateSalaryUsingStoredProcedure(model);
                Assert.AreEqual(actual, expected);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }
        /// <summary>
        /// Test method to retreive data usin name
        /// </summary>
        [TestMethod]
        public void TestForRetrieveUsingName()
        {
            try
            {
                string actual, expected;
                model.name = "Diwakar";
                expected = "Success";
                actual = repository.RetreiveDataBasedOnName(model);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Method to retrive based on range
        /// </summary>
        [TestMethod]
        public void TestForRetrieveUsingRange()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = repository.RetriveDataBasedOnRange(model);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// Test method for aggregate functions group by male 
        /// </summary>
        [TestMethod]
        public void TestForAggregateFunctionsMale()
        {
            try
            {
                string actual, expected;
                expected = $"Total Salary = 177000\n Max Salary = 75000\n Min Salary = 30000\n Avg Salary = 42250\n Gender = M \n Count = 4\n";
                actual = repository.AggregateFunctions("M");
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// Test method for aggregate functions group by female 
        /// </summary>
        [TestMethod]
        public void TestForAggregateFunctionsFemale()
        {
            try
            {
                string actual, expected;
                expected = $"Total Salary = 10000\n Max Salary = 10000\n Min Salary = 10000\n Avg Salary = 10000\n Gender = F \n Count = 1\n";
                actual = repository.AggregateFunctions("F");
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Test method for retrieve all data based on er
        /// </summary>
        [TestMethod]
        public void TestForRetrieveDataEr()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = erRepo.RetriveAllDataER(model);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// test for Update data based on er
        /// </summary>
        [TestMethod]
        public void TestForUpdateEr()
        {
            try
            {
                string actual, expected;
                expected = "Updated";
                actual = erRepo.UpdateDetailsER(model);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Reteive based on range er
        /// </summary>
        [TestMethod]
        public void TestForRetrieveUsingRangeEr()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = erRepo.RetreiveBasedOnRangeEr(model);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// test aggregate function based on er male
        /// </summary>
        [TestMethod]
        public void TestForAggregateFunctionsMaleEr()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = erRepo.AggregateFunctionsEr("M");
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// test aggregate function based on er female
        /// </summary>
        [TestMethod]
        public void TestForAggregateFunctionsFemaleEr()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = erRepo.AggregateFunctionsEr("F");
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        /// <summary>
        /// Test For Insert Into Table Transaction
        /// </summary>
        [TestMethod]
        public void TestForInsertIntoTableTransaction()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = transations.InsertIntoTableUsingTransaction();
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Test For Deelete cascade
        /// </summary>
        [TestMethod]
        public void TestForDeleteCascade()
        {
            try
            {
                string actual, expected;
                expected = "Update 1 rows";
                actual = transations.DeleteCascade();
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        /// <summary>
        /// Test For add column
        /// </summary>
        [TestMethod]
        public void TestForAddIsActiveColumn()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = transations.AddIsActive();
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// Test For update audit
        /// </summary>
        [TestMethod]
        public void TestForAudit()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = transations.ListForAudit(1);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Test For retreive all data transaction
        /// </summary>
        [TestMethod]
        public void TestForRetreiveAllDataTransaction()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = transations.RetriveAllData();
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


    }
}
