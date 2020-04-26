using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace EventTable.Helpers
{
    /// <summary>
    /// Подключение к базе данных
    /// </summary>
    public class DataBaseHelper
    {
        /// <summary>
        /// Полуение соединения
        /// </summary>
        public static NpgsqlConnection GetConnection()
        {
            using var con = new NpgsqlConnection(AppSettings.DBConnection);
            con.Open();
            return con;
        }

        /// <summary>
        /// Метод, который добавляет пользователя
        /// 1. Создать календарь
        /// 2. Создать пользователя
        /// </summary>
        public static void AddUser(string login, string city)
        {
            var con = GetConnection();
            string createCalendar = "INSERT INTO calendar(event_id, note_id) VALUES(null, null)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(createCalendar, con))
            {
                cmd.ExecuteNonQuery();
            }

            string sqlCommand ="INSERT INTO usr(@login, @city) VALUES(@login, @city)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, con))
            {
                cmd.Parameters.AddWithValue("login", login);
                cmd.Parameters.AddWithValue("city", city);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
