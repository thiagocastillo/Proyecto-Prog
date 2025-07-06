using System;

namespace Library.Domain;

public class TiempoDeRecoleccion
{
    public int TiempoTotalSegundos { get; private set; }
    public DateTime Inicio { get; private set; }
    public DateTime Fin => Inicio.AddSeconds(TiempoTotalSegundos);

    public int TiempoRestanteSegundos
    {
        get
        {
            int restante = (int)(Fin - DateTime.Now).TotalSeconds;
            return restante > 0 ? restante : 0;
        }
    }

    public bool EstaCompleta => DateTime.Now >= Fin;

    public TiempoDeRecoleccion(int tiempoEnSegundos)
    {
        TiempoTotalSegundos = tiempoEnSegundos;
        Inicio = DateTime.Now;
    }
}