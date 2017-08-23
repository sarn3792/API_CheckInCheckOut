using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API_CheckInCheckOut.Models
{
    public class LoginAccessModel
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public string UserIDCRM { get; set; }

        public LoginAccessModel(string userName)
        {
            this.UserName = userName;
        }

        public async Task<string> GetCRM()
        {
            try
            {
                String query = String.Format(@"SELECT SystemUserId
                                                FROM SystemUser
                                                WHERE DomainName = 'UN\{0}'", this.UserName);

                DataTable aux = await DBSingleton.GetDB().GetDataTable(query);
                if(aux.Rows.Count > 0)
                {
                    return aux.Rows[0]["SystemUserId"].ToString();
                }

                throw new Exception("Usuario no encontrado en el CRM");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}