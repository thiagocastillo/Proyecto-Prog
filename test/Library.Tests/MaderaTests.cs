namespace Library.Tests;

public class MaderaTests
{
    private Madera madera;
    private Point ubicacion;

    [SetUp]
    
    public void Setup() 
    {
        ubicacion = new Point();
        madera = new Madera(vidaBase: 100, ubicacion: ubicacion);
    }

    [Test]
    public void CrearTasaRecoleccion()
    {
        Assert.AreEqual("Madera", madera.Nombre);
        Assert.AreEqual(0.40, madera.TasaRecoleccion, 2);
    }

    [Test]
    public void SePuedeRecolectarMadera()
    {
        int cantidadInicial = madera.Cantidad;
        int extraido = madera.Recolectar(madera.TasaRecoleccion);
        Assert.AreEqual(1, extraido);
        Assert.AreEqual(cantidadInicial - 1, madera.Cantidad);
    }

    [Test]
    public void MaderaSeExtraeTodo()
    {
        Point ubicacion = new Point();
        Madera maderaAgotable = new Madera(vidaBase: 3, ubicacion: ubicacion); 

        int extraido = maderaAgotable.Recolectar(1);
        Assert.AreEqual(1, extraido);
        Assert.AreEqual(0, maderaAgotable.Cantidad);
        Assert.True(maderaAgotable.EstaAgotado);
    }
}