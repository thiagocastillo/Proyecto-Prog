using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class AyudaCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = new JuegoFachada();

    [Command("ayuda")]
    [Summary("Muestra la ayuda del juego.")]
    public async Task ExecuteAsync()
    {
        Ayuda.ObtenerComandos();
        await ReplyAsync(@"Comandos disponibles:

        - Crear una nueva partida:                         CrearPartida
        - Mostrar el mapa del juego:                       MostrarMapa
        - Ver civilizaciones disponibles:                  Civilizaciones
        - Agregar un jugador a la partida:                 AgregarJugador <nombreJugador> <civilización>
        - Construir un edificio:                           ConstruirEdificio <nombreJugador> <tipo> <x> <y>
        - Entrenar una unidad:                             EntrenarUnidad <nombreJugador> <tipo> <x> <y>
        - Ordenar a un aldeano recolectar un recurso:      Recolectar <nombreJugador> <idAldeano> <x> <y>
        - Mover una unidad:                                MoverUnidad <nombreJugador> <idUnidad> <x> <y>
        - Mover varias unidades en bloque:                 MoverUnidades <nombreJugador> <x> <y> <id1> <id2> ...
        - Atacar una unidad enemiga:                       AtacarUnidad <nombreJugadorAtacante> <idUnidadAtacante> <nombreJugadorObjetivo> <idUnidadObjetivo>
        - Atacar un edificio enemigo:                      AtacarEdificio <nombreJugadorAtacante> <idUnidadAtacante> <nombreJugadorObjetivo> <idEdificioObjetivo>
        - Ver recursos de un jugador:                      RecursosJugador <nombreJugador>
        - Ver unidades de un jugador:                      UnidadesJugador <nombreJugador>
        - Ver edificios de un jugador:                     EdificiosJugador <nombreJugador>
        - Listar todos los recursos en el mapa:            ListarRecursos
        - Listar todos los jugadores en la partida:        ListarJugadores
        - Muestra este menú de ayuda:                      Ayuda
        - Ver edificios disponibles y sus costos:          AyudaEdificios
        - Ver unidades disponibles y sus costos:           AyudaUnidades
        - Salir del juego:                                 Salir o Exit");
    }
}