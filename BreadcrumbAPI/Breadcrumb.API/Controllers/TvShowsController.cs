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
using Breadcrumb.Model.FilesModels;

namespace Breadcrumb.API.Controllers
{
    [Authorize]
    [ApiController]
    public class TvShowsController : ControllerBase
    {
        ILog log4Net;
        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions commonFunctions { get; set; }
        public ITvShowsManager TvShowsManager { get; set; }

        public TvShowsController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ITvShowsManager tvShowsManager)
        {
            log4Net = this.Log<TvShowsController>();
            Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
            commonFunctions = new CommonFunctions(configuration, httpContextAccessor);
            TvShowsManager = tvShowsManager;
        }

        [HttpGet]
        [Route("/api/TvShows/Get/")]
        public ActionResult GetTvshow(int page = 1, int itemsPerPage = 100, string orderBy = null, string FilterQuery = null)
        {
            try
            {
                if (page <= 0)
                {
                    return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Invalid Page Number", page));
                }

                return Ok(TvShowsManager.GetTvShows(page, itemsPerPage, orderBy, FilterQuery));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpGet]
        [Route("/api/TvShows/Get/{ShowId}")]
        public ActionResult GetTvshowById(Guid ShowId)
        {
            try
            {
                return Ok(TvShowsManager.GetTvshowById(ShowId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpPost]
        [Route("/api/TvShows/Insert/")]
        public ActionResult InsertTvshow(vTvShowsViewModel ViewModel)
        {
            try
            {
                return Ok(TvShowsManager.InsertTvShow(ViewModel));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpPost]
        [Route("/api/TvShows/Update/{ShowId}")]
        public ActionResult UpdateTvshow(vTvShowsViewModel ViewModel, Guid ShowId)
        {
            try
            {
                return Ok(TvShowsManager.UpdateTvShow(ViewModel, ShowId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpDelete]
        [Route("/api/TvShows/Delete/{ShowId}")]
        public ActionResult DeleteTvshow(Guid ShowId)
        {
            try
            {
                return Ok(TvShowsManager.DeleteTvShow(ShowId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpGet]
        [Route("/api/TvShows/GetAll/")]
        public ActionResult GetAllTvshows()
        {
            try
            {
                return Ok(TvShowsManager.GetAllTvshows());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }



        [HttpGet]
        [Route("/api/TvShows/Seasons/Get/{ShowId}")]
        public ActionResult GetTvShowSeasons(Guid ShowId)
        {
            try
            {
                return Ok(TvShowsManager.GetTvShowSeasons(ShowId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpPost]
        [Route("/api/TvShows/Seasons/Insert/")]
        public ActionResult InsertTvShowSeason(tbSeasonsViewModel ViewModel)
        {
            try
            {
                return Ok(TvShowsManager.InsertTvShowSeason(ViewModel));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpPost]
        [Route("/api/TvShows/Seasons/InsertMultiple/")]
        public ActionResult InsertUpdateTvShowSeasonMultiple(List<tbSeasonsViewModel> ViewModelList)
        {
            try
            {
                return Ok(TvShowsManager.InsertUpdateTvShowSeasonMultiple(ViewModelList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpPost]
        [Route("/api/TvShows/Seasons/Update/{SeasonId}")]
        public ActionResult UpdateTvShowSeasons(tbSeasonsViewModel ViewModel, Guid SeasonId)
        {
            try
            {
                return Ok(TvShowsManager.UpdateTvShowSeasons(ViewModel, SeasonId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpDelete]
        [Route("/api/TvShows/Seasons/Delete/{SeasonId}")]
        public ActionResult DeleteTvShowSeasons(Guid SeasonId)
        {
            try
            {
                return Ok(TvShowsManager.DeleteTvShowSeasons(SeasonId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }



        [HttpGet]
        [Route("/api/TvShows/Episodes/Get/{SeasonId}")]
        public ActionResult GetTvShowEpisodes(Guid SeasonId)
        {
            try
            {
                return Ok(TvShowsManager.GetTvShowEpisodes(SeasonId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpGet]
        [Route("/api/TvShows/EpisodesWithFiles/Get/{SeasonId}")]
        public ActionResult GetTvShowEpisodesWithFiles(Guid SeasonId)
        {
            try
            {
                return Ok(TvShowsManager.GetTvShowEpisodesWithFiles(SeasonId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpPost]
        [Route("/api/TvShows/Episodes/Insert/")]
        public ActionResult InsertTvShowEpisodes(tbEpisodesViewModel ViewModel)
        {
            try
            {
                return Ok(TvShowsManager.InsertTvShowEpisodes(ViewModel));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpPost]
        [Route("/api/TvShows/Episodes/InsertMultiple/")]
        public ActionResult InsertUpdateTvShowEpisodesMultiple(List<tbEpisodesViewModel> ViewModelList)
        {
            try
            {
                return Ok(TvShowsManager.InsertUpdateTvShowEpisodesMultiple(ViewModelList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpPost]
        [Route("/api/TvShows/Episodes/Update/{EpisodeId}")]
        public ActionResult UpdateTvShowEpisodes(tbEpisodesViewModel ViewModel, Guid EpisodeId)
        {
            try
            {
                return Ok(TvShowsManager.UpdateTvShowEpisodes(ViewModel, EpisodeId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpDelete]
        [Route("/api/TvShows/Episodes/Delete/{EpisodeId}")]
        public ActionResult DeleteTvShowEpisodes(Guid EpisodeId)
        {
            try
            {
                return Ok(TvShowsManager.DeleteTvShowEpisodes(EpisodeId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }



        [HttpPost]
        [Route("/api/TvShows/Full/InsertUpdate/{ImdbId}")]
        public async Task<ActionResult> InsertUpdateFullTvShow(string IMDBId)
        {
            try
            {
                return Ok(await TvShowsManager.InsertUpdateFullTvShow(IMDBId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [HttpPost]
        [Route("/api/TvShows/EpisodesFiles/InsertUpdateDelete/{SeasonId}")]
        public ActionResult InsertUpdateDeleteEpisodesFiles(List<tbShowsFileViewModel> ViewModel, Guid SeasonId)
        {
            try
            {
                return Ok(TvShowsManager.InsertUpdateDeleteEpisodesFiles(ViewModel, SeasonId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
    }
}
