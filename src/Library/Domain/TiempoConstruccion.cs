using System;

namespace Library.Domain;

public class TiempoConstruccion
{
    // Tiempo total de construcción en segundos
    public int TiempoTotalSegundos { get; private set; }

    // Momento en que comenzó la construcción
    public DateTime Inicio { get; private set; }

    // Momento en que termina la construcción
    public DateTime Fin => Inicio.AddSeconds(TiempoTotalSegundos);

    // Tiempo restante en segundos (puede ser 0 si ya terminó)
    public int TiempoRestanteSegundos
    {
        get
        {
            int restante = (int)(Fin - DateTime.Now).TotalSeconds;
            return restante > 0 ? restante : 0;
        }
    }

    // Verdadero si la construcción está completa
    public bool EstaCompleta => DateTime.Now >= Fin;

    // Inicializa el tiempo de construcción con un valor en segundos
    public TiempoConstruccion(int tiempoEnSegundos)
    {
        TiempoTotalSegundos = tiempoEnSegundos;
        Inicio = DateTime.Now;
    }
}