using System;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Breadcrumb.Manager.Interface;
using Breadcrumb.Model;
using Breadcrumb.Utility;
using Microsoft.AspNetCore.Http;
using NuGet.Protocol.Plugins;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Hosting;

using log4net;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Breadcrumb.API.Controllers
{
    [ApiController]
    public class TokenController : ControllerBase
    {
        ILog log4Net;
        ValidationResult ValidationResult;
        public IConfiguration Configuration { get; }
        IWebHostEnvironment Env { get; }
        public string ContentRootPath { get; set; }
        IHttpContextAccessor HttpContextAccessor { get; set; }

        public TokenController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
        {
            log4Net = this.Log<TokenController>();
            ValidationResult = new ValidationResult();
            Configuration = configuration;
            Env = env;
            HttpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("/api/CreateToken")]
        public ActionResult CreateToken(TokenModel model)
        {
            var jwtSection = Configuration.GetSection("Jwt");
            var Secret = jwtSection.GetValue<String>("Secret");
            var ValidIssuer = jwtSection.GetValue<String>("ValidIssuer");
            var ValidAudience = jwtSection.GetValue<String>("ValidAudience");


            var claims = new[]
            {
                new Claim("DatabaseType", model.DatabaseType),
                new Claim("ConnectionString", model.ConnectionString),
                new Claim("SheetId", model.SheetId)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(issuer: ValidIssuer, audience: ValidAudience, claims: claims, expires: DateTime.Now.AddYears(1), signingCredentials: signinCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return Ok(new APIResponse(ResponseCode.SUCCESS,"Token Generated", tokenString));
        }
    }
}
