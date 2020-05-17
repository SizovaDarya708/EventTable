using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EventTable.Models.Commands
{
    /// <summary>
    /// Базовый класс команды
    /// </summary>
    public abstract class Command
    {
        public abstract List<string> Name { get; }

        public abstract void Execute(Update update, TelegramBotClient client, Exception e = null );

        public bool Contains(string command)
        {
            return command.Contains(this.Name.First()) && command.Contains(AppSettings.Name);
        }

    }
}
