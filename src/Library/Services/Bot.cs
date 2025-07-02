using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Library.Services;

/// <summary>
/// Esta clase implementa el bot de Discord.
/// </summary>


public class Bot : IBot
{
    private IServiceProvider? serviceProvider;
    private readonly ILogger<Bot> logger;
    private readonly IConfiguration configuration;
    private readonly DiscordSocketClient client;
    private readonly CommandService commands;
    
    public Bot(ILogger<Bot> logger, IConfiguration configuration)
    {
        this.logger = logger;
        this.configuration = configuration;

        DiscordSocketConfig config = new()
        {
            AlwaysDownloadUsers = true,
            GatewayIntents = 
                GatewayIntents.AllUnprivileged
                | GatewayIntents.MessageContent/*
                | GatewayIntents.GuildMembers*/
        };

        client = new DiscordSocketClient(config);
        commands = new CommandService();
    }

    public async Task StartAsync(IServiceProvider services)
    {
        string discordToken = configuration["DiscordToken"] ?? throw new Exception("Falta el token");

        logger.LogInformation("Iniciando el con token {Token}", discordToken);

        serviceProvider = services;

        await commands.AddModulesAsync(Assembly.GetExecutingAssembly(), serviceProvider);

        await client.LoginAsync(TokenType.Bot, discordToken);
        await client.StartAsync();

        client.MessageReceived += HandleCommandAsync;
    }


    public async Task StopAsync()
    {
        logger.LogInformation("Finalizando");
        await client.LogoutAsync();
        await client.StopAsync();
    }

    private async Task HandleCommandAsync(SocketMessage arg)
    {
        if (arg is not SocketUserMessage message || message.Author.IsBot)
        {
            return;
        }
        
        int position = 0;
        bool messageIsCommand = message.HasCharPrefix('!', ref position);

        
        if (messageIsCommand)
        {
            SocketCommandContext context = new SocketCommandContext(client, message);
            IResult result = await commands.ExecuteAsync(context, position, serviceProvider);

            if (!result.IsSuccess && result.Error == CommandError.UnknownCommand)
            {
                await context.Channel.SendMessageAsync("Comando no reconocido.");
            }
        }
    }
}