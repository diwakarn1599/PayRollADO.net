using System;

namespace PayRollAdo.net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("PayRoll ADO .net");
            EmployeeRepository repository = new EmployeeRepository();
            EmployeeModel model = new EmployeeModel();
            //repository.RetriveAllEmployeeData(model);
            repository.UpdateSalary(model);
        }
    }
}
