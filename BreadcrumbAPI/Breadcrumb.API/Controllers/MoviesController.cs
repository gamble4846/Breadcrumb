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
using Breadcrumb.Utility;

using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Breadcrumb.Model.vTvShowsModels;
using Breadcrumb.Model.tbSeasonsModel;
using Breadcrumb.Model.tbEpisodesModels;
using System.Threading.Tasks;
using Breadcrumb.Model.vMoviesModels;

namespace Breadcrumb.API.Controllers
{
    [Authorize]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        ILog log4Net;
        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions commonFunctions { get; set; }
        public IMoviesManager MoviesManager { get; set; }

        public MoviesController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMoviesManager moviesManager)
        {
            log4Net = this.Log<MoviesController>();
            Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
            commonFunctions = new CommonFunctions(configuration, httpContextAccessor);
            MoviesManager = moviesManager;
        }

        [HttpGet]
        [Route("/api/Movies/Get/")]
        public ActionResult GetMovies(int page = 1, int itemsPerPage = 100, string orderBy = null, string FilterQuery = null)
        {
            try
            {
                if (page <= 0)
                {
                    return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Invalid Page Number", page));
                }

                return Ok(MoviesManager.GetMovies(page, itemsPerPage, orderBy, FilterQuery));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpGet]
        [Route("/api/Movies/Get/{ShowId}")]
        public ActionResult GetMovieById(Guid ShowId)
        {
            try
            {
                return Ok(MoviesManager.GetMovieById(ShowId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpPost]
        [Route("/api/Movies/Insert/")]
        public ActionResult InsertMovie(vMoviesViewModel ViewModel)
        {
            try
            {
                return Ok(MoviesManager.InsertMovie(ViewModel));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpPost]
        [Route("/api/Movies/Update/{ShowId}")]
        public ActionResult UpdateMovie(vMoviesViewModel ViewModel, Guid ShowId)
        {
            try
            {
                return Ok(MoviesManager.UpdateMovie(ViewModel, ShowId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpPost]
        [Route("/api/Movies/Full/InsertUpdate/{ImdbId}")]
        public async Task<ActionResult> InsertUpdateFullMovie(string IMDBId)
        {
            try
            {
                return Ok(await MoviesManager.InsertUpdateFullMovie(IMDBId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
    }
}
