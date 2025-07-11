using NUnit.Framework;
using System;
using System.Threading;
using Library;
namespace Library.Domain.Tests;

[TestFixture]
public class TiempoConstruccionTests
{
    [Test]
    public void Constructor_AlInicializar_TiempoTotalEsCorrecto()
    {
        int tiempo = 5;
        TiempoDeGeneracion tiempoConstruccion = new TiempoDeGeneracion(tiempo);

        Assert.That(tiempoConstruccion.TiempoTotalSegundos, Is.EqualTo(tiempo));
    }

    [Test]
    public void Fin_CalculadoCorrectamente()
    {
        int tiempo = 3;
        TiempoDeGeneracion tiempoConstruccion = new TiempoDeGeneracion(tiempo);

        DateTime esperado = tiempoConstruccion.Inicio.AddSeconds(tiempo);

        Assert.That(tiempoConstruccion.Fin, Is.EqualTo(esperado));
    }

    [Test]
    public void TiempoRestante_DisminuyeConElTiempo()
    {
        TiempoDeGeneracion tiempoConstruccion = new TiempoDeGeneracion(3);

        int restanteInicial = tiempoConstruccion.TiempoRestanteSegundos;

        Thread.Sleep(1500); // Esperar 1.5 segundos

        int restanteFinal = tiempoConstruccion.TiempoRestanteSegundos;

        Assert.That(restanteFinal, Is.LessThan(restanteInicial));
    }

    [Test]
    public void EstaCompleta_DevuelveTrue_DespuesDeTiempo()
    {
        TiempoDeGeneracion tiempoConstruccion = new TiempoDeGeneracion(1); // 1 segundo

        Thread.Sleep(1500); // Esperar 1.5 segundos

        Assert.That(tiempoConstruccion.EstaCompleta, Is.True);
    }

    [Test]
    public void EstaCompleta_DevuelveFalse_SiAunNoPasoElTiempo()
    {
        TiempoDeGeneracion tiempoConstruccion = new TiempoDeGeneracion(2); // 2 segundos

        Thread.Sleep(500); // Esperar medio segundo

        Assert.That(tiempoConstruccion.EstaCompleta, Is.False);
    }

    [Test]
    public void TiempoRestante_CuandoCompletado_EsCero()
    {
        TiempoDeGeneracion tiempoConstruccion = new TiempoDeGeneracion(1); // 1 segundo

        Thread.Sleep(1100); // Esperar un poco más de 1 segundo

        Assert.That(tiempoConstruccion.TiempoRestanteSegundos, Is.EqualTo(0));
    }
}

