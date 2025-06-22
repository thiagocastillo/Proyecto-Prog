namespace Library;

public class TiempoConstruccion
{
    // Tiempo total de construccion desde que inicia hasta que termina
    public int TiempoTotal { get; private set; }
    // Tiempo que aun falta para terminar la construccion
    public int TiempoRestante { get; private set; }
    // Verdadero si el edificio ya esta completamente construido
    public bool EstaCompleta => TiempoRestante <= 0;

    // Inicializa el tiempo de construccion con un valor determinado
    public TiempoConstruccion(int tiempo)
    {
        TiempoTotal = tiempo;
        TiempoRestante = tiempo;
    }

    // Reduce en 1 el tiempo restante de construccion
    public void Avanzar()
    {
        if (TiempoRestante > 0)
            TiempoRestante--;
    }
}