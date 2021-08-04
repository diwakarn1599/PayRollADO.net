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

        [TestInitialize]
        public void Setup()
        {
            repository = new EmployeeRepository();
            model = new EmployeeModel();
        }
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
    }
}
