using EventTable.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace EventTable.Models.Commands
{
    /// <summary>
    /// Вывод определенного предстоящего события, на которое подписался пользователь
    /// </summary>
    class GetFutureEventCommand : Command
    {        
        public override List<string> Name => new List<string>() { "Определенное будущее событие" };

        public override void Execute(Update update, TelegramBotClient client, Exception e = null)
        {
            var Message = update.Message ?? update.CallbackQuery.Message;

            DataBaseHelper db = new DataBaseHelper();
            var events = db.GetAllEvents();

            var eventId = update.CallbackQuery.Data.Split(":")[1];
            var currentEvent = events.Where(x => x.Id == Convert.ToInt32(eventId)).FirstOrDefault();

            var adm = new InlineKeyboardMarkup(new[]
                            {
                                 new[] {InlineKeyboardButton.WithCallbackData("Отписаться от события", $"SignOut:{update.CallbackQuery.Data}") }
                            });

            client.SendTextMessageAsync(Message.Chat.Id, $"Название: {currentEvent.Name}\nОписание: {currentEvent.Description}" +
                $"\nВремя: {currentEvent.Happen_date}\nМесто: {currentEvent.Place}", parseMode: default, false, false, 0, adm);
        }
    }
}
