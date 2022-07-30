using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    internal class DMHandler
    {

    private static bool IsPrivateMessage(SocketMessage msg)
    {
        return (msg.Channel.GetType() == typeof(SocketDMChannel));
    }

    public static async Task OnMessage(SocketMessage msg)
    {
        if (!IsPrivateMessage(msg))
            return;

        String captchaCode = Database.GetVerificationCode(msg.Author.Id);
        if (captchaCode == null)
            return;

        Console.WriteLine(captchaCode);
        if(msg.Content != captchaCode)
        {
            await msg.Channel.SendMessageAsync("Wrong captcha! Try again in 10 seconds please.");
            await Task.Delay(10000);
            MemberJoinActivities.ResendAndStoreCaptcha(msg.Author);
            return;
        }

        else
        {
            await msg.Channel.SendMessageAsync("Thank you, you have been successfully verified!");
        }


    }
    }

