using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApplication1.Models.Data;
using static WebApplication1.Models.Router.Degiskenler;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : ApiController
    {
        [Route("api/getir")]
        public IHttpActionResult Getir()
        {
            return Ok("Deneme");
        }
    }
}