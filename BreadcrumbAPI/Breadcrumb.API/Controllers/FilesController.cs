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
using Breadcrumb.Manager.Impl;
using Breadcrumb.Model.GoogleApiModels;

namespace Breadcrumb.API.Controllers
{
    [Authorize]
    [ApiController]
    public class FilesController : ControllerBase
    {
        ILog log4Net;
        ValidationResult ValidationResult;
        public IConfiguration Configuration { get; }
        IWebHostEnvironment Env { get; }
        public string ContentRootPath { get; set; }
        IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions CommonFunctions { get; set; }
        public IFilesManager FilesManager { get; set; }

        public FilesController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IFilesManager filesManager)
        {
            log4Net = this.Log<FilesController>();
            ValidationResult = new ValidationResult();
            Configuration = configuration;
            Env = env;
            HttpContextAccessor = httpContextAccessor;
            CommonFunctions = new CommonFunctions(configuration, httpContextAccessor);
            FilesManager = filesManager;
        }

        [HttpPost]
        [Route("/api/Files/AddFinalFile")]
        public ActionResult AddFinalFiles(List<FinalFile> FinalFiles)
        {
            try
            {
                return Ok(FilesManager.AddFinalFiles(FinalFiles));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpGet]
        [Route("/api/Files/All")]
        public ActionResult GetFiles()
        {
            try
            {
                return Ok(FilesManager.GetFiles());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpGet]
        [Route("/api/Files/NotAssignedFiles")]
        public ActionResult GetNotAssignedFiles()
        {
            try
            {
                return Ok(FilesManager.GetNotAssignedFiles());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpGet]
        [Route("/api/FilesWithChunks/{EpisodeId}")]
        public ActionResult FilesWithChunksFromEpisodeId(Guid EpisodeId)
        {
            try
            {
                return Ok(FilesManager.FilesWithChunksFromEpisodeId(EpisodeId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
    }
}
