namespace Library;

public interface IRecolector
{
    //void Recolectar(Recurso.TipoRecurso tipoRecurso, IAlmacenamiento? almacenCercano);
    public void Recolectar(RecursoNatural recurso, IAlmacenamiento almacenCercano = null);
}