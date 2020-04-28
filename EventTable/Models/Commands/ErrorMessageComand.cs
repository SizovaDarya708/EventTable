using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EventTable.Models.Commands
{
    class ErrorMessageComand : Command
    {
        public override List<string> Name => new List<string>() { "Error" };
        public override void Execute(Message message, TelegramBotClient client/*Exception e*/)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            client.SendTextMessageAsync(chatId, $"У нас возникла ошибка: , , обратитесь в тех поддержку - @daryayan98 ");
        }
    }
}
