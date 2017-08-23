using API_CheckInCheckOut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API_CheckInCheckOut.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    //[Authorize]
    public class ActivityController : ApiController
    {
        [HttpGet]
        [Route("api/Project/{idProject}/Activities")]
        public async Task<HttpResponseMessage> Get([FromUri] string idProject)
        {
            try
            {
                if (idProject != null)
                {
                    ActivityModel model = new ActivityModel();
                    List<Activity> data = await model.Get(idProject);
                    if(data.Count > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No fueron encontradas actividades abiertas relacionadas a este proyecto");
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Parámetro nulo");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
