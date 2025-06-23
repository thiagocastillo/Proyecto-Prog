using System.Drawing;

namespace Library;

public class Alimento : RecursoNatural
{
    public Alimento(int vidaBase, Point ubicacion)
        : base("Alimento", vidaBase, 2.0, ubicacion)
    {
        
    }
}