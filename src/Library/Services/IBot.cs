using System;
using System.Threading.Tasks;

namespace Library.Services;

/// <summary>
/// La interfaz del Bot de Discord para usar con inyección de dependencias.
/// </summary>
public interface IBot
{
    Task StartAsync(IServiceProvider services);
    Task StopAsync();
}