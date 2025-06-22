using NUnit.Framework;
using System.Collections.Generic;

namespace Library.Tests
{
    public class ArqueroTests
    {
        private Jugador jugadorArmenio;
        private Jugador jugadorEnemigo;
        private Arquero arquero;
        private Mapa mapa;

        [SetUp]
        public void Setup()
        {
            Civilizacion civilizacionArmenia = new Civilizacion("armenios", new List<string>(), "Arquero Compuesto");
            jugadorArmenio = new Jugador("Jugador1", civilizacionArmenia);

            Civilizacion civilizacionEnemiga = new Civilizacion("aztecas", new List<string>(), "Guerrero Jaguar");
            jugadorEnemigo = new Jugador("Jugador2", civilizacionEnemiga);

            arquero = new Arquero(jugadorArmenio) { Salud = 100, Posicion = new Point(1, 1) };
            mapa = new Mapa();
        }

        [Test]
        public void Arquero_Mover_PosicionValida_MueveCorrectamente()
        {
            Point destino = new Point(5, 5);

            bool resultado = arquero.Mover(destino, mapa);

            Assert.IsTrue(resultado);
            Assert.That(arquero.Posicion.X, Is.EqualTo(5));
            Assert.That(arquero.Posicion.Y, Is.EqualTo(5));
        }

        [Test]
        public void Arquero_AtacarU_ContraInfanteria_InfligeDañoConBonus()
        {
            Infanteria infanteria = new Infanteria(jugadorEnemigo) { Salud = 100, Posicion = new Point(2, 2) };
            string resultado = arquero.AtacarUnidad(
                jugadorArmenio,           // jugadorAtacante
                "infanteria",             // tipoUnidad
                1,                        // cantidad
                infanteria.Posicion,      // coordenada
                mapa,                     // mapa
                new List<Jugador> { jugadorArmenio, jugadorEnemigo } // jugadores
            );

            Assert.IsTrue(resultado.Contains("hizo"));
            Assert.That(infanteria.Salud, Is.LessThan(100));
        }

        [Test]
        public void Arquero_AtacarU_ContraUnidadConDefensaAlta_NoDañoNegativo()
        {
            Caballeria unidadDefensiva = new Caballeria(jugadorEnemigo)
            {
                Salud = 100,
                Posicion = new Point(2, 2)
            };

            string resultado = arquero.AtacarUnidad(
                jugadorArmenio,
                "caballeria",
                1,
                unidadDefensiva.Posicion,
                mapa,
                new List<Jugador> { jugadorArmenio, jugadorEnemigo }
            );

            Assert.That(unidadDefensiva.Salud, Is.LessThanOrEqualTo(100));
            Assert.IsTrue(resultado.Contains("daño"));
        }

        [Test]
        public void Arquero_AtacarE_EdificioRecibeDaño()
        {
            Casa edificio = new Casa(jugadorEnemigo) { Vida = 1000 };

            string resultado = arquero.AtacarEdificio(edificio);

            Assert.IsTrue(resultado.Contains("causando"));
            Assert.That(edificio.Vida, Is.LessThan(1000));
        }
    }
}