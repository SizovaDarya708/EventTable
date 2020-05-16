﻿using EventTable.Models.Commands;
using MihaZupan;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace EventTable.Models
{
    public class Bot
    {
        public static IReadOnlyList<Command> Commands => commandsList?.AsReadOnly();

        private static TelegramBotClient client;

        /// <summary>
        /// Список команд бота
        /// </summary>
        private static List<Command> commandsList;

        public static TelegramBotClient Get()
        {
            var proxy = new HttpToSocks5Proxy("54.38.195.161", 37683);
            client = new TelegramBotClient(AppSettings.Key, proxy);

            commandsList = new List<Command>();
            //Регистрация комманд
            commandsList.Add(new HelloCommand());
            commandsList.Add(new MenuCommand());
            commandsList.Add(new CreateNewEventCommand());
            commandsList.Add(new DescriptionCommand());
            commandsList.Add(new ErrorMessageComand());
            commandsList.Add(new AdministrationMenuCommand());
            commandsList.Add(new RecordNewEvent());
            commandsList.Add(new EventListCommand());
            commandsList.Add(new GetEventCommand());


            //TODO: Add more commands

            if (client != null)
            {
                return client;
            }           
            
            return client;
        }
    }
}
