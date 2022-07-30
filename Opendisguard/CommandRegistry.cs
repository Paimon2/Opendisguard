using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    internal class CommandRegistry
    {

    public static async void RegisterCommands(DiscordSocketClient _client)
    {
        var globalCommand = new SlashCommandBuilder();

        // What roles to add after verification?
        globalCommand.WithName("captcha-verify-roles");
        globalCommand.WithDescription("This is my first global slash command");
        globalCommand.AddOption("roles",
                                ApplicationCommandOptionType.Role,
                                "The roles you would like to add after verification",
                                isRequired: true);

        var verifyDMMessage = new SlashCommandBuilder();

        // What roles to add after verification?
        verifyDMMessage.WithName("captcha-verify-dm-message");
        verifyDMMessage.WithDescription("What message should the bot DM after joining the server?");
        verifyDMMessage.AddOption("msg",
                                ApplicationCommandOptionType.String,
                                "The message to DM new users",
                                isRequired: true);


        var captchaDifficulty = new SlashCommandBuilder();
        // How difficult should the CAPTCHA be?
        captchaDifficulty.WithName("captcha-difficulty");
        captchaDifficulty.WithDescription("Configure how difficult the CAPTCHA should be to solve");
        captchaDifficulty.AddOption("difficulty",
                                ApplicationCommandOptionType.Integer,
                                "Difficulty: 1 to 10, where 10 is the hardest to solve",
                                isRequired: true);


        // Add commands!
        await _client.CreateGlobalApplicationCommandAsync(globalCommand.Build());
        await _client.CreateGlobalApplicationCommandAsync(verifyDMMessage.Build());
        await _client.CreateGlobalApplicationCommandAsync(captchaDifficulty.Build());

    }
}

