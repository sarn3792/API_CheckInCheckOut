using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API_CheckInCheckOut.Models
{
    public class ActivityModel
    {
        public async Task<List<Activity>> Get(string idProject)
        {
            try
            {
                String query = String.Format(@"SELECT op.Name, ac.un_name, ac.un_actividaddeproyectoId
                                            FROM Opportunity op 
                                            INNER JOIN un_actividaddeproyectoBase ac ON op.OpportunityId = ac.un_proyecto
                                            WHERE op.un_noproyecto = '{0}' and op.StatusCode = 1 and ac.statuscode = 1 ", idProject);
                DataTable aux = await DBSingleton.GetDB().GetDataTable(query);

                List<Activity> data = aux.AsEnumerable().Select(m => new Activity()
                {
                    description = m.Field<string>("un_name"),
                    idActivity = Convert.ToString(m.Field<Guid>("un_actividaddeproyectoId")),
                    projectName = m.Field<string>("Name")
                }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task SetActivitiesFinished(List<string> data)
        {
            try
            {
                foreach(string act in data)
                {
                    String query = String.Format("UPDATE un_actividaddeproyectoBase SET statuscode = 2, statecode = 1 WHERE un_actividaddeproyectoId = '{0}'", act);
                    await DBSingleton.GetDB().ExecuteQuery(query);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class Activity
    {
        public string description { get; set; }
        public string idActivity { get; set; }
        public string projectName { get; set; }
    }
}