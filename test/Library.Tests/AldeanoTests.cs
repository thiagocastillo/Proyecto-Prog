namespace Library.Tests;

public class AldeanoTests
{
    private Jugador jugador;
    private Mapa mapa;

    [SetUp]
        public void Setup()
        {
            Civilizacion civilizacion = new Civilizacion("Aztecas", new List<string>(), "Guerrero Jaguar");
            jugador = new Jugador("Juan", civilizacion);
            mapa = new Mapa();
        }

        [Test]
        public void Mover_PosicionValida_ActualizaPosicion()
        {
            Aldeano aldeano = new Aldeano(jugador);
            Point destino = new Point(10, 10);
            
            bool resultado = aldeano.Mover(destino, mapa);

            Assert.IsTrue(resultado);
            Assert.That(aldeano.Posicion.X, Is.EqualTo(10));
            Assert.That(aldeano.Posicion.Y, Is.EqualTo(10));
        }

        [Test]
        public void Mover_PosicionInvalidaDevuelveFalse()
        {
            Aldeano aldeano = new Aldeano(jugador);
            Point destino = new Point(-1, 200);

            bool resultado = aldeano.Mover(destino, mapa);

            Assert.IsFalse(resultado);
        }

        [Test]
        public void AtacarUnidad_DevuelveMensajeNoAtaca()
        {
            Aldeano aldeano = new Aldeano(jugador);
            Jugador otroJugador = new Jugador("Otro", jugador.Civilizacion);
            Infanteria objetivo = new Infanteria(otroJugador);

            string resultado = aldeano.AtacarUnidad(objetivo);

            Assert.That(resultado, Is.EqualTo("Los aldeanos no atacan unidades."));
        }

        [Test]
        public void AtacarEdificio_DevuelveMensajeNoAtaca()
        {
            Aldeano aldeano = new Aldeano(jugador);
            Casa edificio = new Casa(jugador);

            string resultado = aldeano.AtacarEdificio(edificio);

            Assert.That(resultado, Is.EqualTo("Los aldeanos no atacan edificios."));
        }

        [Test]
        public void Recolectar_RecursoValido_AlmacenaEnJugador()
        {
            Aldeano aldeano = new Aldeano(jugador) { Posicion = new Point(5, 5) };
            Arbol recurso = new Arbol(100, new Point { X = 5, Y = 5});
            DepositoMadera almacen = new DepositoMadera(jugador) { Posicion = new Point(6, 5) };

            jugador.AgregarEdificio(almacen);

            int antes = jugador.Recursos["Madera"];

            aldeano.Recolectar(recurso);

            int despues = jugador.Recursos["Madera"];
            Assert.That(despues, Is.GreaterThan(antes));
        }

        [Test]
        public void Recolectar_RecursoAgotado_LanzaExcepcion()
        {
            Aldeano aldeano = new Aldeano(jugador);
            Arbol recurso = new Arbol(100, new Point { X = 0, Y = 0});
            recurso.Recolectar(200); // Agotar recurso

            Assert.Throws<InvalidOperationException>(() => aldeano.Recolectar(recurso));
        }

        [Test]
        public void Recolectar_SinAlmacenCompatible_LanzaExcepcion()
        {
            Aldeano aldeano = new Aldeano(jugador) { Posicion = new Point(0, 0) };
            Arbol recurso = new Arbol(100, new Point { X = 0, Y = 0});

            // No hay depósito agregado
            Assert.Throws<InvalidOperationException>(() => aldeano.Recolectar(recurso));
        }
}