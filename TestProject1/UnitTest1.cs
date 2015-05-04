using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using lotdrone;
using Telerik.JustMock;
using lotdrone.Controllers;
using lotdrone.Models;

namespace TestProject1
{
    [TestClass]
    public class EmployeesControllerTest
    {
        [TestMethod]
        public void Index_Returns_All_Employees_In_DB()        
        {
            //Arrange
            var employeeRepository = Mock.Create<EmployeesRepository>(); //AH:  instantiate employees repository.  Will be used as db for test.
            Mock.Arrange(() => employeeRepository.GetAll()). //AH: Mock the Employees table
                Returns(new List<Employee>()
                {
                    new Employee { EmployeeID = 1, FirstName = "Angel", LastName = "Huezo", Email = "angelohuezo@gmail.com", Extension = 1001 },
                    new Employee { EmployeeID = 2, FirstName = "Ed", LastName = "Toro", Email = "eddroid@wyncode.com", Extension = 1002},

                }).MustBeCalled();

            //Act
  //**          EmployeesController controller = new EmployeesController(employeeRepository); //AH: instantiate an EmployeesController with our repository data
     //**       ViewResult viewResult = controller.Index(); //AH:  Finally testing the Index() function of the Employees Controller
    //**        var model = viewResult.Model as IEnumerable<Employee>;

            //Assert
     //**       Assert.AreEqual(2, model.Count());
        }
    }
}
