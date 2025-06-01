using System;
namespace Library;

public class Program
{
    public static void Main(string[] args)
    {
        JuegoFachada juego = new JuegoFachada();

        // Crear una nueva partida
        juego.CrearNuevaPartida(100, 2);
        Console.WriteLine("Partida creada.");

        // Obtener las civilizaciones disponibles
        var civilizaciones = juego.ObtenerCivilizacionesDisponibles();
        Console.WriteLine("\nCivilizaciones disponibles:");
        foreach (var civilizacion in civilizaciones)
        {
            Console.WriteLine($"- {civilizacion}");
        }

        // Agregar jugadores y seleccionar civilizaciones
        string jugador1Nombre = "Juancito";
        string jugador2Nombre = "Roberto";

        juego.AgregarJugadorAPartida("Juancito", "Armenios");
        juego.AgregarJugadorAPartida("Roberto", "Aztecas");

        Console.WriteLine($"\nJugador {jugador1Nombre} se unió como Armenios.");
        Console.WriteLine($"Jugador {jugador2Nombre} se unió como Aztecas.");

        // Obtener recursos iniciales del jugador 1
        var recursosAlice = juego.ObtenerRecursosJugador(jugador1Nombre);
        Console.WriteLine($"\nRecursos iniciales de {jugador1Nombre}:");
        foreach (var recurso in recursosAlice)
        {
            Console.WriteLine($"- {recurso.Key}: {recurso.Value}");
        }

        // Jugador 1 construye una casa
        Console.WriteLine($"\n{jugador1Nombre} intenta construir una casa en (1, 1).");
        juego.ConstruirEdificio(jugador1Nombre, "casa", new Point { X = 1, Y = 1 });
        var edificiosAlice = juego.ObtenerEdificiosJugador(jugador1Nombre);
        Console.WriteLine($"Edificios de {jugador1Nombre}: {edificiosAlice.Count}");

        // Jugador 1 entrena un aldeano
        Console.WriteLine($"\n{jugador1Nombre} intenta entrenar un aldeano.");
        juego.EntrenarUnidad(jugador1Nombre, "aldeano");
        var unidadesAlice = juego.ObtenerUnidadesJugador(jugador1Nombre);
        Console.WriteLine($"Unidades de {jugador1Nombre}: {unidadesAlice.Count}");

        // Jugador 1 construye un cuartel
        Console.WriteLine($"\n{jugador1Nombre} intenta construir un cuartel en (2, 2).");
        juego.ConstruirEdificio(jugador1Nombre, "cuartel", new Point { X = 2, Y = 2 });
        edificiosAlice = juego.ObtenerEdificiosJugador(jugador1Nombre);
        Console.WriteLine($"Edificios de {jugador1Nombre}: {edificiosAlice.Count}");

        // Jugador 1 entrena una infantería
        Console.WriteLine($"\n{jugador1Nombre} intenta entrenar una infantería.");
        juego.EntrenarUnidad(jugador1Nombre, "infanteria");
        unidadesAlice = juego.ObtenerUnidadesJugador(jugador1Nombre);
        Console.WriteLine($"Unidades de {jugador1Nombre}: {unidadesAlice.Count}");

        // Obtener recursos actualizados del jugador 1
        recursosAlice = juego.ObtenerRecursosJugador(jugador1Nombre);
        Console.WriteLine($"\nRecursos de {jugador1Nombre} después de construir y entrenar:");
        foreach (var recurso in recursosAlice)
        {
            Console.WriteLine($"- {recurso.Key}: {recurso.Value}");
        }
        
        // Resumen combate
        string resumenCombate = juego.AtacarUnidad("Juancito", 0, 1); // ID 0 ataca al 1
        Console.WriteLine(resumenCombate);
    }
}