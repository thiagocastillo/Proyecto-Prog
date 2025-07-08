using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace Library.Domain.Tests
{
    public class AldeanoTests
    {
        private Jugador jugador;
        private Aldeano aldeano;
        private Mapa mapa;

        [SetUp]
        public void Setup()
        {
            Civilizacion civilizacion = new Civilizacion("Aztecas", new List<string>(), "");
            jugador = new Jugador("Jugador1", civilizacion);
            aldeano = new Aldeano(jugador) { Salud = 50, Posicion = new Point(0, 0) };
            mapa = new Mapa();
        }

        [Test]
        public void Constructor_AsignaPropietario()
        {
            Assert.AreEqual(jugador, aldeano.Propietario);
        }

        [Test]
        public void PropiedadesPorDefecto()
        {
            Assert.AreEqual(0, aldeano.Ataque);
            Assert.AreEqual(0, aldeano.Defensa);
            Assert.AreEqual(1.0, aldeano.Velocidad);
        }

        [Test]
        public void Mover_PosicionValida_ActualizaPosicion()
        {
            Point destino = new Point(5, 5);
            bool resultado = aldeano.Mover(destino, mapa);
            Assert.IsTrue(resultado);
            Assert.AreEqual(destino, aldeano.Posicion);
        }

        [Test]
        public void Mover_PosicionInvalida_NoMueve()
        {
            Point destino = new Point(-1, 100);
            bool resultado = aldeano.Mover(destino, mapa);
            Assert.IsFalse(resultado);
        }

        [Test]
        public void Mover_DestinoNulo_LanzaExcepcion()
        {
            Assert.Throws<System.ArgumentNullException>(() => aldeano.Mover(null, mapa));
        }

        [Test]
        public void CalcularDaño_SiempreCero()
        {
            Assert.AreEqual(0, aldeano.CalcularDaño(null));
        }

        [Test]
        public void AtacarUnidad_SiempreDevuelveMensajeNoAtaca()
        {
            MockUnidad dummy = new MockUnidad();
            string res = aldeano.AtacarUnidad(dummy);
            Assert.That(res, Is.EqualTo("Los aldeanos no atacan unidades."));
        }

        [Test]
        public void AtacarEdificio_SiempreDevuelveMensajeNoAtaca()
        {
            MockEdificio dummy = new MockEdificio();
            string res = aldeano.AtacarEdificio(dummy);
            Assert.That(res, Is.EqualTo("Los aldeanos no atacan edificios."));
        }

        [Test]
        public void RecolectarEn_RecursoNoExiste_LanzaExcepcion()
        {
            Assert.Throws<System.InvalidOperationException>(() =>
                aldeano.RecolectarEn(new Point(1, 1), mapa));
        }

        [Test]
        public void RecolectarEn_RecursoAgotado_LanzaExcepcion()
        {
            Arbol recurso = new Arbol(0, new Point(2, 2));
            mapa.Recursos.Add(recurso);
            Assert.Throws<System.InvalidOperationException>(() =>
                aldeano.RecolectarEn(new Point(2, 2), mapa));
        }

        [Test]
        public void RecolectarEn_AlmacenCompatible_ActualizaRecursos()
        {
            DepositoMadera almacen = new DepositoMadera(jugador) { Posicion = new Point(0, 0) };
            jugador.Edificios.Add(almacen);
            Arbol recurso = new Arbol(100, new Point(4, 4));
            mapa.Recursos.Add(recurso);

            aldeano.RecolectarEn(new Point(4, 4), mapa);

            Assert.IsTrue(almacen.Recursos.ContainsKey("Madera"));
            Assert.Greater(almacen.Recursos["Madera"], 0);
        }

        [Test]
        public void RecolectarEn_BonoAzteca()
        {
            DepositoMadera almacen = new DepositoMadera(jugador) { Posicion = new Point(0, 0) };
            jugador.Edificios.Add(almacen);
            Arbol recurso = new Arbol(100, new Point(5, 5));
            mapa.Recursos.Add(recurso);

            aldeano.RecolectarEn(new Point(5, 5), mapa);

            Assert.GreaterOrEqual(almacen.Recursos["Madera"], (int)(recurso.Cantidad * recurso.TasaRecoleccion));
        }

        [Test]
        public void RecolectarEn_AlmacenYaTieneRecurso_Suma()
        {
            DepositoMadera almacen = new DepositoMadera(jugador) { Posicion = new Point(0, 0) };
            almacen.Recursos["Madera"] = 10;
            jugador.Edificios.Add(almacen);
            Arbol recurso = new Arbol(100, new Point(6, 6));
            mapa.Recursos.Add(recurso);

            aldeano.RecolectarEn(new Point(6, 6), mapa);

            Assert.Greater(almacen.Recursos["Madera"], 10);
        }
    }

    // Mocks simples para interfaces
    public class MockUnidad : IUnidad
    {
        public Jugador Propietario => null;
        public int Ataque => 0;
        public int Defensa => 0;
        public double Velocidad => 0;
        public int Salud { get; set; }
        public Point Posicion { get; set; }
        public int TiempoDeCreacion => 0;
        public bool Mover(Point destino, Mapa mapa) => true;
        public double CalcularDaño(IUnidad objetivo) => 0;
        public string AtacarEdificio(IEdificio objetivo) => "";
        public string AtacarUnidad(IUnidad objetivo) => "";
    }

    public class MockEdificio : IEdificio
    {
        public Jugador Propietario => null;
        public int Vida { get; set; }
        public Point Posicion { get; set; }
    }
}