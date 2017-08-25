using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API_CheckInCheckOut.Models
{
    public class ActivityHistoryModel
    {
        public async Task Update(List<ActivityHistory> data)
        {
            try
            {
                foreach(ActivityHistory obj in data)
                {
                    String query;
                    if(obj.checkInCheckOut) //check in
                    {
                        query = String.Format(@"UPDATE un_historialactividadproyectoBase SET un_Fecha = '{0}',  
                                               un_Latitud = '{1}', un_Longitud = '{2}', un_User = '{3}'
                                               WHERE un_Actividad = '{4}' AND un_Estatus = 1", 
                                               obj.date.ToString("yyyy-MM-dd HH':'mm':'ss"), obj.latitude, obj.longitude, obj.userID, obj.activityID);
                    }
                    else //check out
                    {
                        query = String.Format(@"UPDATE un_historialactividadproyectoBase SET un_Fecha = '{0}',  
                                               un_Latitud = '{1}', un_Longitud = '{2}', un_User = '{3}'
                                               WHERE un_Actividad = '{4}' AND un_Estatus = 0",
                                               obj.date.ToString("yyyy-MM-dd HH':'mm':'ss"), obj.latitude, obj.longitude, obj.userID, obj.activityID);
                    }
                    await DBSingleton.GetDB().ExecuteQuery(query);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetDataBetweenDates(string idActivity)
        {
            try
            {
                String query = String.Format(@"SELECT DATEDIFF(hour, x.FechaCheckIn, y.FechaCheckOut) as 'Hours', 
		                                            DATEDIFF(MINUTE, x.FechaCheckIn, y.FechaCheckOut) % 60 as 'Minutes'
                                            FROM
	                                            (
		                                            SELECT un_Fecha as 'FechaCheckIn'
		                                            FROM un_historialactividadproyectoBase
		                                            WHERE un_Actividad = '{0}' AND un_Estatus = 1
	                                            ) X, 
	                                            (
		                                            SELECT un_Fecha as 'FechaCheckOut'
		                                            FROM un_historialactividadproyectoBase
		                                            WHERE un_Actividad = '{0}' AND un_Estatus = 0
	                                            ) y", idActivity);
                DataTable aux = await DBSingleton.GetDB().GetDataTable(query);
                if(aux.Rows.Count > 0)
                {
                    string time = string.Empty;
                    if(aux.Rows[0]["Hours"].ToString() != "0")
                    {
                        int hours = Convert.ToInt32(aux.Rows[0]["Hours"].ToString());
                        if (hours <= 1)
                        {
                            time = String.Format("{0} hora ", hours);
                        }
                        else
                        {
                            time = String.Format("{0} horas ", hours);
                        }
                    }

                    if(aux.Rows[0]["Minutes"].ToString() != "0")
                    {
                        int minutes = Convert.ToInt32(aux.Rows[0]["Minutes"].ToString());
                        if(minutes <= 1)
                        {
                            time += String.Format("{0} minuto", minutes);
                        }
                        else
                        {
                            time += String.Format("{0} minutos", minutes);
                        }
                    }
                    return time;
                }

                throw new Exception("Ha ocurrido un error");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
    public class ActivityHistory
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string userID { get; set; }
        public bool checkInCheckOut { get; set;}
        public DateTime date { get; set; }
        //public DateTime date = DateTime.Now;
        public string activityID { get; set; }
    }
}