using System;
using System.Collections.Generic;
using System.Text;
using EventTable.Models.Entities;
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
        public static void AddUser(User user)
        {
            var con = GetConnection();
            string createCalendar = "INSERT INTO calendar(event_id, note_id) VALUES(null, null)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(createCalendar, con))
            {
                cmd.Parameters.AddWithValue("", "some_value");
                cmd.ExecuteNonQuery();
            }

            string sqlCommand ="INSERT INTO usr(@login, @id) VALUES(@login, @id)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, con))
            {
                //Арбуз, посмотри, можно ли так id добавлять
                cmd.Parameters.AddWithValue("login", user.Login);
                cmd.Parameters.AddWithValue("id", user.ChatId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
