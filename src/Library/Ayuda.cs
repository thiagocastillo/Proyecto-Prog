namespace Library;

public static class Ayuda
{
    public static string ObtenerComandos()
    {
        return
            @"Comandos disponibles:
        ayuda                                                             - Muestra este menú de ayuda
        crearpartida                                                      - Crear una nueva partida
        civilizaciones                                                    - Ver civilizaciones disponibles
        agregarjugador <nombreJugador> <civilización>                     - Agregar un jugador a la partida
        construiredificio <nombreJugador> <tipo> <x> <y>                  - Construir un edificio
        entrenarunidad <nombreJugador> <tipo>                             - Entrenar una unidad
        recolectar <nombreJugador> <idAldeano> <x> <y>                    - Ordenar a un aldeano recolectar un recurso
        moverunidad <nombreJugador> <idUnidad> <x> <y>                    - Mover una unidad
        atacarunidad <nombreJugador> <idAtacante> <idObjetivo>            - Atacar con una unidad
        atacarEdificio <nombreJugador> <idAtacante> <idEdificio>          - Atacar un edificio
        recursosjugador <nombreJugador>                                   - Ver recursos de un jugador
        unidadesjugador <nombreJugador>                                   - Ver unidades de un jugador
        edificiosjugador <nombreJugador>                                  - Ver edificios de un jugador
        listarjugadores                                                   - Listar todos los jugadores en la partida
        mostrarMapa                                                       - Mostrar el mapa del juego
        salir                                                             - Salir del juego";
    }
}