using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace EventTable.Models.Entities
{
    /// <summary>
    /// Класс пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный Id чата пользователя с ботом
        /// </summary>
        public int ChatId { get; set; }
        /// <summary>
        /// Логин-никнейм телеграм пользователя
        /// </summary>
        public string Login { get; set; }

        public string City { get; set; }

        /// <summary>
        /// Список событий, где пользователь является участником
        /// </summary>
        public List<Event> Events {get; set;}

        /// <summary>
        /// Список событий, которые пользователь создал
        /// </summary>
        public List<Event> CreatedEvents { get; set; }

        public User(Update update)
        {
            var message = update.Message ?? update.CallbackQuery.Message;
            
            ChatId = Convert.ToInt32(message.Chat.Id);
            Login = message.From.Username;
        }
    }
}
