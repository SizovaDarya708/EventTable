using EventTable.Helpers;
using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace EventTable.Models.Commands
{
    class FutureEventsCommand : Command
    {
        public override List<string> Name => new List<string>() { "Будущие событий", "FutureEvents"};

        public override void Execute(Update update, TelegramBotClient client, Exception e = null)
        {
            var Message = update.Message ?? update.CallbackQuery.Message;

            DataBaseHelper db = new DataBaseHelper();
            var eventList = db.GetUserEvents(Message.Chat.Id, 0);

            if (eventList.Count != 0)
            {
                var events = InlineKeyBoardHelper.CreateInlineKeyboardButtonForMyEvents(eventList, eventList.Count / 10);

                client.SendTextMessageAsync(Message.Chat.Id, "Список событий, на которые вы записаны:", parseMode: default, false, false, 0, events);
            }
            else 
            {
                var menu = new InlineKeyboardMarkup(new[]
                            {
                                new[] {InlineKeyboardButton.WithCallbackData("Список событий","EventList")}
                            });

                client.SendTextMessageAsync(Message.Chat.Id, "Список событий, на которые вы записаны пуст,\n Может среди этих событий вы найдете интересные для вас:",
                    parseMode: default, false, false, 0, menu);
            }
        }
    }
}
