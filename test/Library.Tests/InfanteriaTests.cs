namespace Library.Tests;

public class InfanteriaTests
{
    private Jugador jugadorAzteca;
    private Jugador jugadorRival;
    private Infanteria infanteria;

        [SetUp]
        public void SetUp()
        {
            Civilizacion civilizacionAzteca = new Civilizacion("aztecas", new List<string>(), "Guerrero Jaguar");
            jugadorAzteca = new Jugador("Azteca", civilizacionAzteca);

            Civilizacion civilizacionRival = new Civilizacion("armenios", new List<string>(), "Arquero Compuesto");
            jugadorRival = new Jugador("Rival", civilizacionRival);

            infanteria = new Infanteria(jugadorAzteca) { Posicion = new Point(0, 0), Salud = 100 };
        }

        [Test]
        public void Mover_DestinoValido_MueveCorrectamente()
        {
            Mapa mapa = new Mapa();
            Point destino = new Point(10, 10);

            bool resultado = infanteria.Mover(destino, mapa);

            Assert.IsTrue(resultado);
            Assert.That(infanteria.Posicion.X, Is.EqualTo(10));
            Assert.That(infanteria.Posicion.Y, Is.EqualTo(10));
        }

        [Test]
        public void Mover_DestinoFueraDeMapa_LanzaExcepcion()
        {
            Mapa mapa = new Mapa();
            Point destino = new Point(-1, 200);

            InvalidOperationException  ex = Assert.Throws<InvalidOperationException>(() => infanteria.Mover(destino, mapa));
            Assert.That(ex.Message, Does.Contain("No se pudo mover la unidad"));
        }

        [Test]
        public void AtacarU_ContraCaballeria_AplicaBonus()
        {
            Caballeria caballeria = new Caballeria(jugadorRival) { Salud = 100 };
            string resultado = infanteria.AtacarUnidad(caballeria);

            Assert.That(caballeria.Salud, Is.LessThan(100));
            Assert.IsTrue(resultado.Contains("Infanteria atacó a Caballeria"));
        }

        [Test]
        public void AtacarU_ContraInfanteriaAztecaGuerreroJaguar_AplicaBonus()
        {
            Infanteria infanteriaObjetivo = new Infanteria(jugadorRival) { Salud = 100 };

            string resultado = infanteria.AtacarUnidad(infanteriaObjetivo);

            Assert.That(infanteriaObjetivo.Salud, Is.LessThan(100));
            Assert.IsTrue(resultado.Contains("Infanteria atacó"));
        }

        [Test]
        public void AtacarE_ContraEdificio_CausaDaño()
        {
            Casa casa = new Casa(jugadorRival) { Vida = 1000 };

            string resultado = infanteria.AtacarEdificio(casa);

            Assert.That(casa.Vida, Is.LessThan(1000));
            Assert.IsTrue(resultado.Contains("atacó el edificio"));
        }

        [Test]
        public void AtacarU_UnidadEliminada_SeEliminaDeLista()
        {
            Arquero objetivo = new Arquero(jugadorRival) { Salud = 1 }; // Aseguramos su destrucción
            jugadorRival.Unidades.Add(objetivo);

            string resultado = infanteria.AtacarUnidad(objetivo);

            Assert.IsFalse(jugadorRival.Unidades.Contains(objetivo));
            Assert.IsTrue(resultado.Contains("fue destruido"));
        }
}