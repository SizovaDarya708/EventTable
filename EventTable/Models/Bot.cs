using EventTable.Models.Commands;
using MihaZupan;
using System.Collections.Generic;
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
            //var proxy = new HttpToSocks5Proxy("62.210.92.188", 5950);
            //var proxy = new HttpToSocks5Proxy("54.38.195.161", 52060);
            var proxy = new HttpToSocks5Proxy("54.38.195.161", 63525);
            client = new TelegramBotClient(AppSettings.Key, proxy);

            commandsList = new List<Command>();
            //Регистрация комманд
            commandsList.Add(new HelloCommand());
            commandsList.Add(new MenuCommand());
            commandsList.Add(new CreateNewEventCommand());
            commandsList.Add(new DescriptionCommand());
            commandsList.Add(new ErrorMessageComand());
            commandsList.Add(new AdministrationMenuCommand());
            commandsList.Add(new RecordNewEventCommand());
            commandsList.Add(new EventListCommand());
            commandsList.Add(new GetEventCommand());
            commandsList.Add(new FutureEventsCommand());
            commandsList.Add(new GetMyEventCommand());
            commandsList.Add(new MyEventListCommand());


            //TODO: Add more commands

            if (client != null)
            {
                return client;
            }           
            
            return client;
        }
    }
}
