namespace Library.Domain;

public abstract class RecursoNatural
{
    public string Nombre { get; }
    public int VidaBase { get; }
    public double TasaRecoleccion { get; }
    public int Cantidad { get; set; }
    public bool EstaAgotado;
    public Point Ubicacion { get; set; }

    protected RecursoNatural(string nombre, int vidaBase, double tasaRecoleccion, Point ubicacion)
    {
        Nombre = nombre;
        VidaBase = vidaBase;
        TasaRecoleccion = tasaRecoleccion;
        Cantidad = vidaBase;
        Ubicacion = ubicacion;
    }

    // Recolectar un porcentaje fijo de la vida base
    public int Recolectar()
    {
        if (EstaAgotado)
            throw new InvalidOperationException("El recurso está agotado.");

        int aExtraer = Math.Max(1, (int)Math.Floor(VidaBase * TasaRecoleccion));
        int extraido = Math.Min(Cantidad, aExtraer);
        Cantidad -= extraido;
        int vidaactual = VidaBase- Cantidad;
        vidaactual = 0;

        if (vidaactual <= 0)
        {
            Cantidad = 0;
            EstaAgotado = true;
        }
        return extraido;
    }
}