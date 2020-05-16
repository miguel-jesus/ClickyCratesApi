using ClickyCratesApi.Models;
using Dapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClickyCratesApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Player")]
    public class PlayersController : ApiController
    {
        //Get api/Player/GetPlayerInfo
        [HttpGet]
        [Route("GetPlayerInfo")]
        public PlayersAPIModels GetPlayerInfo()
        {
            string authenticatedAspNetUserId = RequestContext.Principal.Identity.GetUserId();
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"SELECT Id,FirstName,LastName,NickName,City FROM dbo.Players " +
                    $"WHERE Id LIKE '{authenticatedAspNetUserId}'";
                
                var player = cnn.Query<PlayersAPIModels>(sql).FirstOrDefault();
                return player;
            }
        }

        //POST api/Player/InsertNewPlayer
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [HttpPost]
        [Route("InsertNewPlayer")]
        public bool InsertNewPlayer(PlayersAPIModels player)
        {
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"INSERT INTO dbo.Players(Id,FirstName,LastName,NickName,City)" +
                    $"VALUES ('{player.Id}','{player.FirstName}','{player.LastName}','{player.NickName}','{player.City}')";
                int rows = cnn.Execute(sql);
                if(rows != 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }


    }
}
