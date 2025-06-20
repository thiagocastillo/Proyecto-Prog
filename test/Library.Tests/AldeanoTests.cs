using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;

namespace Library.Tests
{
    [TestFixture]
    public class AldeanoTests
    {
        private class DummyCivilizacion
        {
            public string Nombre { get; set; }
        }

        private class DummyJugador : Jugador
        {
            public DummyJugador(string nombre, string civilizacion)
            {
                this.Civilizacion = new DummyCivilizacion { Nombre = civilizacion };
                this.Edificios = new List<IEdificio>();
            }
            public new DummyCivilizacion Civilizacion { get; set; }
            public new List<IEdificio> Edificios { get; set; }
        }

        private class DummyRecurso : RecursoNatural
        {
            public DummyRecurso(string nombre, int vida, double tasa, Point ubicacion)
                : base(nombre, vida, tasa, ubicacion) { }
        }

        private class DummyAlmacen : IAlmacenamiento, IEdificio
        {
            public Point Posicion { get; set; }
            public Dictionary<string, int> Recursos { get; set; } = new();
        }

        private class DummyMapa : Mapa
        {
            public DummyMapa(int ancho, int alto)
            {
                this.Ancho = ancho;
                this.Alto = alto;
                this.Recursos = new List<RecursoNatural>();
            }
            public new int Ancho { get; set; }
            public new int Alto { get; set; }
            public new List<RecursoNatural> Recursos { get; set; }
        }

        [Test]
        public void Constructor_AsignaPropietario()
        {
            var jugador = new DummyJugador("J1", "Aztecas");
            var aldeano = new Aldeano(jugador);
            Assert.AreEqual(jugador, aldeano.Propietario);
        }

        [Test]
        public void PropiedadesPorDefecto()
        {
            var aldeano = new Aldeano(new DummyJugador("J1", "Aztecas"));
            Assert.AreEqual(0, aldeano.Ataque);
            Assert.AreEqual(0, aldeano.Defensa);
            Assert.AreEqual(1.0, aldeano.Velocidad);
        }

        [Test]
        public void Mover_Exitoso()
        {
            var mapa = new DummyMapa(10, 10);
            var aldeano = new Aldeano(new DummyJugador("J1", "Aztecas"));
            var destino = new Point(5, 5);
            var resultado = aldeano.Mover(destino, mapa);
            Assert.IsTrue(resultado);
            Assert.AreEqual(destino, aldeano.Posicion);
        }

        [Test]
        public void Mover_FueraDeLimites()
        {
            var mapa = new DummyMapa(5, 5);
            var aldeano = new Aldeano(new DummyJugador("J1", "Aztecas"));
            var destino = new Point(10, 10);
            var resultado = aldeano.Mover(destino, mapa);
            Assert.IsFalse(resultado);
        }

        [Test]
        public void Mover_DestinoNulo_LanzaExcepcion()
        {
            var mapa = new DummyMapa(5, 5);
            var aldeano = new Aldeano(new DummyJugador("J1", "Aztecas"));
            Assert.Throws<ArgumentNullException>(() => aldeano.Mover(null, mapa));
        }

        [Test]
        public void CalcularDaño_SiempreCero()
        {
            var aldeano = new Aldeano(new DummyJugador("J1", "Aztecas"));
            Assert.AreEqual(0, aldeano.CalcularDaño(null));
        }

        [Test]
        public void AtacarEdificio_RetornaMensaje()
        {
            var aldeano = new Aldeano(new DummyJugador("J1", "Aztecas"));
            Assert.AreEqual("Los aldeanos no atacan edificios.", aldeano.AtacarEdificio(null));
        }

        [Test]
        public void AtacarUnidad_RetornaMensaje()
        {
            var aldeano = new Aldeano(new DummyJugador("J1", "Aztecas"));
            Assert.AreEqual("Los aldeanos no atacan unidades.", aldeano.AtacarUnidad(null));
        }

        [Test]
        public void RecolectarEn_RecursoNoExiste_LanzaExcepcion()
        {
            var mapa = new DummyMapa(10, 10);
            var aldeano = new Aldeano(new DummyJugador("J1", "Aztecas"));
            Assert.Throws<InvalidOperationException>(() => aldeano.RecolectarEn(new Point(1, 1), mapa));
        }

        [Test]
        public void RecolectarEn_RecursoAgotado_LanzaExcepcion()
        {
            var mapa = new DummyMapa(10, 10);
            var recurso = new DummyRecurso("Madera", 100, 0.75, new Point(2, 2)) { Cantidad = 0 };
            mapa.Recursos.Add(recurso);
            var aldeano = new Aldeano(new DummyJugador("J1", "Aztecas"));
            Assert.Throws<InvalidOperationException>(() => aldeano.RecolectarEn(new Point(2, 2), mapa));
        }

        [Test]
        public void RecolectarEn_SinAlmacenCompatible_LanzaExcepcion()
        {
            var jugador = new DummyJugador("J1", "Aztecas");
            var mapa = new DummyMapa(10, 10);
            var recurso = new DummyRecurso("Madera", 100, 0.75, new Point(3, 3));
            mapa.Recursos.Add(recurso);
            var aldeano = new Aldeano(jugador);
            Assert.Throws<InvalidOperationException>(() => aldeano.RecolectarEn(new Point(3, 3), mapa));
        }

        [Test]
        public void RecolectarEn_AlmacenCompatible_ActualizaRecursos()
        {
            var jugador = new DummyJugador("J1", "Aztecas");
            var almacen = new DummyAlmacen { Posicion = new Point(0, 0) };
            jugador.Edificios.Add(almacen);
            var mapa = new DummyMapa(10, 10);
            var recurso = new DummyRecurso("Madera", 100, 0.75, new Point(4, 4));
            mapa.Recursos.Add(recurso);
            var aldeano = new Aldeano(jugador);

            aldeano.RecolectarEn(new Point(4, 4), mapa);

            Assert.IsTrue(almacen.Recursos.ContainsKey("Madera"));
            Assert.Greater(almacen.Recursos["Madera"], 0);
        }

        [Test]
        public void RecolectarEn_BonoAzteca()
        {
            var jugador = new DummyJugador("J1", "Aztecas");
            var almacen = new DummyAlmacen { Posicion = new Point(0, 0) };
            jugador.Edificios.Add(almacen);
            var mapa = new DummyMapa(10, 10);
            var recurso = new DummyRecurso("Madera", 100, 0.75, new Point(5, 5));
            mapa.Recursos.Add(recurso);
            var aldeano = new Aldeano(jugador);

            aldeano.RecolectarEn(new Point(5, 5), mapa);

            // El bono de +3 debe estar aplicado
            Assert.GreaterOrEqual(almacen.Recursos["Madera"], (int)(recurso.Cantidad * recurso.TasaRecoleccion));
        }

        [Test]
        public void RecolectarEn_AlmacenYaTieneRecurso_Suma()
        {
            var jugador = new DummyJugador("J1", "Aztecas");
            var almacen = new DummyAlmacen { Posicion = new Point(0, 0) };
            almacen.Recursos["Madera"] = 10;
            jugador.Edificios.Add(almacen);
            var mapa = new DummyMapa(10, 10);
            var recurso = new DummyRecurso("Madera", 100, 0.75, new Point(6, 6));
            mapa.Recursos.Add(recurso);
            var aldeano = new Aldeano(jugador);

            aldeano.RecolectarEn(new Point(6, 6), mapa);

            Assert.Greater(almacen.Recursos["Madera"], 10);
        }
    }
}