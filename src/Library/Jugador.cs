namespace Library;

public class Jugador
{
    public const int LimiteAldeanos = 20;
    public const int LimiteMilitares = 30;

    public string Nombre { get; set; }
    public Civilizacion Civilizacion { get; set; }
    public CentroCivico CentroCivico { get; set; }
    public List<Aldeano> Aldeanos { get; set; } = new List<Aldeano>();
    public Dictionary<string, int> Recursos { get; set; } = new Dictionary<string, int>()
    {
        { "Alimento", 100 },
        { "Madera", 100 },
        { "Oro", 0 },
        { "Piedra", 0 }
    };

    public int PoblacionActual { get; set; } = 3;
    public int PoblacionMaxima { get; set; } = 5;
    public List<IEdificio> Edificios { get; set; } = new List<IEdificio>();
    public List<IUnidad> Unidades { get; set; } = new List<IUnidad>();

    public Jugador(string nombre, Civilizacion civilizacion)
    {
        Nombre = nombre;
        Civilizacion = civilizacion;
        CentroCivico = new CentroCivico(this) { Posicion = new Point { X = 0, Y = 0 } };
        Edificios.Add(CentroCivico);
        for (int i = 0; i < 3; i++)
        {
            var aldeano = new Aldeano(this) { Posicion = new Point { X = i + 1, Y = 0 } };
            Aldeanos.Add(aldeano);
            Unidades.Add(aldeano);
        }
    }

    public void AgregarRecurso(ITipoRecurso tipo, int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad de recursos debe ser mayor que cero.");
        
        if (!Recursos.ContainsKey(tipo.Nombre))
            Recursos[tipo.Nombre] = 0;

        Recursos[tipo.Nombre] += cantidad;
    }

    public void AumentarPoblacionMaxima(int incremento)
    {
        // No permite superar el máximo absoluto de aldeanos + militares

        try
        {
            if (incremento <= 0)
                throw new ArgumentException("El incremento debe ser mayor que cero.");

            int maxTotal = LimiteAldeanos + LimiteMilitares;
            if (PoblacionMaxima + incremento > maxTotal)
                PoblacionMaxima = maxTotal;
            else
                PoblacionMaxima += incremento;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error al aumentar población máxima: {ex.Message}");
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
            Console.WriteLine($"Error al agregar unidad: {ex.Message}");
        }
    }
    public string ObtenerResumenPoblacion()
    {
        return $"Población: {PoblacionActual}/{PoblacionMaxima}";
    }

    public bool PuedeCrearAldeano()
    {
        int cantidadCentroCivico = Edificios.Count(e => e is CentroCivico);
        int aldeanosActuales = Aldeanos.Count;
        return cantidadCentroCivico > 0 && aldeanosActuales < LimiteAldeanos;
    }
}