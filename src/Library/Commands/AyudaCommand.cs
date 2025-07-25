﻿using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class AyudaCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("ayuda")]
    [Summary("Muestra la ayuda del juego.")]
    public async Task AyudaAsync()
    {
        string ayuda = Ayuda.ObtenerComandos();
        await ReplyAsync(ayuda);
    }
}