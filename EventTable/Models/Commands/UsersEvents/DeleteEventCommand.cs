using EventTable.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EventTable.Models.Commands
{
    /// <summary>
    /// Удаление собственного события
    /// </summary>
    public class DeleteEventCommand : Command
    {
        public override List<string> Name => new List<string>() { "DeleteEvent" };

        public override void Execute(Update update, TelegramBotClient client, Exception e = null)
        {
            var Message = update.Message ?? update.CallbackQuery.Message;

            try
            {
                var eventId = update.CallbackQuery.Data.Split(":");
                var id = eventId.Length - 1;

                DataBaseHelper db = new DataBaseHelper();
                var ev = db.GetAllEvents().Where(x => x.Id == Convert.ToInt32(eventId[id])).FirstOrDefault();

                db.RemoveEvent(ev, Message.Chat.Id);

                client.SendTextMessageAsync(Message.Chat.Id, $"Вы успешно удалили событие: {ev.Name}", parseMode: default, false, false, 0);
            }
            catch (Exception exc)
            {
                client.SendTextMessageAsync(Message.Chat.Id, $"Вы уже удалили событие", parseMode: default, false, false, 0);
            }
        }
    }
}
