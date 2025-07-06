using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using Library.Domain;

namespace Library.Services;

/// <summary>
/// Esta clase implementa el bot de Discord.
/// </summary>
public class Bot : IBot
{
    private readonly JuegoFachada _fachada;
    private IServiceProvider? serviceProvider;
    private readonly ILogger<Bot> logger;
    private readonly IConfiguration configuration;
    private readonly DiscordSocketClient client;
    private readonly CommandService commands;
    private System.Threading.Timer? updateTimer;

    public Bot(ILogger<Bot> logger, IConfiguration configuration, JuegoFachada fachada)
    {
        this.logger = logger;
        this.configuration = configuration;
        this._fachada = fachada;
        DiscordSocketConfig config = new()
        {
            AlwaysDownloadUsers = true,
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
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
        updateTimer = new System.Threading.Timer(_ =>
        {
            logger.LogInformation("Timer ejecutado");
            var jugadores = _fachada.ObtenerJugadores();
            if (jugadores == null)
            {
                logger.LogWarning("La lista de jugadores es null.");
                return;
            }
            foreach (var jugador in jugadores)
            {
                jugador.ActualizarUnidades();
                jugador.ActualizarConstrucciones();
            }
        }, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        await client.LoginAsync(TokenType.Bot, discordToken);
        await client.StartAsync();

        client.MessageReceived += HandleCommandAsync;
        client.Ready += OnReadyAsync;
    }

    private async Task OnReadyAsync()
    {
        string mensaje = "Bienvenido al juego de estrategia en tiempo real. Ejecute el comando '!ayuda' para ver la lista de comandos disponibles.";

        foreach (SocketGuild guild in client.Guilds)
        {
            IEnumerable<SocketTextChannel> generalChannels = guild.TextChannels
                .Where(c => c.Name.Equals("general", StringComparison.OrdinalIgnoreCase));

            foreach (SocketTextChannel channel in generalChannels)
            {
                await channel.SendMessageAsync(mensaje);
            }
        }
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