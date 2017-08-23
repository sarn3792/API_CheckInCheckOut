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
    public class ActivityHistoryController : ApiController
    {
        [HttpPut]
        [Route("api/ActivityHistory")]
        public async Task<HttpResponseMessage> Put([FromBody] List<ActivityHistory> data)
        {
            try
            {
                if(data != null && data.Count > 0)
                {
                    ActivityHistoryModel model = new ActivityHistoryModel();
                    await model.Update(data);
                    if (!(data.First()).checkInCheckOut) //if check out return time between two dates and check activities finished
                    {
                        ActivityModel activity = new ActivityModel();
                        var activitiesID = data.Select(m => m.activityID).ToList();
                        await activity.SetActivitiesFinished(activitiesID);
                        Response time = new Response(await model.GetDataBetweenDates((data.First()).activityID));
                        return Request.CreateResponse(HttpStatusCode.OK, time);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK);
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
