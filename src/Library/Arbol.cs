namespace Library;

public class Arbol : RecursoNatural, ITipoRecurso
{
    public Arbol(int vidaBase, Point ubicacion)
        : base("Madera", vidaBase, 0.75, ubicacion)
    {
        
    }
}