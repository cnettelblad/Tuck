using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Text.RegularExpressions;
using Tuck.Readers;
using Tuck.Model;

namespace Tuck.Services
{
    public class CommandHandlingService
    {
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _discord;
        private readonly IServiceProvider _services;
        private readonly Random _random;

        public CommandHandlingService(IServiceProvider services)
        {
            _commands = services.GetRequiredService<CommandService>();
            _discord = services.GetRequiredService<DiscordSocketClient>();
            _services = services;
            _random = new Random();

            _commands.AddTypeReader(typeof(BuffType), new BuffTypeReader());
            _commands.AddTypeReader(typeof(DateTime), new DateTimeReader(), true);

            _commands.CommandExecuted += CommandExecutedAsync;
            _discord.MessageReceived += MessageReceivedAsync;
        }

        public async Task InitializeAsync()
        {
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        public async Task MessageReceivedAsync(SocketMessage rawMessage)
        {
            // Ignore system messages, or messages from other bots
            if (!(rawMessage is SocketUserMessage message)) return;
            if (message.Source != MessageSource.User) return;

            // Some easter eggs for BoB
            if((message.Channel as IGuildChannel)?.Guild.Id == 610455239757266955) {
                if(message.Author.Id == 410557139719946240 && _random.Next(2) == 0) {
                    await message.Channel.SendMessageAsync("KUJAMBEH IS A FAT BEAR macro");
                }
                if(message.Author.Id == 138775639976050689 && _random.Next(10) == 0) {
                    await message.Channel.SendMessageAsync("Fuck you Sena");
                }
                if(message.Content.Contains("Death's Sting") || message.Content.Contains("item=21126")) {
                    await message.Channel.SendMessageAsync("That weapon should be prio to Mondino!");
                }
                if(_random.Next(100) == 0) {
                    var channel = await message.Author.GetOrCreateDMChannelAsync();
                    await channel.SendMessageAsync(ComplimentService.GetCompliment());
                }
            }

            // This value holds the offset where the prefix ends
            var argPos = 0;
            // Perform prefix check. You may want to replace this with
            // (!message.HasCharPrefix('!', ref argPos))
            // for a more traditional command format like !help.
            if (!message.HasCharPrefix('!', ref argPos)) return;

            var context = new SocketCommandContext(_discord, message);
            // Perform the execution of the command. In this method,
            // the command service will perform precondition and parsing check
            // then execute the command if one is matched.
            await _commands.ExecuteAsync(context, argPos, _services); 
            // Note that normally a result will be returned by this format, but here
            // we will handle the result in CommandExecutedAsync,
        }

        public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            // command is unspecified when there was a search failure (command not found); we don't care about these errors
            if (!command.IsSpecified)
                return;

            // the command was successful, we don't care about this result, unless we want to log that a command succeeded.
            if (result.IsSuccess)
                return;

            // the command failed, let's notify the user that something happened.
            await context.Channel.SendMessageAsync($"{result}");
        }
    }
}