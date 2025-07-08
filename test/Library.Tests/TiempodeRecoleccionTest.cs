using NUnit.Framework;
using System;
using System.Threading;
using Library.Domain;

namespace Library.Domain.Tests
{
    public class TiempoDeRecoleccionTests
    {
        [Test]
        public void Constructor_AsignaTiempoYInicio()
        {
            int segundos = 5;
            var tiempo = new TiempoDeRecoleccion(segundos);

            Assert.AreEqual(segundos, tiempo.TiempoTotalSegundos);
            Assert.That(tiempo.Inicio, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(1)));
        }

        [Test]
        public void Fin_CalculaCorrectamente()
        {
            int segundos = 10;
            var tiempo = new TiempoDeRecoleccion(segundos);

            Assert.That(tiempo.Fin, Is.EqualTo(tiempo.Inicio.AddSeconds(segundos)));
        }

        [Test]
        public void TiempoRestanteSegundos_DecreceYLLegaACero()
        {
            var tiempo = new TiempoDeRecoleccion(1);
            Assert.That(tiempo.TiempoRestanteSegundos, Is.GreaterThanOrEqualTo(0));
            Thread.Sleep(1200);
            Assert.AreEqual(0, tiempo.TiempoRestanteSegundos);
        }

        [Test]
        public void EstaCompleta_TrueDespuesDeTiempo()
        {
            var tiempo = new TiempoDeRecoleccion(1);
            Assert.IsFalse(tiempo.EstaCompleta);
            Thread.Sleep(1200);
            Assert.IsTrue(tiempo.EstaCompleta);
        }
    }
}