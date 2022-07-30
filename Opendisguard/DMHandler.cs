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
    }


    }

