﻿namespace Library.Domain.Tests;

public class DepositoMaderaTests
{
    private Jugador jugador;

    [SetUp]
    public void SetUp()
    {
        Civilizacion civilizacion = new Civilizacion("Chinos", new List<string>(), "Caballeria");
        jugador = new Jugador("Gaston", civilizacion);
    }

    [Test]
    public void Constructores_ValoresIniciales()
    {
        DepositoMadera depositoMadera = new DepositoMadera(jugador);
        
        Assert.AreEqual(jugador, depositoMadera.Propietario);
        Assert.AreEqual(5000, depositoMadera.Vida);
        Assert.AreEqual(500, depositoMadera.CapacidadMaxima);
    }

    [Test]
    public void Eficiciencia_CortaDistancia()
    {
        DepositoMadera depositoMadera = new DepositoMadera(jugador);
        double eficiencia = depositoMadera.Eficiencia(1);
        Assert.AreEqual(1.0, eficiencia); 
    }

    [Test]
    public void Eficiencia_LargaDistancia()
    {
        DepositoMadera depositoMadera = new DepositoMadera(jugador);
        double eficiencia = depositoMadera.Eficiencia(10);
        Assert.AreEqual(0.1, eficiencia); 
    }

    [Test]
    public void Eficiencia_DistanciaIntermedia()
    {
        DepositoMadera depositoMadera = new DepositoMadera(jugador);
        double eficiencia = depositoMadera.Eficiencia(5);
        Assert.AreEqual(0.5, eficiencia); 
    }
}