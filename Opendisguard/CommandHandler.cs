using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

    internal class CommandHandler
    {

    public static async Task Handler(SocketSlashCommand command)
    {
       var b = Database.GetVerificationCode(command.User.Id, (ulong)command.GuildId);
       await command.RespondAsync($"Your code was {b}");
    }
    }

