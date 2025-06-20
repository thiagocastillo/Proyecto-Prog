namespace Library;

public static class Ayuda
{
    public static string ObtenerComandos()
    {
        return
            @"Comandos disponibles:
        
        - Crear una nueva partida:                         CrearPartida
        - Mostrar el mapa del juego:                       MostrarMapa
        - Ver civilizaciones disponibles:                  Civilizaciones                                                    
        - Agregar un jugador a la partida:                 AgregarJugador <nombreJugador> <civilización>                     
        - Construir un edificio:                           ConstruirEdificio <nombreJugador> <tipo> <x> <y>                  
        - Entrenar una unidad:                             EntrenarUnidad <nombreJugador> <tipo>                             
        - Ordenar a un aldeano recolectar un recurso:      Recolectar <nombreJugador> <idAldeano> <x> <y>                    
        - Mover una unidad:                                Moverunidad <nombreJugador> <idUnidad> <x> <y>                    
        - Atacar con una unidad:                           Atacarunidad <nombreJugador> <idAtacante> <idObjetivo>            
        - Atacar un edificio:                              AtacarEdificio <nombreJugador> <idAtacante> <idEdificio>         
        - Ver recursos de un jugador:                      Recursosjugador <nombreJugador>                                   
        - Ver unidades de un jugador:                      Unidadesjugador <nombreJugador>                                   
        - Ver edificios de un jugador:                     Edificiosjugador <nombreJugador>                                  
        - Listar todos los jugadores en la partida:        Listarjugadores                                                   
        - Muestra este menú de ayuda:                      Ayuda                                                 
        - Salir del juego:                                 Salir or Exit";
    }
}