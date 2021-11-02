using CryptoScraping;
using Discord;
using Discord.WebSocket;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    enum Command
    {
        Get_Price_BTC = 1,
        Get_Price_ETH = 2,
        Get_Price_ADA = 3,
        Get_Price_SOL = 4,
        Help = 5
        

    }

    class Program
    {
        private DiscordSocketClient _client;

        private ICryptoPriceScraper priceScraper;
        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
            Console.WriteLine("Hello World!");
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        private async Task MainAsync()
        {
            priceScraper = new HTMLScraper();
             _client = new DiscordSocketClient();

            _client.Log += Log;
            _client.MessageReceived += _client_MessageReceived;
            //  You can assign your bot token to a string, and pass that in to connect.
            //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
            var token = "your token";

            // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
            // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            // var token = File.ReadAllText("token.txt");
            // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            JoinServer("https://discord.gg/MdBQUhAF");
            await Task.Delay(-1);
            
        }

        private Task _client_MessageReceived(SocketMessage arg)
        {
            if (!arg.Author.IsBot)
            {
               
                string textRecived = arg.Content;
                string textRespoce = null;
                if (textRecived == Command.Get_Price_BTC.ToString())
                {
                    //Scraper scraper = new Scraper();
                    var prices = priceScraper.GetPrices();
                    textRespoce = prices["bitcoin"];
                }
                else if (textRecived == Command.Get_Price_ETH.ToString())
                {
                    //Scraper scraper = new Scraper();
                    var prices = priceScraper.GetPrices();
                    textRespoce = prices["ethereum"];
                }
                else if (textRecived == Command.Get_Price_ADA.ToString())
                {
                    //Scraper scraper = new Scraper();
                    var prices = priceScraper.GetPrices();
                    textRespoce = prices["cardano"];
                }
                else if (textRecived == Command.Get_Price_SOL.ToString())
                {
                    //Scraper scraper = new Scraper();
                    var prices = priceScraper.GetPrices();
                    textRespoce = prices["solana"];
                }
                else if (textRecived == Command.Help.ToString())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Available commands");
                    sb.AppendLine(Command.Get_Price_ADA.ToString());
                    sb.AppendLine(Command.Get_Price_BTC.ToString());
                    sb.AppendLine(Command.Get_Price_ETH.ToString());
                    sb.AppendLine(Command.Get_Price_SOL.ToString());
                    sb.AppendLine(Command.Help.ToString());
                    textRespoce = sb.ToString();
                }
                if (textRespoce != null && textRespoce.Length > 0)
                {
                    var chanel = arg.Channel;
                    return chanel.SendMessageAsync(textRespoce);
                    
                }
                else
                {
                    return Task.CompletedTask;
                }
            }
            else
            {
                return Task.CompletedTask;
            }
        }
        public void JoinServer(string id)
        {
          var ress =  _client.GetInviteAsync(id).Result;
            if (ress != null)
            {
                var chanelId = ress.ChannelId;

                var chanel = _client.GetChannel(chanelId);
            }
        }
    }
}
