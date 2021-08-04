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

        /// <summary>
        /// Method to initialize objects
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            repository = new EmployeeRepository();
            model = new EmployeeModel();
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
    }
}
