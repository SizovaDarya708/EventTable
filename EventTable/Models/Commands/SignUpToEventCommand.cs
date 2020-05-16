using EventTable.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EventTable.Models.Commands
{
    class SignUpToEventCommand : Command
    {
        public override List<string> Name => new List<string>() { "SignUp" };

        public override void Execute(Update update, TelegramBotClient client, Exception e = null)
        {
            var Message = update.Message ?? update.CallbackQuery.Message;

            try{
                var eventId = update.CallbackQuery.Data.Split(":")[1];

                DataBaseHelper db = new DataBaseHelper();
                var ev = db.GetAllEvents().Where(x => x.Id == Convert.ToInt32(eventId)).FirstOrDefault();

                db.AddEvent(ev, Message.Chat.Id);

                client.SendTextMessageAsync(Message.Chat.Id, $"Вы успешно записаны на событие {ev.Name}", parseMode: default, false, false, 0);
            }
            catch(Exception exc)
            {
                client.SendTextMessageAsync(Message.Chat.Id, $"Вы не можете записаться на свое событие!", parseMode: default, false, false, 0);
            }

        }
    }
}
