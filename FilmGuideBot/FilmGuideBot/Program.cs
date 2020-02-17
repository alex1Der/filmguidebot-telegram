using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace FilmGuideBot
{
    class Program
    {
        static TelegramBotClient Bot;

        static void Main(string[] args)
        {
            Bot = new TelegramBotClient("957999485:AAF6VFdUAv2oW5NbNUVy0V-aU18z3X6oHvo");

            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;

            var me = Bot.GetMeAsync().Result;

            Console.WriteLine(me.FirstName);

            Bot.StartReceiving();
            Console.ReadLine();
            Bot.StopReceiving();
        }

        private static void BotOnCallbackQueryReceived(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static async void BotOnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;

            if (message == null || message.Type != MessageType.TextMessage)
                return;

            string name = $"{message.From.FirstName} {message.From.LastName}";

            Console.WriteLine($"{name} just sent message: '{message.Text}'");

            switch (message.Text)
            {
                case "/start":
                    string text =
                        @"Command list:
                        /start - start bot
                        /inline - output menu
                        /keyboard - output keyboard";
                    await Bot.SendTextMessageAsync(message.From.Id, text);
                    break;
                case "/keyboard":
                    break;
                case "/inline":
                    break;
                default:
                    break;
            }
        }
    }
}
