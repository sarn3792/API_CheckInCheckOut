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
    public class ProjectController : ApiController
    {
        [HttpGet]
        [Route("api/Project/{idProject}")]
        public async Task<HttpResponseMessage> Get([FromUri] string idProject)
        {
            try
            {
                if (idProject != null)
                {
                    ProjectModel model = new ProjectModel();
                    if (await model.Exists(idProject))
                    {
                        if(await model.IsOpen(idProject))
                        {
                            return Request.CreateResponse(HttpStatusCode.OK);
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "El proyecto se encuentra cerrado");
                        }
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No fue encontrado el proyecto");
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
