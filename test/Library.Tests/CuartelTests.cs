using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace Library.Tests
{
    public class CuartelTests
    {
        private Jugador jugador;

        [SetUp]
        public void Setup()
        {
            Civilizacion civilizacion = new Civilizacion("Aztecas", new List<string>(), "GuerreroJaguar");
            jugador = new Jugador("Luis", civilizacion);
        }

        [Test]
        public void Constructor_AsignacionCorrecta()
        {
            Cuartel cuartel = new Cuartel(jugador);

            Assert.AreEqual(jugador, cuartel.Propietario);
            Assert.AreEqual(10000, cuartel.Vida);
        }

        [Test]
        public void TiempoConstruccion_Default_NoConstruido()
        {
            Cuartel cuartel = new Cuartel(jugador);

            Assert.IsFalse(cuartel.EstaConstruido);
        }

        [Test]
        public void TiempoConstruccion_TotalYRestante()
        {
            Cuartel cuartel = new Cuartel(jugador);

            Assert.AreEqual(60, cuartel.TiempoConstruccionTotal);
            Assert.That(cuartel.TiempoConstruccionRestante, Is.InRange(59, 60));
        }
    }
}