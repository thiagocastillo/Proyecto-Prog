namespace Library.Domain.Tests;


public class GranjaTests
{
    private Granja granja;
    private Jugador jugador;

    [SetUp]
    public void Setup()
    {
        Civilizacion civilizacion = new Civilizacion("Vikingos", new List<string>(), "Guerrero");
        jugador = new Jugador("Jugador1", civilizacion);
        granja = new Granja(jugador);
    }

    [Test]
    public void Propiedades()
    {
        Assert.AreEqual(jugador, granja.Propietario);
        Assert.AreEqual(10000, granja.Vida);
        Assert.AreEqual(500, granja.CapacidadMaxima);
        Assert.IsNotNull(granja.Recursos);
        Assert.AreEqual(0, granja.Recursos.Count);
    }

    [Test]
    public void Ubicacion()
    {
        Point ubicacion = new Point();
        granja.Posicion = ubicacion;
        Assert.AreEqual(ubicacion, granja.Posicion);
    }

    [Test]
    public void Eficiencia_Distancia_MenorOIgualAUno()
    {    
        double eficienciaCero = granja.Eficiencia(0);
        double eficienciaUno = granja.Eficiencia(1);

        Assert.AreEqual(1.0, eficienciaCero, 0.01);
        Assert.AreEqual(1.0, eficienciaUno, 0.01);
    }

    [Test] 

    public void Eficiencia_Distancia_MayorIgualADiez()
    {
        double eficienciaDiez = granja.Eficiencia(10);
        double eficienciaMayor = granja.Eficiencia(15);

        Assert.AreEqual(0.1, eficienciaDiez, 0.01);
        Assert.AreEqual(0.1, eficienciaMayor, 0.01);
    }
}