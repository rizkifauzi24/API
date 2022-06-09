using API.Context;
using API.Models;
using API.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public IConfiguration _configuration;
        public AccountRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.myContext = myContext;
            this._configuration = configuration;
        }

        //public int Login(LoginVM loginVM)
        //{
        //    var res = myContext.Employees.FirstOrDefaultAsync(e => e.Email == loginVM.Email);

        //    if (res == null)
        //    {
        //        return 100;
        //    }
        //    else
        //    {
        //        var check = (from e in myContext.Employees
        //                     join a in myContext.Accounts
        //                     on e.NIK equals a.NIK
        //                     where e.Email == loginVM.Email
        //                     select a).FirstOrDefault();
        //        var check_pass = ValidatePassword(loginVM.Password, check.Password);

        //        if (check_pass == false)
        //        {
        //            return 200;
        //        } else {
        //            return 300;
        //        }
        //    }
        //}

        public int Login(LoginVM loginVM, out string Token)
        {
            if (ValidateEmail(loginVM.Email))
            {
                var password = (from e in myContext.Employees
                                join a in myContext.Accounts
                                on e.NIK equals a.NIK
                                where e.Email == loginVM.Email
                                select a.Password).FirstOrDefault();

                var checkPass = ValidatePassword(loginVM.Password, password);

                if (checkPass != false)
                {
                    
                    var CheckRole = (from e in myContext.Employees
                                     join a in myContext.AccountRole
                                     on e.NIK equals a.AccountId
                                     join r in myContext.Role
                                     on a.RoleId equals r.Id
                                     where e.Email == loginVM.Email
                                     select r).ToList();

                    var claims = new List<Claim>();
                    claims.Add(new Claim("Email", loginVM.Email));
                    foreach (var roles in CheckRole)
                    {
                        claims.Add(new Claim("roles", roles.Name));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["JwtContants:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn
                        );

                    var idToken = new JwtSecurityTokenHandler().WriteToken(token);
                    claims.Add(new Claim("Token Security", idToken.ToString()));

                    Token = idToken;
                    return 200;
                }

                Token = null;
                return 404;
            }

            Token = null;
            return 400;
        }


        public bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }



        public bool ValidateEmail(string Email)
        {
            var emp = myContext.Employees.FirstOrDefault(emp => emp.Email == Email);

            return emp != null;
        }



        //FORGOT PASSWORD
        public string getOTP()
        {
            // generate random OTP
            var chars1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            var stringChars1 = new char[6];
            var random1 = new Random();

            for (int i = 0; i < stringChars1.Length; i++)
            {
                stringChars1[i] = chars1[random1.Next(chars1.Length)];
            }

            var rand_str = new String(stringChars1);
            // end of generate random OTP
            return rand_str;
        }

        public void sendEmail(ForgotVM forgotVM, string otp)
        {
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("c5de1dac4b3759", "4ab72482c1504e"),
                EnableSsl = true
            };
            //client.Send("from@example.com", loginVM.Email, "Kode OTP", "Kode OTP anda adalah : " + getOTP(loginVM));
            MailMessage message = new MailMessage("test666@gmail.com", forgotVM.Email);
            message.Subject = "Test OTP";
            message.Body = "Nomor OTP Anda Adalah : " + otp;
            message.IsBodyHtml = true;
            client.Send(message);
        }

        public int ForgotPassword(ForgotVM forgotVM)
        {
            if (!ValidateEmail(forgotVM.Email))
            {
                return -1;
            } else
            {
                string otp = getOTP();

                Account account = (from e in myContext.Employees
                                   join a in myContext.Accounts
                                   on e.NIK equals a.NIK
                                   where e.Email == forgotVM.Email
                                   select a).FirstOrDefault();

                account.isActive = false;
                account.OTP = otp;
                account.ExpiredTime = DateTime.Now.AddMinutes(5);
                myContext.Entry(account).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                sendEmail(forgotVM, otp);
                return result;
            }
        }



        //CHANGE PASSWORD
        public string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public int ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var account = (from e in myContext.Employees
                           join a in myContext.Accounts
                           on e.NIK equals a.NIK
                           where e.Email == changePasswordVM.Email
                           select a).FirstOrDefault();

            var checkEmail = myContext.Employees.FirstOrDefault(e => e.Email == changePasswordVM.Email);

            if (checkEmail == null)
            {
                return -1;
            }
            else if (account.isActive)
            {
                return -2;
            }
            else if (DateTime.Now > account.ExpiredTime)
            {
                return -3;
            }
            else if (account.OTP != changePasswordVM.Otp)
            {
                return -4;
            }
            else
            {
                account.isActive = true;
                account.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordVM.New_Password, GetRandomSalt());

                myContext.Entry(account).State = EntityState.Modified;
                var result = myContext.SaveChanges();

                return result;
            }
        }

        public IEnumerable<GetRegisterVM> GetRegister()
        {
            var getRegister = (from e in myContext.Employees
                                  //join a in myContext.Accounts on e.NIK equals a.NIK
                              join p in myContext.Profillings on e.NIK equals p.NIK
                              join edu in myContext.Educations on p.Education.Id equals edu.Id
                              join u in myContext.Universities on edu.University.Id equals u.Id
                              join ar in myContext.AccountRole on e.NIK equals ar.AccountId
                              join r in myContext.Role on ar.RoleId equals r.Id
                              
                              
                              select new GetRegisterVM()
                              {
                                  NIK = e.NIK,
                                  FullName = String.Format("{0} {1}", e.FirstName, e.LastName),
                                  Phone = e.Phone,
                                  BirdthDate = e.BirdthDate,
                                  Salary = e.Salary,
                                  Email = e.Email,
                                  Gender = e.Gender.ToString(),
                                  Degree = edu.Degree.ToString(),
                                  GPA = edu.GPA,
                                  University = u.Name,
                                  Role = r.Name
                              }).ToList();

            return getRegister;

        }
    }
}
