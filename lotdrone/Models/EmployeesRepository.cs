using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lotdrone.Models
{
    public interface EmployeesRepository
    {
        List<Employee> GetAll();
    }
}
