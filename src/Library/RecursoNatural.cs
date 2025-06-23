namespace Library;

public abstract class RecursoNatural
{
    // Nombre del recurso
    public string Nombre { get; }
    // Vida del recurso
    public int VidaBase { get; }
    // Tasa a la que se recolecta el recurso
    public double TasaRecoleccion { get; }
    // Cantidad actual disponible para ser recolectada
    public int Cantidad { get; set; }
    // Indica si el recurso esta completamente recolectado
    public bool EstaAgotado;
    // Coordenadas del mapa donde se encuentra el recurso
    public Point Ubicacion { get; set; }

    // Constructor potegido, solo pueden llamarlo las clases derivadas
    protected RecursoNatural(string nombre, int vidaBase, double tasaRecoleccion, Point ubicacion)
    {
        Nombre = nombre;
        VidaBase = vidaBase;
        TasaRecoleccion = tasaRecoleccion;
        // La cantidad inicial se calcula como vida
        Cantidad = (int)(vidaBase * tasaRecoleccion);
        Ubicacion = ubicacion;
    }

    // Recolectar una cantidad de recurso
    public int Recolectar(double cantidad)
    {
        // Si el recurso ya fue extraido por completo, devuelve excepcion
        if (EstaAgotado)
            throw new InvalidOperationException("El recurso está agotado.");

        int extraido = Math.Min(Cantidad, Math.Max(1, (int)Math.Floor(cantidad)));
        Cantidad -= extraido;
        Console.WriteLine($"Cantidad restante: {Cantidad}");
        if (Cantidad <= 0)
        {
            Cantidad = 0;
            EstaAgotado = true;
        }
        // Devuelve cuanto recurso se extrajo
        return extraido;
    }
}