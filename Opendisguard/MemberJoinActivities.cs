using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

    internal class MemberJoinActivities
    {

    public static async void ResendAndStoreCaptcha(SocketUser user)
    {
        Captcha.CaptchaDetails captcha = Captcha.GenerateNewCaptcha();
        // TODO Add user ID and captcha text to DB

        var dmChannel = await user.CreateDMChannelAsync();

        await dmChannel.SendMessageAsync("Configurable text");
        await dmChannel.SendFileAsync(captcha.image.ToMemoryStream(), "captcha.png");

        Database.AddVerificationCode(user.Id, captcha.text);
    }

    private static async void SendAndStoreCaptcha(SocketGuildUser user)
    {
        Captcha.CaptchaDetails captcha = Captcha.GenerateNewCaptcha();


        var dmChannel = await user.CreateDMChannelAsync();

        await dmChannel.SendMessageAsync("Configurable text");
        await dmChannel.SendFileAsync(captcha.image.ToMemoryStream(), "captcha.png");

        Database.AddVerificationCode(user.Id, captcha.text);
    }

    public static async Task OnMemberJoin(SocketGuildUser gUser)
    {
  
        if (gUser.IsBot || gUser.IsWebhook) return;

        SendAndStoreCaptcha(gUser);
        

    }


    }

