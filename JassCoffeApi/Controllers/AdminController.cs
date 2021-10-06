using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace JassCoffeApi.Controllers
{

    [Route("/api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
       
        [HttpPost]
        public IActionResult Auth(string hash)
        {
            var identity = AuthOptions.GetIdentity(hash);
            if (identity==null)
            {
                return StatusCode((int)HttpStatusCode.Forbidden);
            }
            var now = DateTime.UtcNow;
            Console.WriteLine(now);
            Console.WriteLine(now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)));
            var jwt = new JwtSecurityToken(
                claims:identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials:new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return Ok(JsonSerializer.Serialize(encodedJwt));
        }

        [Authorize]
        [HttpGet]
        [Route("/api/admin/join")]
        public IActionResult Join()
        {
            return Ok();
        }
       
    }
}
