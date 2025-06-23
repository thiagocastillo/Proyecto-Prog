using System;
using System.Collections.Generic;
using Library;

class Program
{
    static void Main(string[] args)
    {
        var motor = new Motor();
        Console.WriteLine("Bienvenido. Escriba 'Ayuda' para ver los comandos disponibles.");

        while (true)
        {
            Console.Write("\nComando: ");
            var entrada = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(entrada))
                continue;

            var partes = entrada.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var comando = partes[0].ToLower();
            var argumentos = new List<string>(partes.Length > 1 ? partes[1..] : Array.Empty<string>());

            var resultado = motor.ProcesarComando(comando, argumentos);
            Console.WriteLine(resultado);

            if (comando == "salir" || comando == "exit" || resultado.Contains("ganó la partida"))
                break;
        }
    }
}