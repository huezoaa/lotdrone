using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace lotdrone.Models
{
    public class Employee
    {
        public int EmployeeID {get; set;}
        public string LastName { get; set; }
        public string FirstName { get; set; }
       [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public int Extension { get; set; }

    }
}