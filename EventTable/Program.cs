using EventTable.Models;
using MihaZupan;
using System;
using System.Net.Http;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace EventTable
{
	class Program
	{
		private static TelegramBotClient client;
		static void Main(string[] args)
		{
			client = Bot.Get();
			//client.StartReceiving();

			var commands = Bot.Commands;
			//var lastMessageId = 0;
			//var message = client.GetUpdatesAsync().Result;

			try
			{
				int offset = 0;
				while (true)
				{
					// получаем массив обновлений
					var updates = client.GetUpdatesAsync(offset).Result;
					client.SetWebhookAsync("");

					foreach (var update in updates)
					{
						foreach (var command in commands)
						{
							//Здесь идет сопоставление пришедших комманд с существующими 
							//Происходит их выполнение

							if (command.Name.Contains(update.Message.Text))
							{
								command.Execute(update.Message, client);
								break;
							}
						}
						offset = update.Id + 1;
					}

				}
				//if (message.Length > 0)
				//{
				//	var last = message[message.Length - 1];
				//	if (lastMessageId != last.Id && last.Message != null)
				//	{

				//		foreach (var command in commands)
				//		{
				//			if (command.Name.Contains(last.Message.Text))
				//			{
				//				command.Execute(last.Message, client);
				//				break;
				//			}
				//		}
				//		lastMessageId = last.Id;
				//		Console.ReadLine();
				//		client.StopReceiving();
				//	}
				//}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}