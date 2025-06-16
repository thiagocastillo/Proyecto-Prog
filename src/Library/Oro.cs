namespace Library;

public class Oro : RecursoNatural
{
    public Oro(int vidaBase, Point ubicacion)
        : base("Oro", vidaBase, 0.50)
    {
        Ubicacion = ubicacion;
    }
}