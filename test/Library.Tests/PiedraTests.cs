using NUnit.Framework;

namespace Library.Domain.Tests
{
    public class PiedraTests
    {
        private Piedra piedra;
        private Point ubicacion;

        [SetUp]
        public void Setup()
        {
            ubicacion = new Point();
            piedra = new Piedra(vidaBase: 2, ubicacion: ubicacion);
        }

        [Test]
        public void CrearTasaRecoleccion_DeberiaSerCorrecta()
        {
            Assert.AreEqual("Piedra", piedra.Nombre);
            Assert.That(piedra.TasaRecoleccion, Is.EqualTo(0.5).Within(0.01));
        }

        [Test]
        public void SePuedeRecolectarPiedra_DecrementaCantidad()
        {
            int cantidadInicial = piedra.Cantidad;
            int extraido = piedra.Recolectar();
            Assert.AreEqual(1, extraido);
            Assert.AreEqual(0, piedra.Cantidad);
        }

        [Test]
        public void PiedraSeExtraeTodo_QuedaAgotada()
        {
            var ubicacionLocal = new Point();
            var piedraAgotable = new Piedra(vidaBase: 100, ubicacion: ubicacionLocal);

            int extraido = piedraAgotable.Recolectar();
            Assert.AreEqual(50, extraido);
            Assert.AreEqual(0, piedraAgotable.Cantidad);
            Assert.IsTrue(piedraAgotable.EstaAgotado);
        }
    }
}