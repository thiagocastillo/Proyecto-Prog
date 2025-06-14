using System.Collections.Generic;
using System.Text;
namespace Library;

public class Motor
{
    private readonly JuegoFachada _fachada = new JuegoFachada();

    public string ProcesarComando(string comando, List<string> argumentos)
    {
        try
        {
            switch (comando.ToLower())
            {
                case "ayuda":
                    return Ayuda.ObtenerComandos();
                
                case "crearpartida":
                    _fachada.CrearNuevaPartida();
                    return "Partida creada.";
                
                case "civilizaciones":
                    
                    var civs = _fachada.ObtenerCivilizacionesDisponibles();
                    return "Civilizaciones disponibles:\n" + string.Join("\n", civs);
                
                case "agregarjugador":
                    
                    if (argumentos.Count < 2)
                        return "Faltan argumentos en comando, recordar: agregarjugador <nombre> <civilización>";
                   
                    _fachada.AgregarJugadorAPartida(argumentos[0], argumentos[1].ToLower());
                    return "Jugador agregado.";
                
                case "construiredificio":
                    
                    if (argumentos.Count < 4)
                        return "Faltan argumentos en comando, recordar: construiredificio <nombre> <tipo> <x> <y>";
                        
                    
                    _fachada.ConstruirEdificio(argumentos[0], argumentos[1], new Point(int.Parse(argumentos[2]), int.Parse(argumentos[3])));
                    return "Edificio construido.";
                
                case "entrenarunidad":
                    
                    _fachada.EntrenarUnidad(argumentos[0], argumentos[1]);
                    return "Unidad entrenada.";
                
                case "moverunidad":
                   
                    _fachada.MoverUnidad(argumentos[0], int.Parse(argumentos[1]), new Point(int.Parse(argumentos[2]), int.Parse(argumentos[3])));
                    return "Unidad movida.";
                
                case "atacarunidad":
                    
                    return _fachada.AtacarUnidad(argumentos[0], int.Parse(argumentos[1]), int.Parse(argumentos[2]));
                
                case "recursosjugador":
                    
                    var recursos = _fachada.ObtenerRecursosJugador(argumentos[0]);
                    var sb = new StringBuilder();
                    foreach (var r in recursos)
                        sb.AppendLine($"{r.Key}: {r.Value}");
                    return sb.ToString().TrimEnd();
                
                case "unidadesjugador":
                    
                    var unidades = _fachada.ObtenerUnidadesJugador(argumentos[0]);
                    var sbU = new StringBuilder();
                    
                    for (int i = 0; i < unidades.Count; i++)
                        sbU.AppendLine($"{i}: {unidades[i].GetType().Name}");
                    
                    return sbU.ToString().TrimEnd();
                
                case "edificiosjugador":
                    
                    var edificios = _fachada.ObtenerEdificiosJugador(argumentos[0]);
                    var sbE = new StringBuilder();
                    
                    for (int i = 0; i < edificios.Count; i++)
                        sbE.AppendLine($"{i}: {edificios[i].GetType().Name}");
                    return sbE.ToString().TrimEnd();
                
                case "listarjugadores":
                    
                    var jugadores = _fachada.ObtenerJugadores();
                    var sbJ = new StringBuilder();
                    
                    if(jugadores == null || jugadores.Count == 0)
                        return "No hay jugadores en la partida.";
                    
                    foreach (var jugador in jugadores)
                        sbJ.AppendLine($"Nombre: {jugador.Nombre}, Civilización: {jugador.Civilizacion?.Nombre ?? "Sin civilización"}");
                    
                    return sbJ.ToString().TrimEnd();
                
                case "mostrarmapa":
                   
                    return _fachada.MostrarMapa();               
                
                case "salir":
                    
                    return "Saliendo...";
                
                default:
                    
                    return "Comando no reconocido.";
            }
        }
        catch (System.Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}