using NUnit.Framework;

namespace Library.Domain.Tests
{
    public class AyudaTest
    {
        [Test]
        public void ObtenerComandos_DeberiaContenerComandoAyuda()
        {
            string comandos = Ayuda.ObtenerComandos();
            Assert.IsFalse(string.IsNullOrWhiteSpace(comandos));
            Assert.That(comandos, Does.Contain("Ayuda"));
            Assert.That(comandos, Does.Contain("CrearPartida"));
        }

        [Test]
        public void AyudaEdificios_DeberiaContenerCasaYCuartel()
        {
            string edificios = Ayuda.AyudaEdificios();
            Assert.IsFalse(string.IsNullOrWhiteSpace(edificios));
            Assert.That(edificios, Does.Contain("Casa"));
            Assert.That(edificios, Does.Contain("Cuartel"));
        }

        [Test]
        public void AyudaUnidades_DeberiaContenerAldeanoYArqueroCompuesto()
        {
            string unidades = Ayuda.AyudaUnidades();
            Assert.IsFalse(string.IsNullOrWhiteSpace(unidades));
            Assert.That(unidades, Does.Contain("Aldeano"));
            Assert.That(unidades, Does.Contain("ArqueroCompuesto"));
        }
    }
}