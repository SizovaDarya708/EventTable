using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EventTable.Models.Commands
{
    public class CreateNewEventCommand : Command
    {
        public override List<string> Name => new List<string>() { "CreateEvent", "Создай новое событие", "создай новое событие" };

        public override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            client.SendTextMessageAsync(chatId, "Ну давай создадим новое событие", parseMode: default, false, false, 0);

            client.SendTextMessageAsync(chatId, "Через двойной пробел укажите название мероприятия, " +
                "дату <i>в формате ДД.ММ.ГГ<i>, описание, место проведения. \n Вот шаблон:\n " +
                "Новый год!  01.01.21  ноовый год к нам мчится, скоооро...  ул.Добрый вечер",
                parseMode: default, false, false, 0);
        }
    }
}
