using EventTable.Models;
using System;
using Telegram.Bot;
using EventTable.Helpers;
using System.Linq;
using EventTable.Models.Entities;
using Telegram.Bot.Types;
using EventTable.Models.Commands;

namespace EventTable
{
	class Program
	{
		private static TelegramBotClient client;
		static void Main(string[] args)
		{
			//var usr = new Models.Entities.User("login", 111);
			//var evnt = new Event();
			//DataBaseHelper.AddEventToUser(evnt, 111);
			var db = new EventTable.Data.ApplicationDbContext();
			client = Bot.Get();
			var commands = Bot.Commands;

			int offset = 0;
			while (true)
			{
				// получаем массив обновлений
				var updates = client.GetUpdatesAsync(offset).Result;
				client.SetWebhookAsync("");
				//Перебор полученных обновлений
				foreach (var update in updates)
				{
					DataBaseHelper helper = new DataBaseHelper();
					if (update.CallbackQuery?.Data == "/start" || update.Message?.Text == "/start") {
						try
						{
							if (!helper.UserExist(update.Message.From.Id))
							{
								helper.AddUser(new Models.Entities.User()
								{
									ChatId = update.Message.From.Id,
									Login = update.Message.From.Username
								});
							}
						}
						catch (Exception exc) { throw new Exception(exc.Message); }

					}

					var userCommand = update.CallbackQuery?.Data ?? update.Message?.Text;

					if (userCommand.StartsWith("Новое событие")) new RecordNewEvent().Execute(update, client);
					if (userCommand.StartsWith("MyEvents")) new GetEventCommand().Execute(update, client);
					if (Int32.TryParse(update.CallbackQuery?.Data, out int number)) new GetEventCommand().Execute(update, client);
					if (userCommand.StartsWith("SignUp")) new SignUpToEventCommand().Execute(update, client);
					
					foreach (var command in commands)
					{
						//Здесь идет сопоставление пришедших комманд с существующими 
						//Происходит их выполнениe
						try
						{
							if (command.Name.Contains(userCommand))
							{
								command.Execute(update, client);
								break;
							}
						}
						catch (Exception e)
						{
							var errorCommand = commands.Where(c => c.Name.Contains("Error"))
								.FirstOrDefault();
							errorCommand.Execute(update, client, e);
						}
					}
					offset = update.Id + 1;

				}
			}
		}
	}
}