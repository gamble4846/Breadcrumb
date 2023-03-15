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
using Breadcrumb.Manager.Impl;
using System.Threading.Tasks;

namespace Breadcrumb.API.Controllers
{
    [ApiController]
    public class TheMovieDBController : ControllerBase
    {
        ILog log4Net;
        ValidationResult ValidationResult;
        public IConfiguration Configuration { get; }
        IWebHostEnvironment Env { get; }
        public string ContentRootPath { get; set; }
        IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions CommonFunctions { get; set; }
        public ITheMovieDBManager TheMovieDBManager { get; set; }

        public TheMovieDBController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, ITheMovieDBManager theMovieDBManager)
        {
            log4Net = this.Log<TheMovieDBController>();
            ValidationResult = new ValidationResult();
            Configuration = configuration;
            Env = env;
            HttpContextAccessor = httpContextAccessor;
            CommonFunctions = new CommonFunctions(configuration, httpContextAccessor);
            TheMovieDBManager = theMovieDBManager;
        }


        [HttpGet]
        [Route("/api/TheMovieDB/TvShow/Get/{IMDBId}")]
        public async Task<ActionResult> GetTvshowByIMDBId(string IMDBId)
        {
            try
            {
                return Ok(await TheMovieDBManager.GetTvshowByIMDBId(IMDBId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpGet]
        [Route("/api/TheMovieDB/Movie/Get/{IMDBId}")]
        public async Task<ActionResult> GetMovieByIMDBId(string IMDBId)
        {
            try
            {
                return Ok(await TheMovieDBManager.GetMovieByIMDBId(IMDBId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
    }
}
