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
                string sql = $"SELECT Id,FirstName,LastName,NickName,City,BirthDay FROM dbo.Players " +
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
                string sql = $"INSERT INTO dbo.Players(Id,FirstName,LastName,NickName,City,BirthDay)" +
                    $"VALUES ('{player.Id}','{player.FirstName}','{player.LastName}','{player.NickName}','{player.City}','{player.BirthDay}')";
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

        //Get api/Player/GetPlayersInfo
        [HttpGet]
        [Route("GetPlayersInfo")]
        public List<PlayersAPIModels> GetPlayersInfo()
        {
            string authenticatedAspNetUserId = RequestContext.Principal.Identity.GetUserId();
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"SELECT FirstName,LastName,NickName,City,BirthDay,IsOnline,LastLogin FROM dbo.Players WHERE IsOnline = 1 ";

                List<PlayersAPIModels> player = cnn.Query<PlayersAPIModels>(sql).ToList();
                return player;
            }
        }

        //POST api/Player/UpdatePlayer
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [HttpPost]
        [Route("UpdatePlayer")]
        public IHttpActionResult UpdatePlayer(PlayersAPIModels player)
        {

            IDbConnection con = new ApplicationDbContext().Database.Connection;

            string sql = "UPDATE dbo.Players " +
                $"SET FirstName = '{player.FirstName}', LastName = '{player.LastName}', NickName = '{player.NickName}',City = '{player.City}'," +
                 $"BirthDay = '{player.BirthDay}',IsOnline = '{player.IsOnline}',LastLogin = '{player.LastLogin}' " +
                $"WHERE Id = '{player.Id}'";

            try
            {
                con.Execute(sql);

            }
            catch (Exception e)
            {
                return BadRequest("Error Update player in database, " + e.Message);
            }
            finally
            {
                con.Close();
            }

            return Ok();
        }

    }
}
