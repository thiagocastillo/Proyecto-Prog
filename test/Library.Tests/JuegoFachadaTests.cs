using NUnit.Framework;
using System.Drawing;
using System.Collections.Generic;

namespace Library.Domain.Tests
{
    [TestFixture]
    public class JuegoFachadaTests
    {
        private JuegoFachada fachada;

        [SetUp]
        public void Setup()
        {
            fachada = new JuegoFachada();
        }

        [Test]
        public void CrearNuevaPartida_InicializaPartida()
        {
            fachada.CrearNuevaPartida();
            List<string> civilizaciones = fachada.ObtenerCivilizacionesDisponibles();
            Assert.IsNotNull(civilizaciones);
            Assert.IsTrue(civilizaciones.Count > 0);
        }

        [Test]
        public void AgregarJugadorAPartida_AgregaJugadorCorrectamente()
        {
            fachada.CrearNuevaPartida();
            fachada.AgregarJugadorAPartida("Juan", "aztecas");
            List<Jugador> jugadores = fachada.ObtenerJugadores();
            Assert.AreEqual(1, jugadores.Count);
            Assert.AreEqual("Juan", jugadores[0].Nombre);
        }

        [Test]
        public void AgregarJugadorAPartida_NombreRepetido_LanzaExcepcion()
        {
            fachada.CrearNuevaPartida();
            fachada.AgregarJugadorAPartida("Juan", "aztecas");
            Assert.Throws<InvalidOperationException>(() =>
                fachada.AgregarJugadorAPartida("Juan", "armenios"));
        }

        [Test]
        public void ConstruirEdificio_Correcto_DescuentaRecursos()
        {
            fachada.CrearNuevaPartida();
            fachada.AgregarJugadorAPartida("Ana", "armenios");
            Jugador jugador = fachada.ObtenerJugadores()[0];
            jugador.Recursos["Madera"] = 200;
            fachada.ConstruirEdificio("Ana", "casa", new Point(1, 1));
            Assert.AreEqual(150, jugador.Recursos["Madera"]);
            Assert.IsTrue(jugador.Edificios.Exists(e => e.Posicion.X == 1 && e.Posicion.Y == 1));
        }

        [Test]
        public void EntrenarUnidad_Aldeano_DescuentaRecursos()
        {
            fachada.CrearNuevaPartida();
            fachada.AgregarJugadorAPartida("Luis", "aztecas");
            Jugador jugador = fachada.ObtenerJugadores()[0];
            jugador.Recursos["Alimento"] = 100;
            jugador.PoblacionActual = 0;
            jugador.PoblacionMaxima = 5;
            fachada.EntrenarUnidad("Luis", "aldeano", new Point(2, 2));
            Assert.AreEqual(50, jugador.Recursos["Alimento"]);
            Assert.IsTrue(jugador.Unidades.Exists(u => u.Posicion.X == 2 && u.Posicion.Y == 2));
        }
    }
}