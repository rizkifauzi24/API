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

    public class AccountRoleController : ControllerBase
    {
        private readonly AccountRoleRepository accountRoleRepository;

        public AccountRoleController(AccountRoleRepository accountRoleRepository)
        {
            this.accountRoleRepository = accountRoleRepository;
        }

        [Authorize(Roles = "Director")]
        [HttpPost("SignManager")]

        public ActionResult SignManager(SignManagerVM signManager)
        {
            var result = accountRoleRepository.SignManager(signManager);

            if (result == 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, Message = "Sign Berhasil" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Sign Gagal" });
            }
        }
    }
}