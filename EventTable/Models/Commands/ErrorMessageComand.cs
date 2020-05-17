using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EventTable.Models.Commands
{
    /// <summary>
    /// Команда для корректного вывода сообщения об ошибке
    /// </summary>
    class ErrorMessageComand : Command
    {
        public override List<string> Name => new List<string>() { "Error" };
        public override void Execute(Update update, TelegramBotClient client, Exception e = null)
        {
            var Message = update.Message ?? update.CallbackQuery.Message;

            client.SendTextMessageAsync(Message.Chat.Id, $"У нас возникла ошибка: , {e.Message}, обратитесь в тех поддержку - @daryayan98 ");
        }
    }
}
