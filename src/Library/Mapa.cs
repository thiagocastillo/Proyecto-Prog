namespace DefaultNamespace;

public class Mapa
{
    public int Ancho { get; private set; }
    public int Alto { get; private set; }

    public Mapa(int ancho, int alto)
    {
        Ancho = ancho;
        Alto = alto;
    }
}