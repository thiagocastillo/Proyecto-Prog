namespace Library;

public class Arbol : RecursoNatural, ITipoRecurso
{
    public string Nombre => "Madera";
    public Arbol(int vidaBase, Point ubicacion)
        : base("Madera", vidaBase, 0.75)
    {
        Ubicacion = ubicacion;
    }
}