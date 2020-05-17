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
    /// Вывод определенного созданного события
    /// </summary>
    class GetMyEventCommand : Command
    {        
        public override List<string> Name => new List<string>() { "GetMyEvent", "Мои события", "Созданные мной события" };

        public override void Execute(Update update, TelegramBotClient client, Exception e = null)
        {
            var Message = update.Message ?? update.CallbackQuery.Message;

            DataBaseHelper db = new DataBaseHelper();
            var events = db.GetAllEvents();
            var ev = update.CallbackQuery.Data.Split(":")[1];

            var currentEvent = events.Where(x => x.Id == Convert.ToInt32(ev)).FirstOrDefault();

            var adm = new InlineKeyboardMarkup(new[]
                            {
                                new[] {InlineKeyboardButton.WithCallbackData("Просмотреть записавшихся", $"EventSubs:{update.CallbackQuery.Data}") },
                                 new[] {InlineKeyboardButton.WithCallbackData("Удалить событие", $"DeleteEvent:{update.CallbackQuery.Data}") }
                            });

            client.SendTextMessageAsync(Message.Chat.Id, $"Название: {currentEvent.Name}\nОписание: {currentEvent.Description}" +
                $"\nВремя: {currentEvent.Happen_date}\nМесто: {currentEvent.Place}", parseMode: default, false, false, 0, adm);
        }
    }
}
