using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<Entity> Get()
        {
            

            var hasil = repository.Get();

            if (hasil == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "DATA KOSONG" });
            }
            else
            {
                return Ok(hasil);
            }
        }

        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            var hasil = repository.Get(key);

            if (hasil == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "DATA TIDAK DITEMUKAN" });
            }
            else
            {
                return Ok(repository.Get(key));

            }
        }

        [HttpPost]
        public ActionResult Insert(Entity entity)
        {
            var hasil = repository.Insert(entity);

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
        public ActionResult Update(Entity entity)
        {
            var hasil = repository.Update(entity);

            if (hasil == 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "UPDATE GAGAL" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "UPDATE BERHASIL" });
            }
        }

        [HttpDelete("{key}")]
        public ActionResult Delete(Key key)
        {
            

            var del = Ok(repository.Delete(key));
            if (del == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "DELETE GAGAL" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "DELETE BERHASIL" });
            }
        }


    }
}