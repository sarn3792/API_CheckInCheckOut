using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;

namespace API_CheckInCheckOut.Models
{
    public class DatabaseSettingsAsync
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader reader;
        private String connectionString;
        //private DataTable data = new DataTable();

        public DatabaseSettingsAsync()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["Testing"].ConnectionString;
                conn = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DataTable> GetDataTable(String query)
        {
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandTimeout = 0;
                conn.Open();
                reader = await cmd.ExecuteReaderAsync();
                DataTable data = new DataTable();
                data.Load(reader);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public async Task ExecuteQuery(String query)
        {
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandTimeout = 0;
                conn.Open();
                cmd.CommandType = CommandType.Text;
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }
    }
}