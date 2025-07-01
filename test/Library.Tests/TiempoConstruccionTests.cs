using NUnit.Framework;
using System;
using System.Threading;
using Library;
namespace Library.Tests;

[TestFixture]
public class TiempoConstruccionTests
{
    [Test]
    public void Constructor_AlInicializar_TiempoTotalEsCorrecto()
    {
        int tiempo = 5;
        var tiempoConstruccion = new TiempoConstruccion(tiempo);

        Assert.That(tiempoConstruccion.TiempoTotalSegundos, Is.EqualTo(tiempo));
    }

    [Test]
    public void Fin_CalculadoCorrectamente()
    {
        int tiempo = 3;
        var tiempoConstruccion = new TiempoConstruccion(tiempo);

        var esperado = tiempoConstruccion.Inicio.AddSeconds(tiempo);

        Assert.That(tiempoConstruccion.Fin, Is.EqualTo(esperado));
    }

    [Test]
    public void TiempoRestante_DisminuyeConElTiempo()
    {
        var tiempoConstruccion = new TiempoConstruccion(3);

        int restanteInicial = tiempoConstruccion.TiempoRestanteSegundos;

        Thread.Sleep(1500); // Esperar 1.5 segundos

        int restanteFinal = tiempoConstruccion.TiempoRestanteSegundos;

        Assert.That(restanteFinal, Is.LessThan(restanteInicial));
    }

    [Test]
    public void EstaCompleta_DevuelveTrue_DespuesDeTiempo()
    {
        var tiempoConstruccion = new TiempoConstruccion(1); // 1 segundo

        Thread.Sleep(1500); // Esperar 1.5 segundos

        Assert.That(tiempoConstruccion.EstaCompleta, Is.True);
    }

    [Test]
    public void EstaCompleta_DevuelveFalse_SiAunNoPasoElTiempo()
    {
        var tiempoConstruccion = new TiempoConstruccion(2); // 2 segundos

        Thread.Sleep(500); // Esperar medio segundo

        Assert.That(tiempoConstruccion.EstaCompleta, Is.False);
    }

    [Test]
    public void TiempoRestante_CuandoCompletado_EsCero()
    {
        var tiempoConstruccion = new TiempoConstruccion(1); // 1 segundo

        Thread.Sleep(1100); // Esperar un poco más de 1 segundo

        Assert.That(tiempoConstruccion.TiempoRestanteSegundos, Is.EqualTo(0));
    }
}

