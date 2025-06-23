using NUnit.Framework;
using System.Collections.Generic;

namespace Library.Tests
{
    public class CaballeriaTests
    {
        private Caballeria caballeria;
        private Jugador jugador;
        private Jugador jugadorRival;
        private Mapa mapa;

        [SetUp]
        public void Setup()
        {
            Civilizacion civ = new Civilizacion("Armenios", new List<string>(), "");
            jugador = new Jugador("Jugador1", civ);
            jugadorRival = new Jugador("Jugador2", civ);
            caballeria = new Caballeria(jugador) { Posicion = new Point(0, 0) };
            mapa = new Mapa();
        }

        [Test]
        public void Mover_EnRango_DeberiaActualizarPosicion()
        {
            Point destino = new Point(10, 10);
            bool resultado = caballeria.Mover(destino, mapa);
            Assert.IsTrue(resultado, "La caballería debería poder moverse dentro del rango del mapa.");
            Assert.That(caballeria.Posicion.X, Is.EqualTo(10));
            Assert.That(caballeria.Posicion.Y, Is.EqualTo(10));
        }

        [Test]
        public void Mover_FueraDeRango_DeberiaFallar()
        {
            Point destino = new Point(-1, 101);
            bool resultado = caballeria.Mover(destino, mapa);
            Assert.IsFalse(resultado, "La caballería no debería poder moverse fuera del rango del mapa.");
        }

        [Test]
        public void CalcularDaño_ContraUnidadArquero_DeberiaAplicarBonus()
        {
            Arquero arquero = new Arquero(jugadorRival) { Defensa = 2 };
            double daño = caballeria.CalcularDaño(arquero);
            Assert.That(daño, Is.GreaterThan(caballeria.Ataque - arquero.Defensa), "El daño debería incluir el bonus contra arqueros.");
        }

        [Test]
        public void AtacarU_ConEnemigo_DeberiaReducirSaludYRetornarInfo()
        {
            Arquero arquero = new Arquero(jugadorRival) { Salud = 100, Posicion = new Point(1, 1)};
            jugadorRival.Unidades.Add(arquero);
            string resultado = caballeria.AtacarUnidad(
                jugador,                // jugadorAtacante
                "arquero",              // tipoUnidad
                1,                      // cantidad
                arquero.Posicion,       // coordenada
                mapa,                   // mapa
                new List<Jugador> { jugador, jugadorRival } // jugadores
            );
            Assert.That(arquero.Salud, Is.LessThan(100), "La salud del arquero debería disminuir tras el ataque.");
            Assert.IsTrue(resultado.ToLower().Contains("caballeria atacó") || resultado.ToLower().Contains("caballería atacó"), "El mensaje debe indicar que la caballería atacó.");
        }

        [Test]
        public void AtacarU_EnemigoMuere_DeberiaEliminarUnidad()
        {
            Arquero unidad = new Arquero(jugadorRival) { Salud = 5 };
            jugadorRival.Unidades.Add(unidad);
            string resultado = caballeria.AtacarUnidad(
                jugador,                // jugadorAtacante
                "arquero",              // tipoUnidad
                1,                      // cantidad
                unidad.Posicion,        // coordenada
                mapa,                   // mapa
                new List<Jugador> { jugador, jugadorRival } // jugadores
            );
            Assert.That(jugadorRival.Unidades.Contains(unidad), Is.False, "La unidad debería ser eliminada si muere.");
            Assert.IsTrue(resultado.ToLower().Contains("fue destruido"), "El mensaje debe indicar que la unidad fue destruida.");
        }

        [Test]
        public void AtacarE_EdificioRecibeDañoYPosibleDestruccion()
        {
            Cuartel edificio = new Cuartel(jugadorRival) { Vida = 10 };
            jugadorRival.Edificios.Add(edificio);
            string resultado = caballeria.AtacarEdificio(edificio);
            Assert.That(edificio.Vida, Is.LessThan(10), "El edificio debe recibir daño.");
            Assert.IsTrue(resultado.ToLower().Contains("causando"), "El mensaje debe indicar que se causó daño.");
        }
    }
}