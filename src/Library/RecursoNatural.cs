
namespace Library;

public abstract class RecursoNatural
{
    public string Nombre { get; }
    public int VidaBase { get; }
    public double TasaRecoleccion { get; }
    public int Cantidad { get; set; }
    public bool EstaAgotado => Cantidad <= 0;
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
        int extraido = Math.Min(Cantidad, (int)Math.Floor(cantidad));
        Cantidad -= extraido;
        return extraido;
    }
}