using NUnit.Framework;
using System.Collections.Generic;

namespace Library.Domain.Tests
{
    [TestFixture]
    public class MotorTests
    {
        [Test]
        public void ProcesarComando_CrearPartida_DevuelveMensajeCorrecto()
        {
            var motor = new Motor();
            var resultado = motor.ProcesarComando("crearpartida", new List<string>());
            Assert.AreEqual("Partida creada.", resultado);
        }

        [Test]
        public void ProcesarComando_ComandoNoReconocido_DevuelveMensaje()
        {
            var motor = new Motor();
            var resultado = motor.ProcesarComando("comandox", new List<string>());
            Assert.AreEqual("Comando no reconocido.", resultado);
        }

        [Test]
        public void ProcesarComando_Ayuda_DevuelveComandos()
        {
            var motor = new Motor();
            var resultado = motor.ProcesarComando("ayuda", new List<string>());
            Assert.IsNotNull(resultado);
            Assert.IsTrue(resultado.ToLower().Contains("comando"));
        }

        [Test]
        public void ProcesarComando_AgregarJugador_FaltanArgumentos()
        {
            var motor = new Motor();
            var resultado = motor.ProcesarComando("agregarjugador", new List<string> { "Juan" });
            Assert.IsTrue(resultado.Contains("Faltan argumentos"));
        }

        [Test]
        public void ProcesarComando_Salir_DevuelveMensaje()
        {
            var motor = new Motor();
            var resultado = motor.ProcesarComando("salir", new List<string>());
            Assert.AreEqual("Saliendo...", resultado);
        }
    }
}