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

using log4net;


namespace Breadcrumb.API.Controllers
{
    //[Authorize]
    [ApiController]
    public class TestController : ControllerBase
    {
        ILog log4Net;
        ValidationResult ValidationResult;
        public TestController()
        {
            log4Net = this.Log<TestController>();
            ValidationResult = new ValidationResult();
        }
        [HttpGet]
        [Route(APIEndpoint.DefaultRoute)]
        public ActionResult Get(int page = 1, int itemsPerPage = 100, string orderBy = null)
        {
            return Ok(new { page, itemsPerPage, orderBy });
        }
    }
}
