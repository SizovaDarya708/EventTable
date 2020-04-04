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
        //способы обращения пользователю к боту для вызова функции
        public override List<string> Name => new List<string>() {"Hello", "hello" };
        //Для выолнения не забудь зарегистрировать свой класс в Bot.cs
        public override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            //Здесь прописывается вся логика, функционал, работа с сервисами и т.д.

            client.SendTextMessageAsync(chatId, "Hello!");
        }
    }
}
