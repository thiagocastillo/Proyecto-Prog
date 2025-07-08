using NUnit.Framework;
using System.Collections.Generic;

namespace Library.Domain.Tests;

public class JugadorTests
{
    private Jugador jugador;
    private Civilizacion civilizacion;

    [SetUp]
    public void Setup()
    {
        civilizacion = new Civilizacion("armenios", new List<string>(), "Arquero Compuesto");
        jugador = new Jugador("TestPlayer", civilizacion);
    }

    [Test]
    public void Jugador_InicializaConCentroCivicoY3Aldeanos()
    {
        Assert.That(jugador.Edificios.Count, Is.GreaterThanOrEqualTo(1));
        Assert.That(jugador.Edificios[0], Is.TypeOf<CentroCivico>());
        Assert.That(jugador.Aldeanos.Count, Is.EqualTo(3));
        Assert.That(jugador.Unidades.Count, Is.EqualTo(3));
    }

    [Test]
    public void AumentarPoblacionMaxima_ValidaIncremento()
    {
        jugador.AumentarPoblacionMaxima(10);
        Assert.That(jugador.PoblacionMaxima, Is.EqualTo(15));
    }

    [Test]
    public void AumentarPoblacionMaxima_ConValorNegativo_NoDebeModificar()
    {
        int poblacionAntes = jugador.PoblacionMaxima;
        jugador.AumentarPoblacionMaxima(-5);
        Assert.That(jugador.PoblacionMaxima, Is.EqualTo(poblacionAntes));
    }

    [Test]
    public void AgregarRecurso_CantidadInvalida_LanzaExcepcion()
    {
        Arbol recurso = new Arbol(100, new Point { X = 0, Y = 0});
        Assert.Throws<ArgumentException>(() => jugador.AgregarRecurso(recurso, 0));
    }

    [Test]
    public void PuedeCrearAldeano_DevuelveTrueSiTieneCentroCivicoYMenosDeLimite()
    {
        Assert.IsTrue(jugador.PuedeCrearAldeano());
    }

    [Test]
    public void DepositarRecursos_SumaCorrectamente()
    {
        var recursos = new Dictionary<string, int> { { "Madera", 30 }, { "Oro", 10 } };
        jugador.DepositarRecursos(recursos);
        Assert.That(jugador.Recursos["Madera"], Is.EqualTo(30));
        Assert.That(jugador.Recursos["Oro"], Is.EqualTo(10));
    }

    [Test]
    public void AgregarEdificio_AgregaCorrectamente()
    {
        var edificio = new Casa();
        jugador.AgregarEdificio(edificio);
        Assert.Contains(edificio, jugador.Edificios);
    }

    [Test]
    public void AgregarUnidad_Aldeano_AgregaCorrectamente()
    {
        var aldeano = new Aldeano(jugador);
        jugador.AgregarUnidad(aldeano);
        Assert.Contains(aldeano, jugador.Aldeanos);
        Assert.Contains(aldeano, jugador.Unidades);
    }

    [Test]
    public void AgregarUnidad_Militar_AgregaCorrectamente()
    {
        var unidad = new Infanteria(jugador);
        jugador.AgregarUnidad(unidad);
        Assert.Contains(unidad, jugador.Unidades);
    }

    [Test]
    public void AgregarUnidad_SuperaLimiteAldeanos_LanzaExcepcion()
    {
        for (int i = jugador.Aldeanos.Count; i < Jugador.LimiteAldeanos; i++)
            jugador.AgregarUnidad(new Aldeano(jugador));
        Assert.Throws<InvalidOperationException>(() => jugador.AgregarUnidad(new Aldeano(jugador)));
    }

    [Test]
    public void ObtenerResumenPoblacion_DevuelveFormatoCorrecto()
    {
        string resumen = jugador.ObtenerResumenPoblacion();
        Assert.That(resumen, Does.Contain($"{jugador.PoblacionActual}/{jugador.PoblacionMaxima}"));
    }

    [Test]
    public void PuedeCrearAldeano_SinCentroCivico_False()
    {
        jugador.Edificios.Clear();
        Assert.IsFalse(jugador.PuedeCrearAldeano());
    }
}



