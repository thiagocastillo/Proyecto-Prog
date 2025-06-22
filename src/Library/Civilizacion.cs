namespace Library;

public class Civilizacion
{
    // Nombre de la civilizacion
    public string Nombre { get; set; }
    // Lista de bonificaciones unicas que tiene esta civilizacion
    public List<string> Bonificaciones { get; set; }
    // Unidad especial que reemplaza a otra unidad por defecto
    public string UnidadEspecial { get; set; }

    // Constructor que define los datos de una civilizacion
    public Civilizacion(string nombre, List<string> bonificaciones, string unidadEspecial)
    {
        Nombre = nombre;
        Bonificaciones = bonificaciones;
        UnidadEspecial = unidadEspecial;
    }
}