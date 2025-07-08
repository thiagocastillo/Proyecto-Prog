using NUnit.Framework;
using System.IO;
using System.Collections.Generic;

namespace Library.Domain.Tests
{
    [TestFixture]
    public class JuegoFachadaPersistenceTests
    {
        private JuegoFachada fachada;
        private string carpeta = "./partidas";
        private string nombreArchivo = "test";

        [SetUp]
        public void SetUp()
        {
            fachada = JuegoFachada.Instancia;
            if (Directory.Exists(carpeta))
                Directory.Delete(carpeta, true);
            Directory.CreateDirectory(carpeta);
            fachada.CrearNuevaPartida();
            fachada.AgregarJugadorAPartida("Ana", "armenios");
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(carpeta))
                Directory.Delete(carpeta, true);
        }

        [Test]
        public void GuardarPartida_CreaArchivo()
        {
            fachada.GuardarPartida(nombreArchivo);
            string ruta = Path.Combine(carpeta, $"Partida_{nombreArchivo}.json");
            Assert.IsTrue(File.Exists(ruta), "El archivo de la partida no fue creado.");
        }

        [Test]
        public void CargarPartida_RestauraEstado()
        {
            fachada.GuardarPartida(nombreArchivo);
            fachada.CrearNuevaPartida(); // Borra el estado actual
            fachada.CargarPartida(nombreArchivo);
            var jugadores = fachada.ObtenerJugadores();
            Assert.IsTrue(jugadores.Exists(j => j.Nombre == "Ana"), "No se restauró el jugador correctamente.");
        }

        [Test]
        public void ListarPartidas_DevuelveArchivosGuardados()
        {
            fachada.GuardarPartida(nombreArchivo);
            List<string> partidas = fachada.ListarPartidas();
            Assert.Contains(nombreArchivo, partidas, "No se listó la partida guardada.");
        }
    }
}