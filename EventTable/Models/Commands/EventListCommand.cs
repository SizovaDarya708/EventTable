using EventTable.Helpers;
using EventTable.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace EventTable.Models.Commands
{
    class EventListCommand : Command
    {
        public override List<string> Name => new List<string>() { "EventList", "Список событий", "Все события" };

        public override void Execute(Update update, TelegramBotClient client, Exception e = null)
        {
            var Message = update.Message ?? update.CallbackQuery.Message;

            DataBaseHelper db = new DataBaseHelper();

            var eventList = db.GetAllEvents();
            //Если слишком много событий можно отрегулировать столбцы
            var events = CreateInlineKeyboardButton(eventList, 1);
            
            client.SendTextMessageAsync(Message.Chat.Id, "События:", parseMode: default, false, false, 0, events);
        }

        public static IReplyMarkup CreateInlineKeyboardButton(List<Event> eventList, int columns)
        {
            int rows = (int)Math.Ceiling((double)eventList.Count / (double)columns);
            InlineKeyboardButton[][] buttons = new InlineKeyboardButton[rows][];

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = eventList
                    .Skip(i * columns)
                    .Take(columns)
                    .Select(direction => InlineKeyboardButton.WithCallbackData(
                       direction.Name, direction.Id.ToString())) 
                    .ToArray();
            }
            return new InlineKeyboardMarkup(buttons);
        }

    }
}
