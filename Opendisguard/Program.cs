using System;
using OpenCvSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using Discord.WebSocket;

namespace MyApp // Note: actual namespace depends on the project name.
{

    internal class Program
    {

        private DiscordSocketClient _client;
       

        public static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            String token = "";

            try
            {
                token = File.ReadAllText("token.txt");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("ERR: token.txt with valid token not found!");
                Environment.Exit(1);
            }

            // Add gateway intents
            var socketConfig = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers | GatewayIntents.GuildBans
            };
            
            _client = new DiscordSocketClient(socketConfig);

            // Add handlers
            _client.Ready += Client_Ready;
            _client.SlashCommandExecuted += CommandHandler.Handler;
            _client.UserJoined += MemberJoinActivities.OnMemberJoin;
            _client.MessageReceived += DMHandler.OnMessage;


            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }


        public async Task Client_Ready()
        {
            Database.CheckCreateTables();
            CommandRegistry.RegisterCommands(_client);
           
        }
    }
}