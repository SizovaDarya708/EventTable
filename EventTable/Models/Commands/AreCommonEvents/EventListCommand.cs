using EventTable.Helpers;
using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EventTable.Models.Commands
{
    /// <summary>
    /// Вывод всех событий
    /// </summary>
    class EventListCommand : Command
    {
        public override List<string> Name => new List<string>() { "EventList", "Список событий", "Все события" };

        public override void Execute(Update update, TelegramBotClient client, Exception e = null)
        {
            var Message = update.Message ?? update.CallbackQuery.Message;

            DataBaseHelper db = new DataBaseHelper();

            var eventList = db.GetAllEvents();
            var events = InlineKeyBoardHelper.CreateInlineKeyboardButtonForMyEvents(eventList, eventList.Count / 10);

            client.SendTextMessageAsync(Message.Chat.Id, "События:", parseMode: default, false, false, 0, events);
        }      
    }
}
