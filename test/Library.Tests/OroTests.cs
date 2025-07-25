using NUnit.Framework;

namespace Library.Domain.Tests
{
    public class OroTests
    {
        private Oro oro;
        private Point ubicacion;

        [SetUp]
        public void Setup()
        {
            ubicacion = new Point();
            oro = new Oro(vidaBase: 100, ubicacion: ubicacion);
        }

        [Test]
        public void CrearTasaRecoleccion()
        {
            Assert.AreEqual("Oro", oro.Nombre);
            Assert.AreEqual(0.50, oro.TasaRecoleccion, 2);
        }

        [Test]
        public void SePuedeRecolectarOro()
        {
            int cantidadInicial = oro.Cantidad;
            int extraido = oro.Recolectar();
            Assert.AreEqual(50, extraido);
            Assert.AreEqual(0, oro.Cantidad);
        }

        [Test]
        public void OroSeExtraeTodo()
        {
            Point ubicacion = new Point();
            Oro oroAgotable = new Oro(vidaBase: 1, ubicacion: ubicacion);

            int extraido = oroAgotable.Recolectar();
            Assert.AreEqual(1, extraido);
            Assert.AreEqual(0, oroAgotable.Cantidad);
            Assert.True(oroAgotable.EstaAgotado);
        }
    }
}