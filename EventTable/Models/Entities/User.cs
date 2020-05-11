using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Telegram.Bot.Types;

namespace EventTable.Models.Entities
{
    /// <summary>
    /// Класс пользователя
    /// </summary>
    public class User
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Уникальный Id чата пользователя с ботом
        /// </summary>
        public int ChatId { get; set; }
        /// <summary>
        /// Логин-никнейм телеграм пользователя
        /// </summary>
        public string Login { get; set; }

        public IList<Note> Notes { get; set; }
    }
}
