using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace EventTable.Helpers
{
    /// <summary>
    /// Подключение к базе данных
    /// </summary>
    public class DBConnection
    {
        /// <summary>
        /// Установка соединения
        /// </summary>
        public void GetConnection()
        {
            using var con = new NpgsqlConnection(AppSettings.DBConnection);
            con.Open();
            var sql = "SELECT version()";
            using var cmd = new NpgsqlCommand(sql, con);
            Version = cmd.ExecuteScalar().ToString();
        }
        /// <summary>
        /// Версия подключения
        /// </summary>
        public string Version { get; set; }
    }

    

    class DataBaseHelper
    {
    }
}
