using EventTable.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace EventTable.Models.Commands
{
    class MyEventListCommand : Command
    {
        public override List<string> Name => new List<string>() { "MyEventList", "Мои события", "Созданные мной события" };

        public override void Execute(Update update, TelegramBotClient client, Exception e = null)
        {
            var Message = update.Message ?? update.CallbackQuery.Message;

            DataBaseHelper db = new DataBaseHelper();
            var eventList = db.GetUserEvents(Message.Chat.Id, 1);

            if (eventList.Count != 0)
            {
                var events = InlineKeyBoardHelper.CreateInlineKeyboardButtonForMyEvents(eventList, eventList.Count / 10);

                client.SendTextMessageAsync(Message.Chat.Id, "Список событий, которые вы создали", parseMode: default, false, false, 0, events);
            }
            else
            {
                var menu = new InlineKeyboardMarkup(new[]
                            {
                                new[] {InlineKeyboardButton.WithCallbackData("Создание событий", "CreateEvent") }
                            });

                client.SendTextMessageAsync(Message.Chat.Id, "У вас еще нет созданных событий, но вы всегда можете их создать:",
                    parseMode: default, false, false, 0, menu);
            }
        }
    }
}
