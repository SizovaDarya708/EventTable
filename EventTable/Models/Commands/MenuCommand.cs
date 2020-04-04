using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace EventTable.Models.Commands
{
    /// <summary>
    /// Класс-команда для открытия главного меню
    /// </summary>
    class MenuCommand : Command
    {
        public override List<string> Name => new List<string>() { "Меню", "Menu", "Главное меню" };

        public override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            var menu = new InlineKeyboardMarkup(new[]
                            {
                                new[] {InlineKeyboardButton.WithCallbackData("Список событий","EventList") },
                                new[] {InlineKeyboardButton.WithCallbackData("Создание событий","CreateEvent") },
                                new[] {InlineKeyboardButton.WithCallbackData("Администрирование событий","AdminMenu") }
                            });

            client.SendTextMessageAsync(chatId, "А вот и меню - получай", parseMode: default, false, false, 0, menu);
        }
    }
}
