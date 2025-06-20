namespace Library;

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
        Cantidad = (int)(vidaBase * tasaRecoleccion);
        Ubicacion = ubicacion;
    }

    public int Recolectar(double cantidad)
    {
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
        return extraido;
    }
}