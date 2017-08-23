using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API_CheckInCheckOut.Models
{
    public class ProjectModel
    {
        public async Task<bool> Exists(string idProject)
        {
            try
            {
                String query = String.Format(@"SELECT *
                                            FROM Opportunity
                                            WHERE un_noproyecto = '{0}'", idProject);
                DataTable aux = await DBSingleton.GetDB().GetDataTable(query);
                if(aux.Rows.Count > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> IsOpen(string idProject)
        {
            try
            {
                String query = String.Format(@"SELECT *
                                            FROM Opportunity
                                            WHERE un_noproyecto = '{0}' AND StatusCode = 1 ", idProject);

                DataTable aux = await DBSingleton.GetDB().GetDataTable(query);
                if (aux.Rows.Count > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}