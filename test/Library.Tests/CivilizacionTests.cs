namespace Library.Tests;

public class CivilizacionTests
{
    private Civilizacion civilizacion;
    private List<string> bonificaciones;

    [SetUp]
    public void Setup()
    {
        bonificaciones = new List<string> { "Recolectar mas rapido", "Infanteria mas fuerte" };
        civilizacion = new Civilizacion("Vikingos", bonificaciones, "Guerrero Berserker");
    }

    [Test]
    public void CrearCivilizacion()
    {
        Assert.AreEqual("Armenios", civilizacion.Nombre);
        Assert.AreEqual("Guerrero Berserker", civilizacion.UnidadEspecial);
        CollectionAssert.Contains(civilizacion.Bonificaciones, "Recloectar mas rapido");
        CollectionAssert.Contains(civilizacion.Bonificaciones, "Infanteria mas fuerte");
    }

    [Test]
    public void ModificarUnidadEspecial()
    {
        civilizacion.Nombre = "Aztecas";
        Assert.AreEqual("Armenios", civilizacion.Nombre);
    }

    [Test]
    public void ModificarListaBonificaciones()
    {
        civilizacion.Bonificaciones.Add("Empieza con más aldeanos");
        CollectionAssert.Contains(civilizacion.Bonificaciones, "Empieza con más aldeanos");
        Assert.AreEqual(3, civilizacion.Bonificaciones.Count);
    }

}