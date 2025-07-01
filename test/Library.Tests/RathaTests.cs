using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace Library.Domain.Tests
{
    public class RathaTests
    {
        private Jugador jugador;
        private Jugador jugadorEnemigo;
        private Mapa mapa;

        [SetUp]
        public void Setup()
        {
            Civilizacion civilizacion = new Civilizacion("Mayas", new List<string>(), "Ratha");
            jugador = new Jugador("Pedro", civilizacion);
            jugadorEnemigo = new Jugador("Otro", civilizacion);
            mapa = new Mapa();
        }

        [Test]
        public void Mover_PosicionValida_MueveCorrectamente()
        {
            Ratha ratha = new Ratha(jugador);
            Point destino = new Point(2, 2);

            bool resultado = ratha.Mover(destino, mapa);

            Assert.IsTrue(resultado);
            Assert.AreEqual(destino, ratha.Posicion);
        }

        [Test]
        public void Mover_PosicionInvalida_DevuelveFalse()
        {
            Ratha ratha = new Ratha(jugador);
            Point destino = new Point(-5, 100);

            bool resultado = ratha.Mover(destino, mapa);

            Assert.IsFalse(resultado);
        }

        [Test]
        public void AtacarUnidad_ContraArquero_BonusAplicado()
        {
            Ratha ratha = new Ratha(jugador);
            Arquero enemigo = new Arquero(jugadorEnemigo) { Salud = 50, Posicion = new Point(1, 1) };
            jugadorEnemigo.Unidades.Add(enemigo);
            
            string resultado = ratha.AtacarUnidad(
                jugador,
                "arquero",
                1,
                enemigo.Posicion,
                mapa,
                new List<Jugador> { jugador, jugadorEnemigo }
            );

            Assert.IsTrue(resultado.Contains("ataco a Arquero"));
            Assert.Less(enemigo.Salud, 50);
        }

        [Test]
        public void AtacarEdificio_DisminuyeVida()
        {
            Ratha ratha = new Ratha(jugador);
            Casa casa = new Casa(jugadorEnemigo);
            int vidaInicial = casa.Vida;

            string resultado = ratha.AtacarEdificio(casa);

            Assert.IsTrue(resultado.Contains("ataco el edificio Casa"));
            Assert.Less(casa.Vida, vidaInicial);
        }
    }
}