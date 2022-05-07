using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebApplicationEmp.Controllers;
using WebApplicationEmp.Model;
using WebApplicationEmp.Service;

namespace empApiTest
{
    public class Tests
    {
        private readonly Mock<IEmployeeService> _employeeServicesMock;
        private List<Employee> _employees;
        private readonly EmployeesController _employeeController;

        public Tests()
        {
            _employeeServicesMock = new Mock<IEmployeeService>();
            _employeeController = new EmployeesController(_employeeServicesMock.Object);
        }

        [SetUp]
        public void Setup()
        {
            _employees = new List<Employee>
           {
               new Employee()
               {
        Id  = "1",

        Name="R1",

        Address ="tnj",
        Phone ="123",

        Email="R1@ga.com",
    },
              new Employee()
               {
        Id  = "2",

        Name="v1",

        Address ="tnj",
        Phone ="1456",

        Email="v2@ga.com",
    },

           };
        }

        [TestCase("1")]
        public void GetEmployeeSuccessTest(string id)
        {
            _employeeServicesMock.Setup<Employee>(x => x.Get(id))
             .Returns(_employees.FirstOrDefault(product => product.Id == id));

            var result = _employeeController.Get(id) as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode.Value);
        }

        [TestCase("4")]
        public void GetProductFailTest(string id)
        {
            _employeeServicesMock.Setup(x => x.Get(id))
                .Returns(_employees.FirstOrDefault(product => product.Id == id));

            var result = _employeeController.Get(id) as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode.Value);
        }


    }
}