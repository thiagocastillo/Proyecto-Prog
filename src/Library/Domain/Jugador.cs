using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Library.Domain;

public class Jugador
{
    public const int LimiteAldeanos = 20;
    public const int LimiteMilitares = 30;
    public static Random random = new Random();
    int x = random.Next(0, 100);
    int y = random.Next(0, 100);

    public string Nombre { get; set; }
    public Civilizacion Civilizacion { get; set; }
    public List<IEdificio> EdificiosEnConstruccion { get; set; } = new();
    public CentroCivico CentroCivico { get; set; }
    public List<Aldeano> Aldeanos { get; set; } = new List<Aldeano>();
    public Dictionary<string, int> Recursos { get; set; } = new Dictionary<string, int>()
    {
        { "Alimento", 0 },
        { "Madera", 0 },
        { "Oro", 0 },
        { "Piedra", 0 }
    };
    public int PoblacionActual { get; set; } = 3;
    public int PoblacionMaxima { get; set; } = 5;
    public List<IEdificio> Edificios { get; set; } = new List<IEdificio>();
    public List<IUnidad> Unidades { get; set; } = new List<IUnidad>();

    // NUEVO: Unidades militares en entrenamiento
    public List<IUnidadMilitar> UnidadesEnEntrenamiento { get; set; } = new List<IUnidadMilitar>();

    [JsonConstructor]
    public Jugador(string nombre, Civilizacion civilizacion)
    {
        Nombre = nombre;
        Civilizacion = civilizacion;
        CentroCivico = new CentroCivico(this) { Posicion = new Point { X = x, Y = y } };
        Edificios.Add(CentroCivico);

        for (int i = 0; i < 3; i++)
        {
            Aldeano aldeano = new Aldeano(this) { Posicion = new Point { X = x + i + 1, Y = y } };
            Aldeanos.Add(aldeano);
            Unidades.Add(aldeano);
        }
    }

    public Dictionary<string, int> ObtenerResumenRecursosTotales()
    {
        Dictionary<string, int> total = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        foreach (KeyValuePair<string, int> par in Recursos)
        {
            string key = par.Key.ToLower();
            if (!total.ContainsKey(key))
                total[key] = 0;
            total[key] += par.Value;
        }
        IEnumerable<IAlmacenamiento> almacenamientos = Edificios.OfType<IAlmacenamiento>();
        foreach (IAlmacenamiento almacen in almacenamientos)
        {
            foreach (KeyValuePair<string, int> par in almacen.Recursos)
            {
                string key = par.Key.ToLower();
                if (!total.ContainsKey(key))
                    total[key] = 0;
                total[key] += par.Value;
            }
        }
        return total;
    }

    public void AgregarRecurso(ITipoRecurso tipo, int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad de recursos debe ser mayor que cero.");
        if (!Recursos.ContainsKey(tipo.Nombre))
            Recursos[tipo.Nombre] = 0;
        Recursos[tipo.Nombre] += cantidad;
    }

    public void DepositarRecursos(Dictionary<string, int> recursosEdificio)
    {
        foreach (KeyValuePair<string, int> par in recursosEdificio)
        {
            if (!Recursos.ContainsKey(par.Key))
                Recursos[par.Key] = 0;
            Recursos[par.Key] += par.Value;
        }
    }

    public void AumentarPoblacionMaxima(int incremento)
    {
        if (incremento > 0)
        {
            int maxTotal = LimiteAldeanos + LimiteMilitares;
            if (PoblacionMaxima + incremento > maxTotal)
                PoblacionMaxima = maxTotal;
            else
                PoblacionMaxima += incremento;
        }
    }

    public void AgregarEdificio(IEdificio edificio)
    {
        Edificios.Add(edificio);
    }

    public void AgregarUnidad(IUnidad unidad)
    {
        try
        {
            if (unidad is Aldeano)
            {
                if (Aldeanos.Count >= LimiteAldeanos)
                    throw new InvalidOperationException("No se pueden tener más de 20 aldeanos.");
                Aldeanos.Add((Aldeano)unidad);
            }
            else
            {
                int militares = Unidades.Count(u => !(u is Aldeano));
                if (militares >= LimiteMilitares)
                    throw new InvalidOperationException("No se pueden tener más de 30 unidades militares.");
            }
            Unidades.Add(unidad);
            PoblacionActual++;
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"Error al agregar unidad: {ex.Message}", ex);
        }
    }

    public string ObtenerResumenPoblacion()
    {
        return $"Población: {PoblacionActual}/{PoblacionMaxima}";
    }

    // MODIFICADO: Ahora mueve las unidades terminadas de UnidadesEnEntrenamiento a Unidades
    public string ActualizarUnidades()
    {
        List<string> mensajes = new List<string>();
        List<IUnidadMilitar> terminadas = UnidadesEnEntrenamiento
            .Where(u => u.TiempoGeneracion != null && u.TiempoGeneracion.EstaCompleta)
            .ToList();

        foreach (IUnidadMilitar unidad in terminadas)
        {
            Unidades.Add(unidad);
            PoblacionActual++;
            mensajes.Add($"La unidad {unidad.GetType().Name} en {unidad.Posicion} ha sido generada para el jugador {Nombre}.");
            UnidadesEnEntrenamiento.Remove(unidad);
        }

        return string.Join("\n", mensajes);
    }

    public string ActualizarConstrucciones()
    {
        Console.WriteLine($"Actualizando construcciones de {Nombre}");
        var mensajes = new List<string>();
        var terminados = EdificiosEnConstruccion
            .Where(e =>
                (e is Casa casa && casa.EstaConstruido) ||
                (e is Cuartel cuartel && cuartel.EstaConstruido) ||
                (e is Molino molino && molino.EstaConstruido) ||
                (e is DepositoMadera dm && dm.EstaConstruido) ||
                (e is DepositoOro doo && doo.EstaConstruido) ||
                (e is DepositoPiedra dp && dp.EstaConstruido)
            )
            .ToList();

        foreach (var edificio in terminados)
        {
            Edificios.Add(edificio);
            EdificiosEnConstruccion.Remove(edificio);
            mensajes.Add($"El edificio {edificio.GetType().Name} en {edificio.Posicion} ha sido construido para el jugador {Nombre}.");
        }

        return string.Join("\n", mensajes);
    }

    public bool PuedeCrearAldeano()
    {
        int cantidadCentroCivico = Edificios.Count(e => e is CentroCivico);
        int aldeanosActuales = Aldeanos.Count;
        return cantidadCentroCivico > 0 && aldeanosActuales < LimiteAldeanos;
    }
}