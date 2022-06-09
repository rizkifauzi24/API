using API.Context;
using API.Models;
using API.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext context;

        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

        public Employee GetLast(string FirstName)
        {
            try
            {
                return context.Employees.OrderBy(item => item.FirstName == FirstName).Last();
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


        //REGISTER
        public int Register(RegisterVM registerVM)
        {
            Employee employee = new Employee();
            Account account = new Account();
            Education education = new Education();
            

            //Employee
            employee.NIK = GetAutoIncrement() ;
            employee.FirstName = registerVM.FirstName;
            employee.LastName = registerVM.LastName;
            employee.Phone = registerVM.Phone;
            employee.BirdthDate = registerVM.BirthDate;
            employee.Salary = registerVM.Salary;
            employee.Email = registerVM.Email;
            employee.Gender = (Gender)Enum.Parse(typeof(Gender),registerVM.Gender);

            //account
            account.Password = HashPassword(registerVM.Password);

            //role
            AccountRole accountRole = new AccountRole();
            accountRole.RoleId = "3";
            

            //education
            University uni = context.Universities.Find(registerVM.University_Id);
            education.Degree = (Degree)Enum.Parse(typeof(Degree), registerVM.Degree);
            education.GPA = registerVM.GPA;
            education.University = uni;

            //profilling
            Profilling profilling = new Profilling();
            profilling.Education = education;

            account.Profilling = profilling;
            employee.Account = account;
            accountRole.AccountId = employee.NIK;

            context.Add(accountRole);
            context.Add(employee);
            var result = context.SaveChanges();
            return result;

        }

        public string GetAutoIncrement()
        {
            Employee employee = new Employee();

            var count = (from s in context.Employees orderby s.NIK select s.NIK).LastOrDefault();

            int last_id;

            if (count == null)
            {
                last_id = 1;
            }
            else
            {
                last_id = Convert.ToInt32(count.Substring(count.Length - 4)) + 1;
            }

            string new_count;

            if (last_id < 10)
            {
                new_count = "000" + last_id;
            } else if(last_id < 100)
            {
                new_count = "00" + last_id;
            } else if(last_id < 1000)
            {
                new_count = "0" + last_id;
            } else
            {
                new_count = last_id.ToString();
            }

            var tgl = DateTime.Now.ToString("MMddyyyy") + new_count;

            return tgl;
        }

        public bool validateEmail(string Email)
        {
            var emp = context.Employees.FirstOrDefault(emp => emp.Email == Email);

            return emp != null;
        }

        public bool validatePhone(string Phone)
        {
            var emp = context.Employees.FirstOrDefault(emp => emp.Phone == Phone);

            return emp != null;
        }

        public string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

    }
}
