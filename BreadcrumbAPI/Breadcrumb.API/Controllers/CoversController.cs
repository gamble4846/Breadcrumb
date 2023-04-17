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
using Breadcrumb.Model.tbCoversModels;

namespace Breadcrumb.API.Controllers
{
    [Authorize]
    [ApiController]
    public class CoversController : ControllerBase
    {
        ILog log4Net;
        ValidationResult ValidationResult;
        public IConfiguration Configuration { get; }
        IWebHostEnvironment Env { get; }
        public string ContentRootPath { get; set; }
        IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions CommonFunctions { get; set; }
        public ICoversManager CoversManager { get; set; }

        public CoversController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, ICoversManager coversManager)
        {
            log4Net = this.Log<CoversController>();
            ValidationResult = new ValidationResult();
            Configuration = configuration;
            Env = env;
            HttpContextAccessor = httpContextAccessor;
            CommonFunctions = new CommonFunctions(configuration, httpContextAccessor);
            CoversManager = coversManager;
        }

        [HttpGet]
        [Route("/api/Cover/{BreadId}")]
        public ActionResult GetCoverByBreadId(Guid BreadId)
        {
            try
            {
                return Ok(CoversManager.GetCoverByBreadId(BreadId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpPost]
        [Route("/api/Cover/Get")]
        public ActionResult GetCoverByBreadIds(List<Guid> BreadIds)
        {
            try
            {
                return Ok(CoversManager.GetCoverByBreadIds(BreadIds));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpPost]
        [Route("/api/InsertUpdateDeleteCovers/SingleBread/Post")]
        public ActionResult InsertUpdateDeleteCoversForSingleBread(List<tbCoversModel> coversData)
        {
            try
            {
                return Ok(CoversManager.InsertUpdateDeleteCoversForSingleBread(coversData));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
    }
}
