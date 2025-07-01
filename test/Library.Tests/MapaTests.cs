using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Library.Tests
{
    [TestFixture]
    public class MapaTests
    {
        [Test]
        public void Constructor_GeneraRecursosAleatorios()
        {
            var mapa = new Mapa();
            Assert.IsTrue(mapa.Recursos.Count >= 50 && mapa.Recursos.Count <= 200);
        }

        [Test]
        public void GenerarRecursosAleatorios_CantidadEnRango()
        {
            var mapa = new Mapa();
            mapa.GenerarRecursosAleatorios(10, 20);
            Assert.IsTrue(mapa.Recursos.Count >= 10 && mapa.Recursos.Count <= 20);
        }

        [Test]
        public void ObtenerUnidadesEn_DevuelveUnidadesCorrectas()
        {
            var mapa = new Mapa();
            var punto = new Point(5, 5);
            var unidadMock = new Aldeano(null) { Posicion = punto };
            var jugador = new Jugador("Test", new Civilizacion("aztecas", null, "")) { Unidades = new List<IUnidad> { unidadMock } };
            var unidades = mapa.ObtenerUnidadesEn(punto, new List<Jugador> { jugador });
            Assert.Contains(unidadMock, unidades);
        }

        [Test]
        public void ObtenerEdificiosEn_DevuelveEdificiosCorrectos()
        {
            var mapa = new Mapa();
            var punto = new Point(2, 2);
            var propietario = new Jugador("Test", new Civilizacion("aztecas", null, ""));
            var edificioMock = new Casa(propietario) { Posicion = punto };
            var jugador = new Jugador("Test", new Civilizacion("aztecas", null, "")) { Edificios = new List<IEdificio> { edificioMock } };
            var edificios = mapa.ObtenerEdificiosEn(punto, new List<Jugador> { jugador });
            Assert.Contains(edificioMock, edificios);
        }

        [Test]
        public void MostrarMapa_DevuelveMensajeCorrecto()
        {
            var mapa = new Mapa();
            var resultado = mapa.MostrarMapa(new List<Jugador>());
            Assert.AreEqual("Abriendo Mapa en el Bloc de notas...", resultado);
            Assert.IsTrue(System.IO.File.Exists("mapa.txt"));
        }
    }
}