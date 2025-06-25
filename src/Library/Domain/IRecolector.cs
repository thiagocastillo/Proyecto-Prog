namespace Library.Domain;

public interface IRecolector
{
    //void Recolectar(Recurso.TipoRecurso tipoRecurso, IAlmacenamiento? almacenCercano);
    public void RecolectarEn(Point coordenada, Mapa mapa);
}