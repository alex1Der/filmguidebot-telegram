using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types;

namespace FilmGuideBot
{
    class Program
    {
        static TelegramBotClient Bot;

        static void Main(string[] args)
        {
            Bot = new TelegramBotClient("HERE SHOULD BE YOUR TOKEN");

            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;

            var me = Bot.GetMeAsync().Result;

            Console.WriteLine(me.FirstName);

            Bot.StartReceiving();
            Console.ReadLine();
            Bot.StopReceiving();
        }

        private static async void BotOnCallbackQueryReceived(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            string buttonText = e.CallbackQuery.Data;
            string name = $"{e.CallbackQuery.From.FirstName} {e.CallbackQuery.From.LastName}";

            Console.WriteLine($"{name} pressed button called {buttonText}");

            try
            {
                await Bot.AnswerCallbackQueryAsync(e.CallbackQuery.Id, $"You pressed the button called {buttonText}");
            }
            catch
            {

            }
        }

        private static async void BotOnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;

            if (message == null || message.Type != MessageType.Text)
                return;

            string name = $"{message.From.FirstName} {message.From.LastName}";

            Console.WriteLine($"{name} just sent message: '{message.Text}'");

            switch (message.Text)
            {
                case "/start":
                    string text =
                        @"Command list:
/start - start bot 
/genre_selection - contains list of film genres";

                    await Bot.SendTextMessageAsync(message.From.Id, text);
                    break;
                case "/genre_selection":

                    ShowFilmGenres(message);
                    break;
                default:
                    break;
            }
        }

        async internal static void ShowFilmGenres(Message message)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Comedy"),
                            InlineKeyboardButton.WithCallbackData("Adventure"),
                            InlineKeyboardButton.WithCallbackData("Cartoons")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Horror"),
                            InlineKeyboardButton.WithCallbackData("Thriller"),
                            InlineKeyboardButton.WithCallbackData("Crime")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Drama"),
                            InlineKeyboardButton.WithCallbackData("Fantasy"),
                            InlineKeyboardButton.WithCallbackData("Hirstorical")
                        }
                    });
            await Bot.SendTextMessageAsync(message.From.Id, "Choose your destiny:", replyMarkup: inlineKeyboard);
        }
    }
}
