﻿using EventTable.Models;
using System;
using Telegram.Bot;
using EventTable.Helpers;
using System.Linq;

namespace EventTable
{
	class Program
	{
		private static TelegramBotClient client;
		static void Main(string[] args)
		{
			//var connection = DataBaseHelper.GetConnection();
			//DataBaseHelper.AddUser("Albert", "Astrakhan");


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
					foreach (var command in commands)
					{
						//Здесь идет сопоставление пришедших комманд с существующими 
						//Происходит их выполнение
						try
						{
							if (command.Name.Contains(update.Message.Text))
							{
								command.Execute(update, client);
								break;
							}
							else if (update.Message != null && command.Name.Contains("Описание"))
							{
								command.Execute(update, client);
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