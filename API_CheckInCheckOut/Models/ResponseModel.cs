using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_CheckInCheckOut.Models
{
    public class ResponseModel
    {

    }

    public class Response
    {
        public string message { get; set; }

        public Response(string message)
        {
            this.message = message;
        }
    }
}