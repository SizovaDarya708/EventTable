using EventTable.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EventTable.Models.Commands
{
    /// <summary>
    /// Получение подписавшихся на определенное событие
    /// </summary>
    class GetEventSubscribersCommand : Command
    {
        public override List<string> Name => new List<string>() { "GetSubs" };

        public override void Execute(Update update, TelegramBotClient client, Exception e = null)
        {
            var Message = update.Message ?? update.CallbackQuery.Message;

            try
            {
                var eventId = update.CallbackQuery.Data.Split(":");
                var id = eventId.Length - 1;

                DataBaseHelper db = new DataBaseHelper();
                var ev = db.GetAllEvents().Where(x => x.Id == Convert.ToInt32(eventId[id])).FirstOrDefault();

                var users = db.GetEventSubs(ev.Id);
                string list = $"Общее количество записавшихся на ваше событие: {users.Count}";
                foreach (var u in users)
                {
                    list = list + $"\n{u.Login}";                
                }
                if (users.Count == 0) list = "Еще никто не записался на ваше событие, расскажите о нем друзьям!";

                client.SendTextMessageAsync(Message.Chat.Id, list, parseMode: default, false, false, 0);
            }
            catch (Exception exc)
            {
            }
        }
    }
}
