namespace Library;

public class Piedra : RecursoNatural
{
    public Piedra(int vidaBase, Point ubicacion)
        : base("Piedra", vidaBase, 0.40)
    {
        Ubicacion = ubicacion;
    }
}