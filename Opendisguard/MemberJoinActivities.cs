using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

    internal class MemberJoinActivities
    {

    private static async void SendCaptcha(SocketGuildUser user)
    {
        Mat captcha = Captcha.getCaptchaImage();

        var dmChannel = await user.CreateDMChannelAsync();

        await dmChannel.SendMessageAsync("Configurable text");
        await dmChannel.SendFileAsync(captcha.ToMemoryStream(), "captcha.png");
    }

    public static async Task OnMemberJoin(SocketGuildUser gUser)
    {
  
        if (gUser.IsBot || gUser.IsWebhook) return;
        SendCaptcha(gUser);
        

    }


    }

