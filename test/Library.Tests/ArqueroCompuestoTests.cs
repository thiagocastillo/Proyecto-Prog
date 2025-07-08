using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace Library.Domain.Tests
{
    public class ArqueroCompuestoTests
    {
        private Jugador jugadorArmenio;
        private Jugador jugadorRival;
        private ArqueroCompuesto arqueroCompuesto;
        private Mapa mapa;

        [SetUp]
        public void SetUp()
        {
            Civilizacion civilizacionArmenia = new Civilizacion("Armenios", new List<string>(), "Arquero Compuesto");
            jugadorArmenio = new Jugador("Armenio", civilizacionArmenia);

            Civilizacion civilizacionRival = new Civilizacion("Persas", new List<string>(), "Elefante");
            jugadorRival = new Jugador("Rival", civilizacionRival);

            arqueroCompuesto = new ArqueroCompuesto(jugadorArmenio) { Posicion = new Point(0, 0), Salud = 100 };
            mapa = new Mapa();
        }

        [Test]
        public void Mover_DestinoValido_MueveCorrectamente()
        {
            Point destino = new Point(5, 5);

            bool resultado = arqueroCompuesto.Mover(destino, mapa);

            Assert.IsTrue(resultado);
            Assert.That(arqueroCompuesto.Posicion.X, Is.EqualTo(5));
            Assert.That(arqueroCompuesto.Posicion.Y, Is.EqualTo(5));
        }

        [Test]
        public void Mover_DestinoFueraDeMapa_LanzaExcepcion()
        {
            Point destino = new Point(-10, 100);

            var ex = Assert.Throws<InvalidOperationException>(() => arqueroCompuesto.Mover(destino, mapa));
            Assert.That(ex.Message, Does.Contain("No se pudo mover la unidad"));
        }

        [Test]
        public void AtacarU_ContraInfanteria_CausaDaño()
        {
            Infanteria objetivo = new Infanteria(jugadorRival) { Salud = 100, Posicion = new Point(1, 1) };
            jugadorRival.Unidades.Add(objetivo);

            string resultado = arqueroCompuesto.AtacarUnidad(
                jugadorArmenio,
                "infanteria",
                1,
                objetivo.Posicion,
                mapa,
                new List<Jugador> { jugadorArmenio, jugadorRival }
            );

            TestContext.WriteLine($"Mensaje devuelto: {resultado}");

            Assert.That(objetivo.Salud, Is.LessThan(100));
            Assert.IsTrue(resultado.Contains("atacó")); // Ajusta aquí según el mensaje real
        }

        [Test]
        public void AtacarE_ContraEdificio_CausaDaño()
        {
            Casa casa = new Casa(jugadorRival) { Vida = 1000 };

            string resultado = arqueroCompuesto.AtacarEdificio(casa);

            Assert.That(casa.Vida, Is.LessThan(1000));
            Assert.IsTrue(resultado.Contains("atacó el edificio"));
        }

        [Test]
        public void AtacarU_UnidadEliminada_SeEliminaDeLista()
        {
            Infanteria objetivo = new Infanteria(jugadorRival) { Salud = 1, Posicion = new Point(2, 2) };
            jugadorRival.Unidades.Add(objetivo);

            string resultado = arqueroCompuesto.AtacarUnidad(
                jugadorArmenio,
                "infanteria",
                1,
                objetivo.Posicion,
                mapa,
                new List<Jugador> { jugadorArmenio, jugadorRival }
            );

            Assert.IsFalse(jugadorRival.Unidades.Contains(objetivo));
            Assert.IsTrue(resultado.Contains("fue destruida"));
        }

        [Test]
        public void Constructor_Armenios_BonusAtaque()
        {
            Assert.That(arqueroCompuesto.Ataque, Is.EqualTo(402));
        }

        [Test]
        public void Constructor_ValoresBaseCorrectos()
        {
            Assert.That(arqueroCompuesto.Defensa, Is.EqualTo(5));
            Assert.That(arqueroCompuesto.Velocidad, Is.EqualTo(1.3));
            Assert.That(arqueroCompuesto.TiempoDeCreacion, Is.EqualTo(12));
        }
    }
}