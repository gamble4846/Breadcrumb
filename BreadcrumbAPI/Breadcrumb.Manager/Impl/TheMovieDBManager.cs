using Microsoft.AspNetCore.Http;
using Breadcrumb.Manager.Interface;
using Breadcrumb.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Breadcrumb.DataAccess.SQLServer.Impl;
using Breadcrumb.Model.vTvShowsModels;
using Breadcrumb.Model.tbSeasonsModel;
using Breadcrumb.Model.tbEpisodesModels;
using Breadcrumb.Model;
using System.Threading.Tasks;
using System.Linq;
using Breadcrumb.Model.TheMovieDBModels;

namespace Breadcrumb.Manager.Impl
{
    public class TheMovieDBManager : ITheMovieDBManager
    {
        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions CommonFunctions { get; set; }
        MSSqlDatabase MsSqlDatabase { get; set; }
        Breadcrumb.DataAccess.SQLServer.Interface.ITvShowsDataAccess SqlTvShowsDataAccess { get; set; }
        public TokenModel TokenData { get; set; }
        public string TheMovieDBAPIKey { get; set; }

        public TheMovieDBManager(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
            CommonFunctions = new CommonFunctions(configuration, httpContextAccessor);
            TokenData = CommonFunctions.GetTokenData();
            TheMovieDBAPIKey = TokenData.TheMovieDBAPIKey;
        }

        public async Task<APIResponse> GetTvshowByIMDBId(string IMDBId)
        {
            FullTVShowTMDBModel TvShow = new FullTVShowTMDBModel();
            TvShow.Seasons = new List<SeasonTMDBModel>();

            var tvShowFromIMDB = await UtilityCustom.RestCall("https://api.themoviedb.org/3/find/" + IMDBId + "?api_key=" + TheMovieDBAPIKey + "&external_source=imdb_id");
            var TMDBId = tvShowFromIMDB.tv_results[0].id;
            var tvShowFromTMDB = await UtilityCustom.RestCall("https://api.themoviedb.org/3/tv/" + TMDBId + "?api_key=" + TheMovieDBAPIKey);

            List<string> generes = new List<string>();

            foreach (var genre in tvShowFromTMDB.genres)
            {
                generes.Add(genre.name.ToString());
            }

            TvShow.PrimaryName = tvShowFromTMDB.original_name;
            TvShow.OtherNames = tvShowFromTMDB.name;
            TvShow.Description = tvShowFromTMDB.overview;
            TvShow.IMDBID = IMDBId;
            TvShow.ReleaseYear = ((DateTime.Parse(tvShowFromTMDB.first_air_date.ToString())).Year).ToString();
            TvShow.Genres = String.Join(',', generes);

            foreach(var season in tvShowFromTMDB.seasons)
            {
                var seasonModel = new SeasonTMDBModel()
                {
                    TMDBId = season.id.ToString(),
                    Number = int.Parse(season.season_number.ToString()),
                    Name = season.name.ToString()
                };

                TvShow.Seasons.Add(seasonModel);
            }

            foreach(var season in TvShow.Seasons)
            {
                season.Episodes = new List<EpisodeTMDBModel>();
                var Episodes = (await UtilityCustom.RestCall("https://api.themoviedb.org/3/tv/" + TMDBId + "/season/" + season.Number + "?api_key=" + TheMovieDBAPIKey)).episodes;
                foreach(var epi in Episodes)
                {
                    var EpisodeTMDB = new EpisodeTMDBModel();
                    EpisodeTMDB.Number = epi.episode_number;
                    EpisodeTMDB.Name = epi.name;

                    try
                    {
                        EpisodeTMDB.RelaseDate = DateTime.Parse((epi.air_date).ToString());
                    }
                    catch
                    {
                        EpisodeTMDB.RelaseDate = null;
                    }

                    season.Episodes.Add(EpisodeTMDB);
                }
            }

            return new APIResponse(ResponseCode.SUCCESS, "Records Found", TvShow);
        }
    }
}

