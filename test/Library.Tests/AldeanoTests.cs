namespace Library.Tests;

public class AldeanoTests
{
    private Jugador jugador;
    private Mapa mapa;

    [SetUp]
        public void Setup()
        {
            var civilizacion = new Civilizacion("Aztecas", new List<string>(), "Guerrero Jaguar");
            jugador = new Jugador("Juan", civilizacion);
            mapa = new Mapa();
        }

        [Test]
        public void Mover_PosicionValida_ActualizaPosicion()
        {
            var aldeano = new Aldeano(jugador);
            var destino = new Point(10, 10);
            
            bool resultado = aldeano.Mover(destino, mapa);

            Assert.IsTrue(resultado);
            Assert.That(aldeano.Posicion.X, Is.EqualTo(10));
            Assert.That(aldeano.Posicion.Y, Is.EqualTo(10));
        }

        [Test]
        public void Mover_PosicionInvalidaDevuelveFalse()
        {
            var aldeano = new Aldeano(jugador);
            var destino = new Point(-1, 200);

            bool resultado = aldeano.Mover(destino, mapa);

            Assert.IsFalse(resultado);
        }

        [Test]
        public void AtacarU_DevuelveMensajeNoAtaca()
        {
            var aldeano = new Aldeano(jugador);
            var otroJugador = new Jugador("Otro", jugador.Civilizacion);
            var objetivo = new Infanteria(otroJugador);

            string resultado = aldeano.AtacarUnidad(objetivo);

            Assert.That(resultado, Is.EqualTo("Los aldeanos no atacan unidades."));
        }

        [Test]
        public void AtacarE_DevuelveMensajeNoAtaca()
        {
            var aldeano = new Aldeano(jugador);
            var edificio = new Casa(jugador);

            string resultado = aldeano.AtacarEdificio(edificio);

            Assert.That(resultado, Is.EqualTo("Los aldeanos no atacan edificios."));
        }

        [Test]
        public void Recolectar_RecursoValido_AlmacenaEnJugador()
        {
            var aldeano = new Aldeano(jugador) { Posicion = new Point(5, 5) };
            var recurso = new Madera();
            var almacen = new DepositoMadera(jugador) { Posicion = new Point(6, 5) };

            jugador.AgregarEdificio(almacen);

            int antes = jugador.Recursos["Madera"];

            aldeano.Recolectar(recurso);

            int despues = jugador.Recursos["Madera"];
            Assert.That(despues, Is.GreaterThan(antes));
        }

        [Test]
        public void Recolectar_RecursoAgotado_LanzaExcepcion()
        {
            var aldeano = new Aldeano(jugador);
            var recurso = new Madera();
            recurso.Recolectar(200); // Agotar recurso

            Assert.Throws<InvalidOperationException>(() => aldeano.Recolectar(recurso));
        }

        [Test]
        public void Recolectar_SinAlmacenCompatible_LanzaExcepcion()
        {
            var aldeano = new Aldeano(jugador) { Posicion = new Point(0, 0) };
            var recurso = new Madera();

            // No hay depósito agregado
            Assert.Throws<InvalidOperationException>(() => aldeano.Recolectar(recurso));
        }
}