using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Library.Domain.Tests
{
    [TestFixture]
    public class MapaTests
    {
        [TearDown]
        public void Cleanup()
        {
            if (File.Exists("mapa.txt")) File.Delete("mapa.txt");
            if (File.Exists("mapa.html")) File.Delete("mapa.html");
        }

        [Test]
        public void Constructor_GeneraRecursosAleatorios_EnRangoYUnicos()
        {
            var mapa = new Mapa();
            Assert.That(mapa.Recursos.Count, Is.InRange(50, 200));
            var posiciones = mapa.Recursos.Select(r => (r.Ubicacion.X, r.Ubicacion.Y)).ToList();
            Assert.That(posiciones.Distinct().Count(), Is.EqualTo(posiciones.Count));
        }

        [Test]
        public void GenerarRecursosAleatorios_CantidadEnRangoYUnicos()
        {
            var mapa = new Mapa();
            mapa.GenerarRecursosAleatorios(10, 15);
            Assert.That(mapa.Recursos.Count, Is.InRange(10, 15));
            var posiciones = mapa.Recursos.Select(r => (r.Ubicacion.X, r.Ubicacion.Y)).ToList();
            Assert.That(posiciones.Distinct().Count(), Is.EqualTo(posiciones.Count));
        }

        [Test]
        public void ObtenerUnidadesEn_DevuelveUnidadesCorrectas()
        {
            var mapa = new Mapa();
            var punto = new Point(5, 5);
            var unidad = new Aldeano(null) { Posicion = punto };
            var jugador = new Jugador("Test", new Civilizacion("aztecas", null, "")) { Unidades = new List<IUnidad> { unidad } };
            var unidades = mapa.ObtenerUnidadesEn(punto, new List<Jugador> { jugador });
            Assert.Contains(unidad, unidades);
        }

        [Test]
        public void ObtenerUnidadesEn_ListaJugadoresVacia_NoFalla()
        {
            var mapa = new Mapa();
            var unidades = mapa.ObtenerUnidadesEn(new Point(1, 1), new List<Jugador>());
            Assert.That(unidades, Is.Empty);
        }

        [Test]
        public void ObtenerUnidadesEn_CoordenadaFueraDeRango_NoFalla()
        {
            var mapa = new Mapa();
            var jugador = new Jugador("Test", new Civilizacion("aztecas", null, "")) { Unidades = new List<IUnidad>() };
            var unidades = mapa.ObtenerUnidadesEn(new Point(999, 999), new List<Jugador> { jugador });
            Assert.That(unidades, Is.Empty);
        }

        [Test]
        public void ObtenerEdificiosEn_DevuelveEdificiosCorrectos()
        {
            var mapa = new Mapa();
            var punto = new Point(2, 2);
            var propietario = new Jugador("Test", new Civilizacion("aztecas", null, ""));
            var edificio = new Casa(propietario) { Posicion = punto };
            var jugador = new Jugador("Test", new Civilizacion("aztecas", null, "")) { Edificios = new List<IEdificio> { edificio } };
            var edificios = mapa.ObtenerEdificiosEn(punto, new List<Jugador> { jugador });
            Assert.Contains(edificio, edificios);
        }

        [Test]
        public void ObtenerEdificiosEn_ListaJugadoresVacia_NoFalla()
        {
            var mapa = new Mapa();
            var edificios = mapa.ObtenerEdificiosEn(new Point(1, 1), new List<Jugador>());
            Assert.That(edificios, Is.Empty);
        }

        [Test]
        public void MostrarMapaTXT_DevuelveMensajeYArchivo()
        {
            var mapa = new Mapa();
            var resultado = mapa.MostrarMapaTXT(new List<Jugador>());
            Assert.That(resultado, Does.Contain("Bloc de notas"));
            Assert.That(File.Exists("mapa.txt"), Is.True);
        }

        [Test]
        public void MostrarMapaHtml_DevuelveMensajeYArchivo()
        {
            var mapa = new Mapa();
            var resultado = mapa.MostrarMapaHtml(new List<Jugador>());
            Assert.That(resultado, Does.Contain("Mapa abierto"));
            Assert.That(File.Exists("mapa.html"), Is.True);
        }

        [Test]
        public void MostrarMapaTXT_SimbolosCorrectos()
        {
            var mapa = new Mapa();
            
            mapa.Recursos.Clear();
            mapa.Recursos.Add(new Arbol(100, new Point(1, 1)));
            mapa.Recursos.Add(new Piedra(100, new Point(2, 2)));
            mapa.Recursos.Add(new Oro(100, new Point(3, 3)));
            var resultado = mapa.MostrarMapaTXT(new List<Jugador>());
            var contenido = File.ReadAllText("mapa.txt");
            Assert.That(contenido, Does.Contain("T"));
            Assert.That(contenido, Does.Contain("#"));
            Assert.That(contenido, Does.Contain("$"));
        }

        [Test]
        public void MostrarMapaTXT_UnidadYEdificio_SimbolosCorrectos()
        {
            var mapa = new Mapa();
           
            mapa.Recursos.Clear();
            var jugador = new Jugador("Test", new Civilizacion("aztecas", null, ""));
            var aldeano = new Aldeano(jugador) { Posicion = new Point(0, 0) };
            var cc = new CentroCivico(jugador) { Posicion = new Point(4, 4) };
            jugador.Unidades = new List<IUnidad> { aldeano };
            jugador.Edificios = new List<IEdificio> { cc };
            mapa.MostrarMapaTXT(new List<Jugador> { jugador });
            var contenido = File.ReadAllText("mapa.txt");
            Assert.That(contenido, Does.Contain("A"));
            Assert.That(contenido, Does.Contain("C"));
        }

        [Test]
        public void MostrarMapaHtml_SimbolosCorrectos()
        {
            var mapa = new Mapa();
            mapa.Recursos.Clear();
            mapa.Recursos.Add(new Arbol(100, new Point(1, 1)));
            mapa.Recursos.Add(new Piedra(100, new Point(2, 2)));
            mapa.Recursos.Add(new Oro(100, new Point(3, 3)));
            mapa.MostrarMapaHtml(new List<Jugador>());
            var contenido = File.ReadAllText("mapa.html");
            Assert.That(contenido, Does.Contain("T"));
            Assert.That(contenido, Does.Contain("#"));
            Assert.That(contenido, Does.Contain("$"));
        }

        [Test]
        public void MostrarMapaHtml_UnidadYEdificio_SimbolosCorrectos()
        {
            var mapa = new Mapa();
            mapa.Recursos.Clear();
            var jugador = new Jugador("Test", new Civilizacion("aztecas", null, ""));
            var aldeano = new Aldeano(jugador) { Posicion = new Point(0, 0) };
            var cc = new CentroCivico(jugador) { Posicion = new Point(4, 4) };
            jugador.Unidades = new List<IUnidad> { aldeano };
            jugador.Edificios = new List<IEdificio> { cc };
            mapa.MostrarMapaHtml(new List<Jugador> { jugador });
            var contenido = File.ReadAllText("mapa.html");
            Assert.That(contenido, Does.Contain("A"));
            Assert.That(contenido, Does.Contain("C"));
        }

        [Test]
        public void Mapa_SinJugadores_NiRecursos_NoFalla()
        {
            var mapa = new Mapa();
            mapa.Recursos.Clear();
            var resultadoTxt = mapa.MostrarMapaTXT(new List<Jugador>());
            var resultadoHtml = mapa.MostrarMapaHtml(new List<Jugador>());
            Assert.That(resultadoTxt, Is.Not.Null);
            Assert.That(resultadoHtml, Is.Not.Null);
        }
    }
}