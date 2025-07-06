namespace Library.Domain;
using System.Collections.Generic;
using System;
using System.Linq;

public class Jugador
{
    // Limites maximos permitidos para aldeanos y unidades militares
    public const int LimiteAldeanos = 20;
    public const int LimiteMilitares = 30;
    public static Random random = new Random();
    // Posicion inicial aleatoria del jugador
    int x = random.Next(0, 100);
    int y = random.Next(0, 100);
    
    // Datos principales del jugador
    public string Nombre { get; set; }
    public Civilizacion Civilizacion { get; set; }
    public List<IEdificio> EdificiosEnConstruccion { get; set; } = new();
    // Edificio principal del jugador
    public CentroCivico CentroCivico { get; set; }
    // Lista de aldeanos del jugador
    public List<Aldeano> Aldeanos { get; set; } = new List<Aldeano>();
    
    // Recursos del jugador
    public Dictionary<string, int> Recursos { get; set; } = new Dictionary<string, int>()
    {
        { "Alimento", 0 },
        { "Madera", 0 },
        { "Oro", 0 },
        { "Piedra", 0 }
    };

    // Control de poblacion del jugador
    public int PoblacionActual { get; set; } = 3;
    public int PoblacionMaxima { get; set; } = 5;
    public List<IEdificio> Edificios { get; set; } = new List<IEdificio>();
    // Todas las unidades del jugador
    public List<IUnidad> Unidades { get; set; } = new List<IUnidad>();

    public Jugador(string nombre, Civilizacion civilizacion)
    {
        // Crea un centro civico al inicio y lo ubica
        Nombre = nombre;
        Civilizacion = civilizacion;
        CentroCivico = new CentroCivico(this) { Posicion = new Point { X = x, Y = y } };
        Edificios.Add(CentroCivico);
        
        // Crea 3 aldeanos iniciales junto al centro civico
        for (int i = 0; i < 3; i++)
        {
            Aldeano aldeano = new Aldeano(this) { Posicion = new Point { X = x + i + 1, Y = y } };
            Aldeanos.Add(aldeano);
            Unidades.Add(aldeano);
        }
    }
    // Devuelve un resumen total de recursos
    public Dictionary<string, int> ObtenerResumenRecursosTotales()
    {
        Dictionary<string, int> total = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        // Sumar recursos del jugador
        foreach (KeyValuePair<string, int> par in Recursos)
        {
            string key = par.Key.ToLower();
            if (!total.ContainsKey(key))
                total[key] = 0;
            total[key] += par.Value;
        }

        // Sumar recursos de los edificios de almacenamiento
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

    // Agrega una cantidad de recurso al jugador
    public void AgregarRecurso(ITipoRecurso tipo, int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad de recursos debe ser mayor que cero.");

        if (!Recursos.ContainsKey(tipo.Nombre))
            Recursos[tipo.Nombre] = 0;

        Recursos[tipo.Nombre] += cantidad;
    }

    // Suma recursos desde un edificio
    public void DepositarRecursos(Dictionary<string, int> recursosEdificio)
    {
        foreach (KeyValuePair<string, int> par in recursosEdificio)
        {
            if (!Recursos.ContainsKey(par.Key))
                Recursos[par.Key] = 0;
            Recursos[par.Key] += par.Value;
        }
    }

    // Incrementa el limite de poblacion
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

    // Agrega un nuevo edificio al jugador
    public void AgregarEdificio(IEdificio edificio)
    {
        Edificios.Add(edificio);
    }

    // Agrega una unidad al jugador
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

    // Devuelve texto con el estado actual de la poblacion
    public string ObtenerResumenPoblacion()
    {
        return $"Población: {PoblacionActual}/{PoblacionMaxima}";
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