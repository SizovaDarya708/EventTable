using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using EventTable.Models.Commands;

namespace EventTable.Models
{
    public static class Bot
    {
        private static TelegramBotClient client;
        private static List<Command> commandsList;

        public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();

        public static async Task<TelegramBotClient> Get()
        {
            if (client != null)
            {
                return client;
            }

            commandsList = new List<Command>();
            //Регистрация комманд
            commandsList.Add(new HelloCommand());
            //TODO: Add more commands

            client = new TelegramBotClient(AppSettings.Key);
            var hook = string.Format(AppSettings.Url, "api/message/update");
            await client.SetWebhookAsync(hook);

            return client;
        }

    }
}