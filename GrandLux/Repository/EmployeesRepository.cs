using GrandLux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandLux.Repository
{
    public class EmployeesRepository
    {
        GrandLuxDBContext _context = new GrandLuxDBContext();
        List<Employees> employees;

        public EmployeesRepository()
        {
            employees = _context.Employees.ToList();
        }

        public IEnumerable<Employees> GetEmployees()
        {
            return employees;
        }
    }
}
