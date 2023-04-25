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
using Breadcrumb.Model.vMoviesModels;

namespace Breadcrumb.Manager.Impl
{
    public class TheMovieDBManager : ITheMovieDBManager
    {
        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public CommonFunctions CommonFunctions { get; set; }
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

            foreach (var season in tvShowFromTMDB.seasons)
            {
                var seasonModel = new SeasonTMDBModel()
                {
                    TMDBId = season.id.ToString(),
                    Number = int.Parse(season.season_number.ToString()),
                    Name = season.name.ToString()
                };

                TvShow.Seasons.Add(seasonModel);
            }

            foreach (var season in TvShow.Seasons)
            {
                season.Episodes = new List<EpisodeTMDBModel>();
                var Episodes = (await UtilityCustom.RestCall("https://api.themoviedb.org/3/tv/" + TMDBId + "/season/" + season.Number + "?api_key=" + TheMovieDBAPIKey)).episodes;
                foreach (var epi in Episodes)
                {
                    var EpisodeTMDB = new EpisodeTMDBModel();
                    EpisodeTMDB.Number = epi.episode_number;
                    EpisodeTMDB.Name = epi.name;
                    EpisodeTMDB.Description = epi.overview;

                    if (epi.still_path != null)
                    {
                        EpisodeTMDB.ThumbnailLink = "[||REPLACEWITHTMDBIMAGEHOST||]" + epi.still_path;
                    }

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


        public async Task<APIResponse> GetMovieByIMDBId(string IMDBId)
        {
            vMoviesViewModel fullMovie = new vMoviesViewModel();
            
            var moivieFromIMDB = await UtilityCustom.RestCall("https://api.themoviedb.org/3/find/" + IMDBId + "?api_key=" + TheMovieDBAPIKey + "&external_source=imdb_id");
            var TMDBId = moivieFromIMDB.movie_results[0].id;
            var movieFromTMDB = await UtilityCustom.RestCall("https://api.themoviedb.org/3/movie/" + TMDBId + "?api_key=" + TheMovieDBAPIKey);

            List<string> generes = new List<string>();

            foreach (var genre in movieFromTMDB.genres)
            {
                generes.Add(genre.name.ToString());
            }

            fullMovie.PrimaryName = movieFromTMDB.title;
            fullMovie.OtherNames = movieFromTMDB.original_title;
            fullMovie.Description = movieFromTMDB.overview;
            fullMovie.IMDBID = IMDBId;
            fullMovie.ReleaseYear = ((DateTime.Parse(movieFromTMDB.release_date.ToString())).Year).ToString();
            fullMovie.Genres = String.Join(',', generes);

            return new APIResponse(ResponseCode.SUCCESS, "Records Found", fullMovie);
        }


        public async Task<APIResponse> GetImagesByIMDBId(string IMDBId, string ShowType)
        {
            string TMDBId = "";
            if (ShowType == "tv")
            {
                var tvShowFromIMDB = await UtilityCustom.RestCall("https://api.themoviedb.org/3/find/" + IMDBId + "?api_key=" + TheMovieDBAPIKey + "&external_source=imdb_id");
                TMDBId = tvShowFromIMDB.tv_results[0].id;
            }
            else
            {
                var moivieFromIMDB = await UtilityCustom.RestCall("https://api.themoviedb.org/3/find/" + IMDBId + "?api_key=" + TheMovieDBAPIKey + "&external_source=imdb_id");
                TMDBId = moivieFromIMDB.movie_results[0].id;
            }

            List<FullImageTMDBModel> FullImages = new List<FullImageTMDBModel>();
            string apiLink = "https://api.themoviedb.org/3/" + ShowType + "/" + TMDBId + "/images?api_key=" + TheMovieDBAPIKey;
            var imagesFromTMDB = await UtilityCustom.RestCall(apiLink);

            foreach (var imageData in imagesFromTMDB.backdrops)
            {
                FullImageTMDBModel image = new FullImageTMDBModel()
                {
                    AspectRatio = imageData.aspect_ratio,
                    Height = imageData.height,
                    ISO6391 = imageData.iso_639_1,
                    FilePath = "[||REPLACEWITHTMDBIMAGEHOST||]" + imageData.file_path,
                    VoteAverage = imageData.vote_average,
                    VoteCount = imageData.vote_count,
                    Width = imageData.width
                };

                FullImages.Add(image);
            }

            foreach (var imageData in imagesFromTMDB.posters)
            {
                FullImageTMDBModel image = new FullImageTMDBModel()
                {
                    AspectRatio = imageData.aspect_ratio,
                    Height = imageData.height,
                    ISO6391 = imageData.iso_639_1,
                    FilePath = "[||REPLACEWITHTMDBIMAGEHOST||]" + imageData.file_path,
                    VoteAverage = imageData.vote_average,
                    VoteCount = imageData.vote_count,
                    Width = imageData.width
                };

                FullImages.Add(image);
            }

            return new APIResponse(ResponseCode.SUCCESS, "Records Found", FullImages);
        }
    }
}

