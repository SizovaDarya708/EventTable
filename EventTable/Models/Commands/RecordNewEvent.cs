﻿using EventTable.Helpers;
using EventTable.Models.Entities;
using EventTable.Services;
using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EventTable.Models.Commands
{
    class RecordNewEvent : Command
    {
        public override List<string> Name => new List<string>() { "Запись события в бд", "Новое событие" };
        public override void Execute(Update update, TelegramBotClient client, Exception e = null)
        {
            var chatId = update.Message.Chat.Id;

            try
            {
                //Создание события
                var Event = ParserMessageService.ParseMessageToNewEvent(update);
                // Добавление события в БД
                DataBaseHelper db = new DataBaseHelper();
                db.AddEvent(Event, update.Message.From.Id);

                client.SendTextMessageAsync(chatId, "Событие ", parseMode: default, false, false, 0);
            }
            catch(Exception exc) 
            {
                client.SendTextMessageAsync(chatId, $"Не удалось создать событие, напиши текст по шаблону, приятель {exc.Message}", parseMode: default, false, false, 0);
            }            
        }
    }
}
