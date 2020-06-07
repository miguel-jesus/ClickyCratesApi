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
    [RoutePrefix("api/Messages")]
    public class MessagesController : ApiController
    {
        //POST api/Messages/InsertNewMessage
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [HttpPost]
        [Route("InsertNewMessage")]
        public bool InsertNewPlayer(MessagesModel messagesModel)
        {
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"INSERT INTO dbo.Messages(IdPlayer,Messages,MessageHour)" +
                    $"VALUES ('{messagesModel.IdPlayer}','{messagesModel.Messages}','{messagesModel.MessageHour}')";
                int rows = cnn.Execute(sql);
                if (rows != 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        //Get api/Messages/GetMessages
        [HttpGet]
        [Route("GetMessages")]
        public List<MessagesModel> GetMessages()
        {
            string authenticatedAspNetUserId = RequestContext.Principal.Identity.GetUserId();
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"SELECT IdPlayer,Messages,MessageHour FROM dbo.Messages ";

                List<MessagesModel> messages = cnn.Query<MessagesModel>(sql).ToList();
                return messages;
            }
        }
    }
}
