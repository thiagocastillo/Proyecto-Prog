namespace Library;

public static class Ayuda
{
    public static string ObtenerComandos()
    {
        return
            @"Comandos disponibles:
        ayuda                                         - Muestra este menú de ayuda
        crearpartida                                  - Crear una nueva partida
        civilizaciones                                - Ver civilizaciones disponibles
        agregarjugador <nombre> <civilización>        - Agregar un jugador a la partida
        seleccionarcivilizacion <nombre> <civilización> - Seleccionar civilización para un jugador
        construiredificio <nombre> <tipo> <x> <y>     - Construir un edificio
        entrenarunidad <nombre> <tipo>                - Entrenar una unidad
        moverunidad <nombre> <idUnidad> <x> <y>       - Mover una unidad
        atacarunidad <nombre> <idAtacante> <idObjetivo> - Atacar con una unidad
        estadojugador <nombre>                        - Ver informacion detallada del jugador
        recursosjugador <nombre>                      - Ver recursos de un jugador
        unidadesjugador <nombre>                      - Ver unidades de un jugador
        edificiosjugador <nombre>                     - Ver edificios de un jugador
        listarjugadores                              - Listar todos los jugadores en la partida
        mostrarMapa                                   - Mostrar el mapa del juego
        salir                                         - Salir del juego";
    }
}