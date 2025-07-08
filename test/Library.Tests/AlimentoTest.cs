using NUnit.Framework;

namespace Library.Domain.Tests
{
    public class AlimentoTests
    {
        private Alimento alimento;
        private Point ubicacion;

        [SetUp]
        public void Setup()
        {
            ubicacion = new Point();
            alimento = new Alimento(vidaBase: 100, ubicacion: ubicacion);
        }

        [Test]
        public void CrearTasaRecoleccion()
        {
            Assert.AreEqual("Alimento", alimento.Nombre);
            Assert.AreEqual(2.0, alimento.TasaRecoleccion, 2);
        }

        [Test]
        public void SePuedeRecolectarAlimento()
        {
            int cantidadInicial = alimento.Cantidad;
            int extraido = alimento.Recolectar();
            Assert.AreEqual(100, extraido);
            Assert.AreEqual(0, alimento.Cantidad);
        }

        [Test]
        public void AlimentoSeExtraeTodo()
        {
            Point ubicacion = new Point();
            Alimento alimentoAgotable = new Alimento(vidaBase: 1, ubicacion: ubicacion);

            int extraido = alimentoAgotable.Recolectar();
            Assert.AreEqual(1, extraido);
            Assert.AreEqual(0, alimentoAgotable.Cantidad);
            Assert.True(alimentoAgotable.EstaAgotado);
        }
    }
}