using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EventTable.Models.Commands
{
    public class CreateNewEventCommand : Command
    {
        public override List<string> Name => new List<string>() { "CreateEvent", "Создай новое событие", "создай новое событие" };

        public override void Execute(Update update, TelegramBotClient client, Exception e = null)
        {
            var Message = update.Message ?? update.CallbackQuery.Message;

           // client.SendTextMessageAsync(Message.Chat.Id, "Ну давай создадим новое событие", ParseMode.Html, false, false, 0);

            client.SendTextMessageAsync(Message.Chat.Id, "Начните сообщение со слов: \"Новое событие\" Через двойной пробел укажите название мероприятия, " +
                "дату (в формате ДД.ММ.ГГ ЧЧ.ММ), описание, место проведения. \n Вот шаблон:\n " +
                "Новое событие  Новый год!  31.12.20 23:59 ноовый год к нам мчится, скоооро...  ул.Добрый вечер",
               parseMode: default, false, false, 0);
        }
    }
}
