namespace Library;

public class Ayuda
{
    public static string ObtenerAyuda()
    {
        string ayuda = "Ayuda de Comandos: \n\n";
        ayuda += "- mover (x, y): Mueve tu unidad a la posición (x, y).\n";
        ayuda += "- AtacarU (id_objetivo): Ataca a la unidad especificada.\n";
        ayuda += "- AtacarE (id_objetivo): Ataca al edificio especificado.\n";
        ayuda += "- recolectar (recurso): Inicia recolección de recurso.\n";
        ayuda += "- construir (edificio): Construye un edificio.\n";
        ayuda += "- estado: Muestra el estado actual del jugador.\n";
        ayuda += "- ayuda: Muestra esta ayuda.\n";
        return ayuda;
    }
}