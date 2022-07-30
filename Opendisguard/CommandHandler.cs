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
        Mat captcha = Captcha.getCaptchaImage();
        await command.RespondAsync($"You executed {command.Data.Name}");
        await command.Channel.SendFileAsync(captcha.ToMemoryStream(), "image.jpeg", "Text");
    }
    }

