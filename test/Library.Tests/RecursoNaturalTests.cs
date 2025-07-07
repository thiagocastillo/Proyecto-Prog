using NUnit.Framework;
using System;

namespace Library.Domain.Tests
{
    // Clase derivada para testear la abstracción
    public class RecursoNaturalDummy : RecursoNatural
    {
        public RecursoNaturalDummy(string nombre, int vidaBase, double tasaRecoleccion, Point ubicacion)
            : base(nombre, vidaBase, tasaRecoleccion, ubicacion) { }
    }

    [TestFixture]
    public class RecursoNaturalTests
    {
        [Test]
        public void Constructor_ValoresInicialesCorrectos()
        {
            RecursoNatural recurso = new RecursoNaturalDummy("Madera", 100, 1.5, new Point(2, 3));
            Assert.AreEqual("Madera", recurso.Nombre);
            Assert.AreEqual(100, recurso.VidaBase);
            Assert.AreEqual(1.5, recurso.TasaRecoleccion);
            Assert.AreEqual(100, recurso.Cantidad); // Cantidad inicial igual a vidaBase según tu implementación
            Assert.AreEqual(2, recurso.Ubicacion.X);
            Assert.AreEqual(3, recurso.Ubicacion.Y);
            Assert.IsFalse(recurso.EstaAgotado);
        }

        [Test]
        public void Recolectar_ExtraeCantidadCorrecta()
        {
            RecursoNatural recurso = new RecursoNaturalDummy("Oro", 50, 2, new Point(1, 1));
            int extraido = recurso.Recolectar();
            Assert.AreEqual(50, extraido); // vidaBase * tasaRecoleccion = 100, pero solo hay 50, así que extrae 50
            Assert.AreEqual(0, recurso.Cantidad);
            Assert.IsTrue(recurso.EstaAgotado);
        }

        [Test]
        public void Recolectar_AgoteRecurso_EstaAgotadoTrue()
        {
            RecursoNatural recurso = new RecursoNaturalDummy("Piedra", 10, 1, new Point(0, 0));
            int extraido = recurso.Recolectar();
            Assert.AreEqual(10, extraido);
            Assert.AreEqual(0, recurso.Cantidad);
            Assert.IsTrue(recurso.EstaAgotado);
        }

        [Test]
        public void Recolectar_RecursoAgotado_LanzaExcepcion()
        {
            RecursoNatural recurso = new RecursoNaturalDummy("Madera", 5, 1, new Point(0, 0));
            recurso.Recolectar(); // Agota el recurso
            Assert.Throws<InvalidOperationException>(() => recurso.Recolectar());
        }
    }
}