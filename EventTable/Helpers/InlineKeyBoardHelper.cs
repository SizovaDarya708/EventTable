using EventTable.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace EventTable.Helpers
{
    public class InlineKeyBoardHelper
    {
        public static IReplyMarkup CreateInlineKeyboardButtonForMyEvents(List<Event> eventList, int columns)
        {
            if (columns == 0) columns = 1;
            int rows = (int)Math.Ceiling((double)eventList.Count / (double)columns);
            InlineKeyboardButton[][] buttons = new InlineKeyboardButton[rows][];

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = eventList
                    .Skip(i * columns)
                    .Take(columns)
                    .Select(direction => InlineKeyboardButton.WithCallbackData(
                       direction.Name, direction.Id.ToString()))
                    .ToArray();
            }
            return new InlineKeyboardMarkup(buttons);
        }

        public static IReplyMarkup CreateInlineKeyboardButtonForUserEvents(List<Event> eventList, int columns)
        {
            if (columns == 0) columns = 1;
            int rows = (int)Math.Ceiling((double)eventList.Count / (double)columns);
            InlineKeyboardButton[][] buttons = new InlineKeyboardButton[rows][];

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = eventList
                    .Skip(i * columns)
                    .Take(columns)
                    .Select(direction => InlineKeyboardButton.WithCallbackData(
                       direction.Name, $"MyEvent:{direction.Id.ToString()}"))
                    .ToArray();
            }
            return new InlineKeyboardMarkup(buttons);
        }

        public static IReplyMarkup CreateInlineKeyboardButtonForEditMyEvents(List<Event> eventList, int columns)
        {
            if (columns == 0) columns = 1;
            int rows = (int)Math.Ceiling((double)eventList.Count / (double)columns);
            InlineKeyboardButton[][] buttons = new InlineKeyboardButton[rows][];

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = eventList
                    .Skip(i * columns)
                    .Take(columns)
                    .Select(direction => InlineKeyboardButton.WithCallbackData(
                       direction.Name, $"GetMyEvent:{direction.Id.ToString()}"))
                    .ToArray();
            }
            return new InlineKeyboardMarkup(buttons);
        }

        public static IReplyMarkup CreateInlineKeyboardButtonForFutureEvents(List<Event> eventList, int columns)
        {
            if (columns == 0) columns = 1;
            int rows = (int)Math.Ceiling((double)eventList.Count / (double)columns);
            InlineKeyboardButton[][] buttons = new InlineKeyboardButton[rows][];

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = eventList
                    .Skip(i * columns)
                    .Take(columns)
                    .Select(direction => InlineKeyboardButton.WithCallbackData(
                       direction.Name, $"GetFutureEvents:{direction.Id.ToString()}"))
                    .ToArray();
            }
            return new InlineKeyboardMarkup(buttons);
        }
    }
}
