using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace EventTable.Models.Commands
{
    class AdministrationMenuCommand : Command
    {
        public override List<string> Name => new List<string>() { "AdministrationMenu", "Администрирование", "Я администратор" };

        public override void Execute(Update update, TelegramBotClient client, Exception? e)
        {
            var Message = update.Message ?? update.CallbackQuery.Message;

            var menu = new InlineKeyboardMarkup(new[]
                            {
                                new[] {InlineKeyboardButton.WithCallbackData("Мои события","MyEventList") }
                            });

            client.SendTextMessageAsync(Message.Chat.Id, "Администрируй на здоровье, босс", parseMode: default, false, false, 0, menu);
        }
    }
}
