using System;
using System.Collections.Generic;
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
        public override List<string> Name => new List<string>() {"Hello", "hello", "Привет" };
        //Для выолнения не забудь зарегистрировать свой класс в Bot.cs
        public override void Execute(Update update, TelegramBotClient client, Exception? e)
        {
            var chatId = update.Message.Chat.Id;

            //Здесь прописывается вся логика, функционал, работа с сервисами и т.д.
            client.SendTextMessageAsync(chatId, "Привет, странник", parseMode: default, false, false, 0);
        }
    }
}
