using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CnthEmployeesController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;

        public CnthEmployeesController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]

        public ActionResult Get()
        {

            var hasil = employeeRepository.Get();

            if (hasil == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "DATA KOSONG" });
            }
            else
            {
                return Ok(hasil);
            }
        }

        [HttpPost]
        public ActionResult Post(Employee employee)
        {
            var hasil = employeeRepository.Insert(employee);

            if (hasil == 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "INSERT GAGAL" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "INSERT BERHASIL" });
            }
        }

        [HttpPut]
        public ActionResult Put(Employee employee)
        {
            
            var hasil = employeeRepository.Update(employee);

            if (hasil == 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "UPDATE GAGAL" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "UPDATE BERHASIL" });
            }
        }

        [HttpDelete("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            var getNik = employeeRepository.Get(NIK);
            if (getNik == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "DATA TIDAK DITEMUKAN" });
            }

            var del = Ok(employeeRepository.Delete(NIK));
            if(del == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "DELETE GAGAL" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "DELETE BERHASIL" });
            }
        }

        [HttpGet("{NIK}")]
        public ActionResult Get(string NIK)
        {

            var hasil = employeeRepository.Get(NIK);

            if (hasil == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "DATA TIDAK DI TEMUKAN" });
            }
            else
            {
                return Ok(employeeRepository.Get(NIK));
                
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

        [HttpGet("Single/{FirstName}")]
        public ActionResult GetSingle(string FirstName)
        {
            var hasil = employeeRepository.GetSingle(FirstName);

            if (hasil == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "DATA TIDAK DITEMUKAN" });
            }
            else
            {
                return Ok(employeeRepository.GetSingle(FirstName));
            }
        }

        [HttpGet("SingleOrDefault/{FirstName}")]
        public ActionResult GetSingleOrDefault(string FirstName)
        {
            var hasil = employeeRepository.GetSingleOrDefault(FirstName);

            if (hasil == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "DATA TIDAK DITEMUKAN" });
            }
            else
            {
                return Ok(employeeRepository.GetSingleOrDefault(FirstName));
            }
        }


    }
}