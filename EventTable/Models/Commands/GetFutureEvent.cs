using EventTable.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace EventTable.Models.Commands
{
    class GetFutureEvent : Command
    {
        public override List<string> Name => new List<string>() { "Определенное будущее событие" };

        public override void Execute(Update update, TelegramBotClient client, Exception e = null)
        {
            var Message = update.Message ?? update.CallbackQuery.Message;

            DataBaseHelper db = new DataBaseHelper();
            var events = db.GetAllEvents();

            var currentEvent = events.Where(x => x.Id == Convert.ToInt32(update.CallbackQuery.Data)).FirstOrDefault();

            var adm = new InlineKeyboardMarkup(new[]
                            {
                                 new[] {InlineKeyboardButton.WithCallbackData("Отписаться от события", $"DeleteEvent:{update.CallbackQuery.Data}") }
                            });

            client.SendTextMessageAsync(Message.Chat.Id, $"Название: {currentEvent.Name}\nОписание: {currentEvent.Description}" +
                $"\nВремя: {currentEvent.Happen_date}\nМесто: {currentEvent.Place}", parseMode: default, false, false, 0, adm);
        }
    }
}
