using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using Microsoft.AspNetCore.Http;
using IniParser;
using IniParser.Model;
using System.Data;
using System.Reflection;
using Breadcrumb.Model;
using Newtonsoft.Json;
using Breadcrumb.Model.GoogleApiModels;
using static System.Net.WebRequestMethods;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace Breadcrumb.Utility
{
    public class CommonFunctions
    {
        public IConfiguration Configuration { get; }
        public string ContentRootPath { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public CommonFunctions(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
        }

        public CommonFunctions()
        {

        }

        public TokenModel GetTokenData()
        {
            try
            {
                string token = GetTokenFromHeader();

                if(String.IsNullOrEmpty(token))
                {
                    return null;
                }

                token = token.Replace("Bearer ", "");
                var jwtSection = Configuration.GetSection("Jwt");
                var Secret = Encoding.ASCII.GetBytes(jwtSection.GetValue<String>("Secret"));
                var ValidIssuer = jwtSection.GetValue<String>("ValidIssuer");
                var ValidAudience = jwtSection.GetValue<String>("ValidAudience");
                var handler = new JwtSecurityTokenHandler();
                var validations = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Secret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var claims = handler.ValidateToken(token, validations, out var tokenSecure);
                var otherClaims = claims.Identities.ToList()[0].Claims.ToList(); 

                TokenModel TokenData = new TokenModel();
                TokenData = JsonConvert.DeserializeObject<TokenModel>(otherClaims.Find(x => x.Type == "TokenData").Value);
                return TokenData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetTokenFromHeader()
        {
            try
            {
                return HttpContextAccessor.HttpContext.Request.Headers["Authorization"];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<FolderAPI> GetFilesByFolderID(FolderAPI CurrentFolder, string GoogleApiKey)
        {
            var CurrentApiLink = "https://www.googleapis.com/drive/v2/files?q=%27" + CurrentFolder.Id + "%27+in+parents&key=" + GoogleApiKey + "#";


            while (!String.IsNullOrEmpty(CurrentApiLink))
            {

                var MainFolderFiles = await UtilityCustom.RestCall(CurrentApiLink);
                foreach (var item in MainFolderFiles.items)
                {
                    if (item.mimeType == "application/vnd.google-apps.folder")
                    {
                        var Folder = new FolderAPI()
                        {
                            Name = item.title,
                            Id = item.id,
                            Files = new List<FilesApi>(),
                            Folders = new List<FolderAPI>()
                        };
                        CurrentFolder.Folders.Add(Folder);
                    }
                    else
                    {
                        var File = new FilesApi()
                        {
                            Name = item.title,
                            ThumbnailLink = item.thumbnailLink,
                            Type = item.mimeType,
                            Size = item.fileSize,
                            Email = item.owners[0].emailAddress,
                            Id = item.id
                        };
                        CurrentFolder.Files.Add(File);
                    }
                }

                try
                {
                    string NextLink = MainFolderFiles.nextLink + "";
                    if (!String.IsNullOrEmpty(NextLink))
                    {
                        CurrentApiLink = MainFolderFiles.nextLink + "&key=" + GoogleApiKey;
                    }
                    else
                    {
                        CurrentApiLink = null;
                    }

                }
                catch (Exception ex)
                {
                    CurrentApiLink = null;
                }
            }

            return CurrentFolder;
        }
    }
}
