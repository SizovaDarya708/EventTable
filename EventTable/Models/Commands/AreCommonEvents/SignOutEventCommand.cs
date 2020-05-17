using EventTable.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EventTable.Models.Commands
{
    /// <summary>
    /// Отписка пользователем от события
    /// </summary>
    class SignOutEventCommand : Command
    {
        public override List<string> Name => new List<string>() { "SignOutEvent" };

        public override void Execute(Update update, TelegramBotClient client, Exception e = null)
        {
            var Message = update.Message ?? update.CallbackQuery.Message;

            try
            {
                var evn = update.CallbackQuery.Data.Split(":");
                var id = evn.Length - 1;
                var eventId = evn[id];

                DataBaseHelper db = new DataBaseHelper();
                var ev = db.GetAllEvents().Where(x => x.Id == Convert.ToInt32(eventId)).FirstOrDefault();

                db.RemoveEvent(ev, Message.Chat.Id);

                client.SendTextMessageAsync(Message.Chat.Id, $"Вы отписались от события {ev.Name}", parseMode: default, false, false, 0);
            }
            catch (Exception exc)
            {
                client.SendTextMessageAsync(Message.Chat.Id, $"Вы уже отписались от события", parseMode: default, false, false, 0);
            }
        }
    }
}
