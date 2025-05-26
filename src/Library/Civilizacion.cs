namespace Library;

public class Civilizacion
{
    public string Nombre { get; set; }
    public List<string> Bonificaciones { get; set; }
    public string UnidadEspecial { get; set; }

    public Civilizacion(string nombre, List<string> bonificaciones, string unidadEspecial)
    {
        Nombre = nombre;
        Bonificaciones = bonificaciones;
        UnidadEspecial = unidadEspecial;
    }
}