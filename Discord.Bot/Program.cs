using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

class Program
{
    static IBot bot = new TestBot();

    static void Main(string[] args) => MainAsync().Wait();

    static async Task MainAsync()
    {
        var client = new DiscordSocketClient();

        await client.LoginAsync(TokenType.Bot, bot.Token);
        await client.StartAsync();

        client.MessageReceived += Client_MessageReceived;

        Console.ReadLine();
    }

    static async Task Client_MessageReceived(SocketMessage arg)
    {
        if (arg.Author.Username.Equals(bot.Name))
        {
            // Avoide infinite loop.
            return;
        }
        if (arg.Channel.Name != "bot")
        {
            // Only #bot channel is allowed.
            return;
        }
        await arg.Channel.SendMessageAsync(string.Format("I'm {0}", bot.Name));
    }
}