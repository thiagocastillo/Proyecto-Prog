namespace Library;

public class Ayuda
{
    public static void MostrarAyuda()
    {
        Console.WriteLine("AYUDA - COMANDOS DISPONIBLES: ");
        Console.WriteLine("1) Mover Unidad (x,y) - Mueve la unidad a las coordenadas indicadas");
        Console.WriteLine("Ejemplo: mover unidad (2, 10)");
        Console.WriteLine("2) Atacar Unidad Objetivo - Ordena a una unidad a atacar a otra unidad");
        Console.WriteLine("Ejemplo: atacar unidad1 unidad2");
        Console.WriteLine("3) AtacarE Unidad Edificio - Ordena a una unidad a atacar un edificio");
        Console.WriteLine("Ejemplo: atacarE unidad1 edificio1");
        Console.WriteLine("4) Recolectar Unidad Recurso - Ordena a un aldeano recolectar un recurso");
        Console.WriteLine("Ejemplo: recolectar aldeano1 oro");
        Console.WriteLine("5) Construir Edificio (x,y) - Construye un edificio en la posicion indicada");
        Console.WriteLine("Ejemplo: construir granja (10, 10)");
        Console.WriteLine("6) Mostrar Recursos - Muestra los recurso actuales");
        Console.WriteLine("7) Mostrar Unidades - Muestra todas las unidades del jugador actual");
        Console.WriteLine("8) Ayuda - Muestra esta lista de comandos");
    }
}