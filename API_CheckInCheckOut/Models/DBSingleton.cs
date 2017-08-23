using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_CheckInCheckOut.Models
{
    public sealed class DBSingleton
    {
        /// <summary>
        /// this class create DBSettings object using Singleton pattern
        /// </summary>
        private static DatabaseSettingsAsync _dbManager = new DatabaseSettingsAsync();

        public static DatabaseSettingsAsync GetDB()
        {
            return _dbManager;
        }
    }
}