using System;
using System.Drawing;

namespace Library.Domain;

public class Casa : IEdificio
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public const int AumentoPoblacion = 5;
    public int Vida { get; set; }

    private int max_aldeanos = 20;
    private int max_unidadMilitar = 30;
    public int CantidadAldeanos { get; set; }
    public int CantidadUnidadesMilitar { get; set; }

    public DateTime InicioConstruccion { get; private set; }
    public int TiempoConstruccionTotal { get; } = 15; // segundos

    public bool EstaConstruido => (DateTime.Now - InicioConstruccion).TotalSeconds >= TiempoConstruccionTotal;
    public int TiempoConstruccionRestante
        => Math.Max(0, TiempoConstruccionTotal - (int)(DateTime.Now - InicioConstruccion).TotalSeconds);

    public Casa(Jugador propietario)
    {
        Propietario = propietario;
        Vida = 10000;
        InicioConstruccion = DateTime.Now;
        propietario.AumentarPoblacionMaxima(AumentoPoblacion);
    }

    public bool CantidadAldeano() => CantidadAldeanos <= max_aldeanos;
    public bool CantidadUnidadMilitar() => CantidadUnidadesMilitar <= max_unidadMilitar;
}