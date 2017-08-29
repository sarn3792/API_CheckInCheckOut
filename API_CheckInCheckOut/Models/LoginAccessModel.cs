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
        public string FullName { get; set; }

        public LoginAccessModel(string userName)
        {
            this.UserName = userName;
            Task.Run(() => this.GetCRM()).Wait();
        }

        private async Task GetCRM()
        {
            try
            {
                String query = String.Format(@"SELECT SystemUserId, FullName
                                                FROM SystemUser
                                                WHERE DomainName = 'UN\{0}'", this.UserName);

                DataTable aux = await DBSingleton.GetDB().GetDataTable(query);
                if(aux.Rows.Count > 0)
                {
                    this.UserIDCRM = aux.Rows[0]["SystemUserId"].ToString();
                    this.FullName = aux.Rows[0]["FullName"].ToString();
                }
                else
                {
                    throw new Exception("Usuario no encontrado en el CRM");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}