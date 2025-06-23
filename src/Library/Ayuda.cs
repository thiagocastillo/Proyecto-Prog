namespace Library;

public static class Ayuda
{
    public static string ObtenerComandos()
    {
        return
            @"Comandos disponibles:
        
        - Crear una nueva partida:                         CrearPartida
        - Mostrar el mapa del juego:                       MostrarMapa (muestra el mapa con coordenadas en un txt)
        - Mostrar el mapa del juego con colores:           MostrarMapaColores(muestra el mapa con colores en la consola)
        - Ver civilizaciones disponibles:                  Civilizaciones                                                    
        - Agregar un jugador a la partida:                 AgregarJugador <nombreJugador> <civilización>                     
        - Construir un edificio:                           ConstruirEdificio <nombreJugador> <tipo> <x> <y>                  
        - Entrenar una unidad:                             EntrenarUnidad <nombreJugador> <tipo> <x> <y>                             
        - Ordenar a un aldeano recolectar un recurso:      Recolectar <nombreJugador> <idAldeano> <x> <y>                    
        - Mover una unidad:                                MoverUnidad <nombreJugador> <idUnidad> <x> <y> 
        - Mover varias unidades en bloque:                 MoverUnidades <nombreJugador> <x> <y> <id1> <id2> ...                   
        - Atacar con una unidad:                           AtacarUnidad <nombreJugador> <idAtacante> <tipoUnidad> <cantidad> <x> <y>
        - Atacar un edificio:                              AtacarEdificio <nombreJugador> <idAtacante> <idEdificio>
        - Ver recursos de un jugador:                      RecursosJugador <nombreJugador>                                   
        - Ver unidades de un jugador:                      UnidadesJugador <nombreJugador>                                   
        - Ver edificios de un jugador:                     EdificiosJugador <nombreJugador>                                  
        - Listar todos los jugadores en la partida:        ListarJugadores                                                   
        - Muestra este menú de ayuda:                      Ayuda                                                 
        - Salir del juego:                                 Salir o Exit";
    }
}