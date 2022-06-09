using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    interface IEmployeeRepository
    {
        IEnumerable<Employee> Get();

        Employee GetFirst(string FirstName);

        Employee GetFirstOrDefault(string FirstName);

        Employee GetSingle(string FirstName);

        Employee GetSingleOrDefault(string FirstName);

        Employee Get(string NIK);

        int Insert(Employee employee);

        int Update(Employee employee);

        int Delete(string NIK);
    }
}
