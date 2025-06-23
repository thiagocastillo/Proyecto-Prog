using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace Library.Tests
{
    public class InfanteriaTests
    {
        private Jugador jugadorAzteca;
        private Jugador jugadorRival;
        private Infanteria infanteria;
        private Mapa mapa;

        [SetUp]
        public void SetUp()
        {
            Civilizacion civilizacionAzteca = new Civilizacion("aztecas", new List<string>(), "Guerrero Jaguar");
            jugadorAzteca = new Jugador("Azteca", civilizacionAzteca);

            Civilizacion civilizacionRival = new Civilizacion("armenios", new List<string>(), "Arquero Compuesto");
            jugadorRival = new Jugador("Rival", civilizacionRival);

            infanteria = new Infanteria(jugadorAzteca) { Posicion = new Point(0, 0), Salud = 100 };
            mapa = new Mapa();
        }

        [Test]
        public void Mover_DestinoValido_MueveCorrectamente()
        {
            Point destino = new Point(10, 10);

            bool resultado = infanteria.Mover(destino, mapa);

            Assert.IsTrue(resultado);
            Assert.That(infanteria.Posicion.X, Is.EqualTo(10));
            Assert.That(infanteria.Posicion.Y, Is.EqualTo(10));
        }

        [Test]
        public void Mover_DestinoFueraDeMapa_LanzaExcepcion()
        {
            Point destino = new Point(-1, 200);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => infanteria.Mover(destino, mapa));
            Assert.That(ex.Message, Does.Contain("No se pudo mover la unidad"));
        }

        [Test]
        public void AtacarU_ContraCaballeria_AplicaBonus()
        {
            Caballeria caballeria = new Caballeria(jugadorRival) { Salud = 100, Posicion = new Point(1, 1) };
            string resultado = infanteria.AtacarUnidad(
                jugadorAzteca,
                "caballeria",
                1,
                caballeria.Posicion,
                mapa,
                new List<Jugador> { jugadorAzteca, jugadorRival }
            );

            Assert.That(caballeria.Salud, Is.LessThan(100));
            Assert.IsTrue(resultado.Contains("Infanteria atacó a Caballeria"));
        }

        [Test]
        public void AtacarU_ContraInfanteriaAztecaGuerreroJaguar_AplicaBonus()
        {
            Infanteria infanteriaObjetivo = new Infanteria(jugadorRival) { Salud = 100, Posicion = new Point(2, 2) };
            jugadorRival.Unidades.Add(infanteriaObjetivo);

            string resultado = infanteria.AtacarUnidad(
                jugadorAzteca,
                "infanteria",
                1,
                infanteriaObjetivo.Posicion,
                mapa,
                new List<Jugador> { jugadorAzteca, jugadorRival }
            );

            Assert.That(infanteriaObjetivo.Salud, Is.LessThan(100));
            Assert.IsTrue(resultado.Contains("Infanteria atacó"));
        }

        [Test]
        public void AtacarE_ContraEdificio_CausaDaño()
        {
            Casa casa = new Casa(jugadorRival) { Vida = 1000 };

            string resultado = infanteria.AtacarEdificio(casa);

            Assert.That(casa.Vida, Is.LessThan(1000));
            Assert.IsTrue(resultado.Contains("atacó el edificio"));
        }

        [Test]
        public void AtacarU_UnidadEliminada_SeEliminaDeLista()
        {
            Arquero objetivo = new Arquero(jugadorRival) { Salud = 1, Posicion = new Point(3, 3) };
            jugadorRival.Unidades.Add(objetivo);

            string resultado = infanteria.AtacarUnidad(
                jugadorAzteca,
                "arquero",
                1,
                objetivo.Posicion,
                mapa,
                new List<Jugador> { jugadorAzteca, jugadorRival }
            );

            Assert.IsFalse(jugadorRival.Unidades.Contains(objetivo));
            Assert.IsTrue(resultado.Contains("fue destruida"));
        }
    }
}