namespace Library.Tests;

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
            caballeria = new Caballeria(jugador);
            mapa = new Mapa();
        }

        [Test]
        public void Mover_EnRango_DeberiaActualizarPosicion()
        {
            Point destino = new Point(10, 10);
            bool resultado = caballeria.Mover(destino, mapa);

            Assert.IsTrue(resultado);
            Assert.That(caballeria.Posicion.X, Is.EqualTo(10));
            Assert.That(caballeria.Posicion.Y, Is.EqualTo(10));
        }

        [Test]
        public void Mover_FueraDeRango_DeberiaFallar()
        {
            Point destino = new Point(-1, 101);
            bool resultado = caballeria.Mover(destino, mapa);

            Assert.IsFalse(resultado);
        }

        [Test]
        public void CalcularDaño_ContraUnidadArquero_DeberiaAplicarBonus()
        {
            Arquero arquero = new Arquero(jugadorRival);
            arquero.Defensa = 2;
            double daño = caballeria.CalcularDaño(arquero);

            Assert.That(daño, Is.GreaterThan(caballeria.Ataque - arquero.Defensa)); // Por el +2 contra arqueros
        }

        [Test]
        public void AtacarU_ConEnemigo_DeberiaReducirSaludYRetornarInfo()
        {
            Arquero arquero = new Arquero(jugadorRival) { Salud = 100 };
            string resultado = caballeria.AtacarUnidad(arquero);

            Assert.That(arquero.Salud, Is.LessThan(100));
            Assert.IsTrue(resultado.Contains("Caballeria atacó"));
        }

        [Test]
        public void AtacarU_EnemigoMuere_DeberiaEliminarUnidad()
        {
            Arquero unidad = new Arquero(jugadorRival) { Salud = 5 }; // daño mínimo = 5
            jugadorRival.Unidades.Add(unidad);

            string resultado = caballeria.AtacarUnidad(unidad);

            Assert.That(jugadorRival.Unidades.Contains(unidad), Is.False);
            Assert.IsTrue(resultado.Contains("fue destruido"));
        }

        [Test]
        public void AtacarE_EdificioRecibeDañoYPosibleDestruccion()
        {
            Cuartel edificio = new Cuartel(jugadorRival) { Vida = 10 };
            jugadorRival.Edificios.Add(edificio);

            string resultado = caballeria.AtacarEdificio(edificio);

            Assert.That(edificio.Vida, Is.LessThan(10));
            Assert.IsTrue(resultado.Contains("causando"));
        }
}