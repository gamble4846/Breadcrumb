using Breadcrumb.Manager.Interface;
using Breadcrumb.Utility;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Breadcrumb.API.Controllers
{
    [Authorize]
    [ApiController]
    public class GoogleApiController : ControllerBase
    {
        ILog log4Net;
        ValidationResult ValidationResult;
        public IConfiguration Configuration { get; }
        IWebHostEnvironment Env { get; }
        public string ContentRootPath { get; set; }
        IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions CommonFunctions { get; set; }
        public IGoogleApiManager GoogleApiManager { get; set; }

        public GoogleApiController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IGoogleApiManager googleApiManager)
        {
            log4Net = this.Log<GoogleApiController>();
            ValidationResult = new ValidationResult();
            Configuration = configuration;
            Env = env;
            HttpContextAccessor = httpContextAccessor;
            CommonFunctions = new CommonFunctions(configuration, httpContextAccessor);
            GoogleApiManager = googleApiManager;
        }

        [HttpGet]
        [Route("/api/GetFilesFromFolderId/{FolderID}")]
        public async Task<ActionResult> GetFilesFromFolderId(string FolderID)
        {
            try
            {
                return Ok(await GoogleApiManager.GetFilesFromFolderId(FolderID));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
    }
}
