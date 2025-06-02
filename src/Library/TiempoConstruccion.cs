namespace Library;

public class TiempoConstruccion
{
    public int TiempoTotal { get; private set; }
    public int TiempoRestante { get; private set; }
    public bool EstaCompleta => TiempoRestante <= 0;

    public TiempoConstruccion(int tiempo)
    {
        TiempoTotal = tiempo;
        TiempoRestante = tiempo;
    }

    public void Avanzar()
    {
        if (TiempoRestante > 0)
            TiempoRestante--;
    }
}