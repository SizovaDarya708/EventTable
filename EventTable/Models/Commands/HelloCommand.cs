using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EventTable.Models.Commands
{
    /// <summary>
    /// Шаблон коммадны бота, наследуется от Command
    /// </summary>
    public class HelloCommand : Command
    {
        public override string Name => "hello";

        public override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            //Здесь прописывается вся логика, функционал, работа с сервисами и т.д.

            client.SendTextMessageAsync(chatId, "Hello!", replyToMessageId: messageId);
        }
    }
}
