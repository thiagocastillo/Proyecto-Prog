namespace DefaultNamespace;

public interface IRecolector
{
    void Recolectar(Recurso.TipoRecurso tipoRecurso, IAlmacenamiento? almacenCercano);
}