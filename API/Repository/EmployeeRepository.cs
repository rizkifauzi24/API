using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly MyContext context;

        public EmployeeRepository(MyContext context)
        {
            this.context = context;
        }
        public int Delete(string NIK)
        {
            var entity = context.Employees.Find(NIK);
            context.Remove(entity);
            var result = context.SaveChanges();
            return result;
        }

        public IEnumerable<Employee> Get()
        {
            return context.Employees.ToList();
        }

        public Employee Get(string NIK)
        {
            return context.Employees.Find(NIK);
        }

        public int Insert(Employee employee)
        {
            context.Employees.Add(employee);
            var result = context.SaveChanges();
            return result;
        }

        public int Update(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result;
        }

        public Employee GetFirst(string FirstName)
        {
           

            try
            {
                return context.Employees.First(f => f.FirstName == FirstName);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Employee GetFirstOrDefault(string FirstName)
        {
            return context.Employees.FirstOrDefault(f => f.FirstName == FirstName);
        }

        public Employee GetSingle(string FirstName)
        {
            return context.Employees.Single(s => s.FirstName == FirstName);
        }

        public Employee GetSingleOrDefault(string FirstName)
        {
            return context.Employees.SingleOrDefault(f => f.FirstName == FirstName);
        }

    }
}
