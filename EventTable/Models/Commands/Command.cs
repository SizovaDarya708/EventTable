using EventTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EventTable.Models.Commands
{
    /// <summary>
    /// Тут наверное лучше через интерфейс сделать, о вот так пока
    /// </summary>
    public abstract class Command
    {
        public abstract List<string> Name { get; }

        public abstract void Execute(Message message, TelegramBotClient client);

        public bool Contains(string command)
        {
            return command.Contains(this.Name.First()) && command.Contains(AppSettings.Name);
        }

    }
}
