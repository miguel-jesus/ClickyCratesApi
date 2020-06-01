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
    [RoutePrefix("api/Objects")]
    public class ObjectsController : ApiController
    {
        //Get api/Objects/GetObjectsInfo
        [HttpGet]
        [Route("GetObjectsInfo")]
        public ObjectsModel GetObjectsInfo()
        {
            string authenticatedAspNetUserId = RequestContext.Principal.Identity.GetUserId();
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"SELECT Id,Synti,Box,Barrel,Skull FROM dbo.Objects " +
                    $"WHERE Id LIKE '{authenticatedAspNetUserId}'";

                var objects = cnn.Query<ObjectsModel>(sql).FirstOrDefault();
                return objects;
            }
        }

        //POST api/Objects/InsertNewObjects
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [HttpPost]
        [Route("InsertNewObjects")]
        public bool InsertNewObjects(ObjectsModel objects)
        {
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"INSERT INTO dbo.Objects(Id) " +
                    $"VALUES ('{objects.Id}')";
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

        //POST api/Objects/UpdateObjects
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [HttpPost]
        [Route("UpdateObjects")]
        public IHttpActionResult UpdateObjects(ObjectsModel objects)
        {

            IDbConnection con = new ApplicationDbContext().Database.Connection;

            string sql = "UPDATE dbo.Objects " +
                $"SET Synti = '{objects.Synti}', Box = '{objects.Box}', Barrel = '{objects.Barrel}',Skull = '{objects.Skull}' " +
                $"WHERE Id = '{objects.Id}'";

            try
            {
                con.Execute(sql);

            }
            catch (Exception e)
            {
                return BadRequest("Error Update objects in database, " + e.Message);
            }
            finally
            {
                con.Close();
            }

            return Ok();
        }
    }

    
}
