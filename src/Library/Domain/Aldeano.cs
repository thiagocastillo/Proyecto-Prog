using System;
using System.Drawing;
using System.Linq;

namespace Library.Domain;

public class Aldeano : IUnidad, IRecolector
{ 
    public Jugador Propietario { get; set; }

    // Propiedades de combate
    public int Ataque { get; private set; } = 0;
    public int Defensa { get; private set; } = 0;
    public int TiempoRecoleccionS { get; set; } // Tiempo en segundos (igual a la distancia)
    public double Velocidad { get; private set; } = 1.0;

    public int Salud { get; set; }
    public Point Posicion { get; set; }
    public int TiempoDeCreacion => TiempoGeneracion.TiempoTotalSegundos;
    public TiempoDeGeneracion TiempoGeneracion { get; private set; }

    public Aldeano(Jugador propietario)
    {
        Propietario = propietario;
        TiempoGeneracion = new TiempoDeGeneracion(10);
    }

    public bool Mover(Point destino, Mapa mapa)
    {
        if (destino == null)
            throw new ArgumentNullException(nameof(destino), "El destino no puede ser nulo.");

        if (destino.X < 0 || destino.X >= mapa.Ancho || destino.Y < 0 || destino.Y >= mapa.Alto)
            return false;

        Posicion = destino;
        return true;
    }

    public double CalcularDaño(IUnidad objetivo) => 0;

    public string AtacarEdificio(IEdificio objetivo) => "Los aldeanos no atacan edificios.";

    public string AtacarUnidad(IUnidad objetivo) => "Los aldeanos no atacan unidades.";

    public void RecolectarEn(Point coordenada, Mapa mapa)
    {
        RecursoNatural recurso = mapa.Recursos.FirstOrDefault(r => r.Ubicacion.X == coordenada.X && r.Ubicacion.Y == coordenada.Y);
        if (recurso == null)
            throw new InvalidOperationException("No hay recurso natural en la coordenada especificada.");

        if (recurso.Cantidad <= 0 || recurso.EstaAgotado)
            throw new InvalidOperationException("El recurso está agotado.");

        int distanciaRecurso = (int)CalcularDistancia(this.Posicion, coordenada);      
        IAlmacenamiento almacenMasCercano = null;
        double distanciaMinima = double.MaxValue;

        TiempoRecoleccionS = distanciaRecurso;

        foreach (IEdificio edificio in Propietario.Edificios)
        {
            if (edificio is IAlmacenamiento almacen && EsCompatible(almacen, recurso.Nombre))
            {
                double distancia = CalcularDistancia(coordenada, almacen.Posicion);
                if (distancia < distanciaMinima)
                {
                    distanciaMinima = distancia;
                    almacenMasCercano = almacen;
                }
            }
        }

        if (almacenMasCercano == null)
            throw new InvalidOperationException("No existe un edificio de almacenamiento compatible para este recurso.");

        this.Posicion = coordenada;

        int extraido = recurso.Recolectar();

        if (Propietario.Civilizacion.Nombre == "Aztecas")
            extraido += 3;

        if (!almacenMasCercano.Recursos.ContainsKey(recurso.Nombre))
            almacenMasCercano.Recursos[recurso.Nombre] = 0;

        almacenMasCercano.Recursos[recurso.Nombre] += extraido;

        if (recurso.EstaAgotado)
            mapa.Recursos.Remove(recurso);
    }

    private bool EsCompatible(IAlmacenamiento almacen, string nombre)
    {
        return (nombre == "Madera" && (almacen is DepositoMadera || almacen is CentroCivico)) ||
               (nombre == "Alimento" && (almacen is Granja || almacen is Molino || almacen is CentroCivico)) ||
               (nombre == "Oro" && (almacen is DepositoOro || almacen is CentroCivico)) ||
               (nombre == "Piedra" && (almacen is DepositoPiedra || almacen is CentroCivico));
    }

    private double CalcularDistancia(Point a, Point b)
    {
        int dx = a.X - b.X;
        int dy = a.Y - b.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}