namespace Library;

public abstract class RecursoNatural
{
    public int Vida { get; protected set; }
    public double TasaRecoleccion { get; protected set; }
    public string Nombre { get; protected set; }

    protected RecursoNatural(string nombre, int vida, double tasaRecoleccion)
    {
        Nombre = nombre;
        Vida = vida;
        TasaRecoleccion = tasaRecoleccion;
    }

    public int Recolectar(int cantidad)
    {
        int recolectado = Math.Min(cantidad, Vida);
        Vida -= recolectado;
        return recolectado;
    }

    public bool EstaAgotado() => Vida <= 0;
}