
﻿namespace Library.Domain;

public static class Ayuda
{
    public static string ObtenerComandos()
    {
        return
            @"Comandos disponibles:

        - Crear una nueva partida:                         !CrearPartida
        - Mostrar el mapa del juego en TXT:                !MostrarMapaTXT
        - Mostrar el mapa del juego en html:               !MostrarMapaHTML
        - Ver civilizaciones disponibles:                  !VerCivilizaciones
        - Agregar un jugador a la partida:                 !AgregarJugador <nombreJugador> <civilización>
        - Construir un edificio:                           !ConstruirEdificio <nombreJugador> <tipo> <x> <y>
        - Entrenar una unidad:                             !EntrenarUnidad <nombreJugador> <tipo> <x> <y>
        - Ordenar a un aldeano recolectar un recurso:      !Recolectar <nombreJugador> <idAldeano> <x> <y>
        - Mover una unidad:                                !MoverUnidad <nombreJugador> <idUnidad> <x> <y>
        - Mover varias unidades en bloque:                 !MoverUnidades <nombreJugador> <x> <y> <id1> <id2> ...
        - Atacar una unidad enemiga:                       !AtacarUnidad <nombreJugadorAtacante> <idUnidadAtacante> <nombreJugadorObjetivo> <idUnidadObjetivo>
        - Atacar un edificio enemigo:                      !AtacarEdificio <nombreJugadorAtacante> <idUnidadAtacante> <nombreJugadorObjetivo> <idEdificioObjetivo>
        - Ver unidades de un jugador:                      !UnidadesJugador <nombreJugador>
        - Ver edificios de un jugador:                     !ListarEdificiosJugador <nombreJugador>
        - Listar todos los recursos en el mapa:            !listarRecursosJugador <nombreJugador>
        - Listar todos los jugadores en la partida:        !ListarJugadores
        - Muestra este menú de ayuda:                      !Ayuda
        - Ver edificios disponibles y sus costos:          !AyudaEdificios
        - Ver unidades disponibles y sus costos:           !AyudaUnidades
        - Salir del juego:                                 !Salir o !Exit";
    }

    public static string AyudaPartida()
    {
        return @"
            -Guardar partida:                                 !GuardarPartida <nombrePartida>
            -Cargar partida:                                 !CargarPartida <nombrePartida>
            -listar partidas guardadas:                     !ListarPartidas";
    }
    public static string AyudaEdificios()
    {
        return
        @"Edificios disponibles y sus costos:
        - Casa: 50 Madera
        - Cuartel: 100 Madera
        - Molino: 75 Madera
        - DepositoMadera: 60 Madera
        - DepositoOro: 60 Madera
        - DepositoPiedra: 60 Madera
        - CentroCivico: 200 Madera";
    }

    public static string AyudaUnidades()
    {
        return
        @"Unidades disponibles y sus costos:
        - Aldeano: 50 Alimento
        - Infanteria: 60 Alimento
        - Arquero: 70 Madera
        - Caballeria: 80 Alimento, 60 Madera
        - GuerreroJaguar: 60 Alimento
        - ArqueroCompuesto: 70 Madera
        - Ratha: 80 Alimento, 60 Madera";
    }
}
