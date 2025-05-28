namespace Library;


public class Recurso
{
    public enum TipoRecurso
    {
        Madera,
        Alimento,
        Oro,
        Piedra
    }

    public TipoRecurso Tipo { get; private set; }
    
    public int Cantidad { get; private set; }

    public Recurso(TipoRecurso tipo, int cantidadinicial)
    {
        Tipo = tipo;
        Cantidad = cantidadinicial;
    }

    public void Alerta(int Cantidad)
    {
        if (Cantidad <= 10)
        {
            Console.WriteLine($"¡ Cuidado ! {Tipo} esta por agotarse. {Tipo} restante: {Cantidad}");
        }
    }
}