namespace Library.Tests;

public class ArqueroTests
{
    private Jugador jugadorArmenio;
    private Jugador jugadorEnemigo;
    private Arquero arquero;

        [SetUp]
        public void Setup()
        {
            var civilizacionArmenia = new Civilizacion("armenios", new List<string>(), "Arquero Compuesto");
            jugadorArmenio = new Jugador("Jugador1", civilizacionArmenia);

            var civilizacionEnemiga = new Civilizacion("aztecas", new List<string>(), "Guerrero Jaguar");
            jugadorEnemigo = new Jugador("Jugador2", civilizacionEnemiga);

            arquero = new Arquero(jugadorArmenio) { Salud = 100, Posicion = new Point(1, 1) };
        }

      /*  [Test]
        public void Arquero_Constructor_AplicaBonusCivilizacion()
        {
            Assert.That(arquero.Ataque, Is.EqualTo(10)); // 8 base + 2 bonus por ser Armenios con Arquero Compuesto
        }*/

        [Test]
        public void Arquero_Mover_PosicionValida_MueveCorrectamente()
        {
            var mapa = new Mapa();
            var destino = new Point(5, 5);

            bool resultado = arquero.Mover(destino, mapa);

            Assert.IsTrue(resultado);
            Assert.That(arquero.Posicion.X, Is.EqualTo(5));
            Assert.That(arquero.Posicion.Y, Is.EqualTo(5));
        }

        [Test]
        public void Arquero_AtacarU_ContraInfanteria_InfligeDañoConBonus()
        {
            var infanteria = new Infanteria(jugadorEnemigo) { Salud = 100 };
            string resultado = arquero.AtacarUnidad(infanteria);

            Assert.IsTrue(resultado.Contains("hizo"));
            Assert.That(infanteria.Salud, Is.LessThan(100));
        }

        [Test]
        public void Arquero_AtacarU_ContraUnidadConDefensaAlta_NoDañoNegativo()
        {
            var unidadDefensiva = new Caballeria(jugadorEnemigo)
            {
                Salud = 100,
                Posicion = new Point(2, 2)
            };

            string resultado = arquero.AtacarUnidad(unidadDefensiva);

            Assert.That(unidadDefensiva.Salud, Is.LessThanOrEqualTo(100));
            Assert.IsTrue(resultado.Contains("daño"));
        }

        [Test]
        public void Arquero_AtacarE_EdificioRecibeDaño()
        {
            var edificio = new Casa(jugadorEnemigo) { Vida = 1000 };

            string resultado = arquero.AtacarEdificio(edificio);

            Assert.IsTrue(resultado.Contains("causando"));
            Assert.That(edificio.Vida, Is.LessThan(1000));
        }
}