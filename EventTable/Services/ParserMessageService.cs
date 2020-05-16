using EventTable.Models.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Telegram.Bot.Types;

namespace EventTable.Services
{
    public class ParserMessageService
    {
        public static Event ParseMessageToNewEvent(Update update)
        {
            /* "Новое событие  Новый год!  01.01.21  ноовый год к нам мчится, скоооро...  ул.Добрый вечер"*/
            
            var evAr = update.Message.Text.Split("  ");

            CultureInfo MyCultureInfo = new CultureInfo("ru-RU");
            string MyString = evAr[2];
            DateTime MyDateTime = DateTime.Parse(MyString, MyCultureInfo);

            // DateTime d = DateTime.ParseExact(evAr[2], "DD/MM/YY", CultureInfo.CurrentCulture);
            return new Event()
            {
                Id = update.Message.MessageId,
                Name = evAr[1],
                Happen_date = MyDateTime,
                Description = evAr[3],
                Place = evAr[4]              
            };        
        }
    }
}
