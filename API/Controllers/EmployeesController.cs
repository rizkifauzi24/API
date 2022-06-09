using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.Models.ViewModel;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        private readonly MyContext context;
        public EmployeesController(EmployeeRepository employeeRepository, MyContext myContext) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this.context = myContext;
        }

        [HttpGet("FirstOrDefault/{FirstName}")]
        public ActionResult GetFirstOrDefault(string FirstName)
        {

            var hasil = employeeRepository.GetFirstOrDefault(FirstName);

            if (hasil == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "DATA TIDAK DI TEMUKAN" });
            }
            else
            {
                return Ok(employeeRepository.GetFirstOrDefault(FirstName));
            }

        }

        [HttpGet("First/{FirstName}")]
        public ActionResult GetFirst(string FirstName)
        {
            var hasil = employeeRepository.GetFirst(FirstName);

            if (hasil == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "DATA TIDAK DITEMUKAN" });
            }
            else
            {
                return Ok(employeeRepository.GetFirst(FirstName));
            }
        }

        [HttpGet("Last/{FirstName}")]
        public ActionResult GetLast(string FirstName)
        {

            var hasil = employeeRepository.GetLast(FirstName);

            if (hasil == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "DATA TIDAK DI TEMUKAN" });
            }
            else
            {
                return Ok(hasil);
            }
        }

        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {

            if (employeeRepository.validateEmail(registerVM.Email))
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Email Telah Digunakan" });
            }

            if (employeeRepository.validatePhone(registerVM.Phone))
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Phone Telah Digunakan" });
            }

            var hasil = employeeRepository.Register(registerVM);

            if (hasil == 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "REGISTER GAGAL" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "REGISTER BERHASIL" });
            }


        }

        [HttpGet("testcors")]
        public ActionResult TestCORS()
        {
            return Ok("Test CORS berhasil");
        }
    }
}