using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Models;
using API.Models.ViewModel;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public AccountsController(AccountRepository accountRepository) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [HttpPost("Login")]

        public ActionResult Login(LoginVM loginVM)
        {
            string Token;
            int TokenLogin = accountRepository.Login(loginVM, out Token);

            if (accountRepository.Login(loginVM, out Token) == 200)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, Token, message = "LOGIN BERHASIL" });
            }
            else if (accountRepository.Login(loginVM, out Token) == 404)
            {
                return StatusCode(404, new { status = HttpStatusCode.BadRequest, Token, message = "Password Salah" });
            }
            else 
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Token, message = "Email Salah" });
            }


            //var checkEmail = accountRepository.checkEmail(loginVM);
            //if (checkEmail == false)
            //{
            //    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Email Tidak Terdaftar" });
            //}


            //var password = accountRepository.checkPassword(loginVM);

            //if (password == 0)
            //{
            //    return StatusCode(200, new { status = HttpStatusCode.OK, message = "LOGIN BERHASIL" });
            //}
            //else
            //{
            //    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "PASSWORD SALAH" });
            //}
        }

        [HttpPost]
        [Route("Forgot")]

        public ActionResult ForgotPassword(ForgotVM forgotVM)
        {
            var result = accountRepository.ForgotPassword(forgotVM);

            if (result == -1)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Email Tidak Terdaftar" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, Message = "Kode OTP Sudah Dikirim" });
            }
        }

        [HttpPost]
        [Route("ChangePassword")]

        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var result = accountRepository.ChangePassword(changePasswordVM);


            if (result == -1)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Email Salah!" });
            }
            else if (result == -2)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "OTP Telah Aktif" });
            }
            else if (result == -3)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Kode OTP Telah Kadaluarsa" });
            }
            else if (result == -4)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Kode OTP Salah!" });
            }
            else if (result > 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, Message = "Password Berhasil Di Ganti" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Password Gagal Di Ganti" });
            }
        }

        //[Authorize(Roles = "Employee")]
        [Authorize]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT berhasil");
        }

        [Authorize(Roles = "Director, Manager")]
        [HttpGet]
        [Route("GetRegister")]
        public IEnumerable<GetRegisterVM> GetRegister()
        {
            return accountRepository.GetRegister().ToList();
        }
    }
}