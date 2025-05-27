namespace Library;

public class Recurso
{
   /* public enum TipoRecurso
    {
        Madera,
        Alimento,
        Oro,
        Piedra
    }

    public TipoRecurso Tipo { get; private set; }

    public Recurso(TipoRecurso tipo)
    {
        Tipo = tipo;
    }*/
   
    public ITipoRecurso Tipo { get; private set; }
    
    public Recurso(ITipoRecurso tipo)
    {
        Tipo = tipo;
    }
}