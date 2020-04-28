using EventTable.Models;
using MihaZupan;
using System;
using System.Net.Http;
using Telegram.Bot;
using Npgsql;
using EventTable.Helpers;
using EventTable.Models.Commands;

namespace EventTable
{
	class Program
	{
		private static TelegramBotClient client;
		static void Main(string[] args)
		{
			var connection = DataBaseHelper.GetConnection();
			DataBaseHelper.AddUser("Albert", "Astrakhan");


			client = Bot.Get();
			var commands = Bot.Commands;

			try
			{
				int offset = 0;
				while (true)
				{
					// получаем массив обновлений
					var updates = client.GetUpdatesAsync(offset).Result;
					client.SetWebhookAsync("");
					//Перебор полученных обновлений
					foreach (var update in updates)
					{
						foreach (var command in commands)
						{
							//Здесь идет сопоставление пришедших комманд с существующими 
							//Происходит их выполнение
							if (command.Name.Contains(update.Message.Text) || command.Name.Contains(update.CallbackQuery.Data))
							{
								command.Execute(update.Message, client);
								break;
							}
							//else(new DescriptionCommand().Execute(update.Message, client));
						}
						offset = update.Id + 1;
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}