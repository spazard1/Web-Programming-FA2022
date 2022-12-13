using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using FinalExam.Services;
using FinalExam.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using FinalExam.Filters;

namespace FinalExam.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2022-04-02")]
    [TypeFilter(typeof(RequestIdFilter))]
    public class LoginController : Controller
    {
        private readonly ISecurityProvider securityProvider;

        private UsersDatabase usersModel;

        public LoginController(ISecurityProvider securityProvider, UsersDatabase usersModel)
        {
            this.securityProvider = securityProvider;
            this.usersModel = usersModel;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserModel userModel)
        {
            if (!usersModel.ValidatePassword(userModel.Username, userModel.Password))
            {
                return new StatusCodeResult((int)HttpStatusCode.Unauthorized);
            }

            var claims = new List<Claim>
            {
                new Claim("username", userModel.Username),
                new Claim("password", userModel.Password),
                new Claim("create_time", DateTime.Now.ToString())
            };

            return Json(new { token = this.securityProvider.GetToken(claims) });
        }

        [HttpPut]
        public IActionResult AllUsers()
        {
            return Json(this.usersModel.AllUsernames);
        }
    }
}
