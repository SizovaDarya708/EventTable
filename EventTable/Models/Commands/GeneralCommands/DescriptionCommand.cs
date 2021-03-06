﻿using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EventTable.Models.Commands
{
    /// <summary>
    /// Общее описание бота
    /// </summary>
    public class DescriptionCommand : Command
    {
        public override List<string> Name => new List<string>() { "Description", "Описание", "Для чего ты?" };

        public override void Execute(Update update, TelegramBotClient client, Exception e = null)
        {
            var chatId = update.Message.Chat.Id;

            client.SendTextMessageAsync(chatId, "Я бот, который позволит тебе создавать события, приглашать туда друзей," +
                " а также записываться на другие события. Напиши мне *Меню*, чтобы просмотреть, что я умею", parseMode: default, false, false, 0);
        }
    }
}
