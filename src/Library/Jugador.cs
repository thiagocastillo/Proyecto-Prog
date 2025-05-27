namespace Library;

public class Jugador
{
    public string Nombre { get; set; }
    public Civilizacion Civilizacion { get; set; }
    public CentroCivico CentroCivico { get; set; }
    public List<Aldeano> Aldeanos { get; set; } = new List<Aldeano>();
    public Dictionary<Recurso.TipoRecurso, int> Recursos { get; set; } = new Dictionary<Recurso.TipoRecurso, int>()
    {
        { Recurso.TipoRecurso.Alimento, 100 },
        { Recurso.TipoRecurso.Madera, 100 },
        { Recurso.TipoRecurso.Oro, 0 },
        { Recurso.TipoRecurso.Piedra, 0 }
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

    public void AumentarPoblacionMaxima(int incremento)
    {
        PoblacionMaxima += incremento;
    }

    public void AgregarEdificio(IEdificio edificio)
    {
        Edificios.Add(edificio);
    }

    public void AgregarUnidad(IUnidad unidad)
    {
        Unidades.Add(unidad);
        PoblacionActual++;
    }
    
    public bool PuedeCrearAldeano()
    {
        int cantidadCentroCivico = Edificios.Count(e => e is CentroCivico);
        int aldeanosActuales = Aldeanos.Count;
        return cantidadCentroCivico > 0 && aldeanosActuales < 10;
    }
}
