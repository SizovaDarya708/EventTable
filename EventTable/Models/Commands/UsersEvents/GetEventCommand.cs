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
    /// Вывод описания определенного события
    /// </summary>
    class GetEventCommand : Command
    {
        public override List<string> Name => new List<string>() { "Определенное событие" };

        public override void Execute(Update update, TelegramBotClient client, Exception e = null)
        {
            var Message = update.Message ?? update.CallbackQuery.Message;

            DataBaseHelper db = new DataBaseHelper();
            var events = db.GetAllEvents();

            var currentEvent = events.Where(x => x.Id == Convert.ToInt32(update.CallbackQuery.Data)).FirstOrDefault();

            var sinUp = new InlineKeyboardMarkup(new[]
                            {
                                new[] {InlineKeyboardButton.WithCallbackData("Записаться", $"SignUp:{update.CallbackQuery.Data}") }
                            });

            client.SendTextMessageAsync(Message.Chat.Id, $"Название: {currentEvent.Name}\nОписание: {currentEvent.Description}" +
                $"\nВремя: {currentEvent.Happen_date}\nМесто: {currentEvent.Place}", parseMode: default, false, false, 0, sinUp);
        }
    }
}
