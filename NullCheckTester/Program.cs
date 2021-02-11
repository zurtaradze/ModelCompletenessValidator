using MCV;
using System.Collections.Generic;

namespace NullCheckTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var employee = new Employee();

            var obj = new Company()
            {
                Id = 1,
                Employee = employee
            };

            //employee.Company = obj;
            employee.Id = 2;

            var result = obj.HasNullOrEmptyProperties();

            System.Console.WriteLine(result);
        }
    }

    class Company
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
    }

    class Employee
    {
        public int Id { get; set; }
        public Company Company { get; set; }
    }
}
