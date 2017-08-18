using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_CheckInCheckOut.Models
{
    public class LoginAccessModel
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
    }
}