namespace Library.Tests;

public class PointTests
{

    [Test]
    public void LeerCoordenadas()
    {
        Point coordenadas = new Point();
        coordenadas.X = 1;
        coordenadas.Y = 2;
        
        Assert.AreEqual(1, coordenadas.X);
        Assert.AreEqual(2, coordenadas.Y);
    }

    [Test]
    public void InicializarEnCeroCero()
    {
        Point coordenadas = new Point();
        
        Assert.AreEqual(0, coordenadas.X);
        Assert.AreEqual(0, coordenadas.Y);
    }

    [Test]
    public void ConParametros()
    {
        Point coordenadas = new Point(3, 7);
        
        Assert.AreEqual(3, coordenadas.X);
        Assert.AreEqual(7, coordenadas.Y);
    }
}