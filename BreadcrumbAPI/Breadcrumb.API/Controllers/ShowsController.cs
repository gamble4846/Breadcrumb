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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Breadcrumb.API.Controllers
{
    [Authorize]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        ILog log4Net;
        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions commonFunctions { get; set; }
        public IShowsManager ShowsManager { get; set; }

        public ShowsController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IShowsManager showsManager)
        {
            log4Net = this.Log<ShowsController>();
            Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
            commonFunctions = new CommonFunctions(configuration, httpContextAccessor);
            ShowsManager = showsManager;
        }

        [HttpGet]
        [Route("/api/Shows/Get")]
        public ActionResult Get(int page = 1, int itemsPerPage = 100, string orderBy = null, string FilterQuery = null, string Type = "Movie")
        {
            try
            {
                if (page <= 0)
                {
                    return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Invalid Page Number", page));
                }

                List<OrderByModel> orderModelList = UtilityCommon.ConvertStringOrderToOrderModel(orderBy);

                return Ok(ShowsManager.GetShows(page, itemsPerPage, orderModelList, FilterQuery, Type));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
    }
}
