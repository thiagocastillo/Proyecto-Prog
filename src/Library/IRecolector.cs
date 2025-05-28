namespace Library;

public interface IRecolector
{
    //void Recolectar(Recurso.TipoRecurso tipoRecurso, IAlmacenamiento? almacenCercano);
    void Recolectar(ITipoRecurso tipoRecurso, IAlmacenamiento almacenCercano = null);
}