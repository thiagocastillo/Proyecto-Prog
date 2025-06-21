namespace Library.Tests;

public class PiedraTests
{
    private Piedra piedra;
    private Point ubicacion;

    [SetUp]
    
    public void Setup() 
    {
        ubicacion = new Point();
        piedra = new Piedra(vidaBase: 100, ubicacion: ubicacion);
    }

    [Test]
    public void CrearTasaRecoleccion()
    {
        Assert.AreEqual("Piedra", piedra.Nombre);
        Assert.AreEqual(0.50, piedra.TasaRecoleccion, 2);
    }

    [Test]
    public void SePuedeRecolectarOro()
    {
        int cantidadInicial = piedra.Cantidad;
        int extraido = piedra.Recolectar(piedra.TasaRecoleccion);
        Assert.AreEqual(1, extraido);
        Assert.AreEqual(cantidadInicial - 1, piedra.Cantidad);
    }

    [Test]
    public void PiedraSeExtraeTodo()
    {
        var oroAgotable = new Piedra(vidaBase: 2, ubicacion: ubicacion); 

        int extraido = oroAgotable.Recolectar(1);
        Assert.AreEqual(1, extraido);
        Assert.AreEqual(0, oroAgotable.Cantidad);
        Assert.True(oroAgotable.EstaAgotado);
    }
}