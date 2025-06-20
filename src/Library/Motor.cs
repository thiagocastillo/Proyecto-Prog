using System.Collections.Generic;
using System.Text;
namespace Library;

public class Motor
{
    private readonly JuegoFachada _fachada = new JuegoFachada();

    public string ProcesarComando(string comando, List<string> argumentos)
    {
        try    //ver esto: Proporcionar ayuda y ejemplos de comandos: a q se refieren con ejemplos de comandos?
        {
            switch (comando.ToLower())
            {
                case "ayuda":
                    return Ayuda.ObtenerComandos();
                
                case "crearpartida":
                    _fachada.CrearNuevaPartida();
                    return "Partida creada.";
                
                case "civilizaciones":
                    
                    List<string> civs = _fachada.ObtenerCivilizacionesDisponibles();
                    return "Civilizaciones disponibles:\n" + string.Join("\n", civs);
                
                case "agregarjugador":
                    
                    if (argumentos.Count < 2)
                        return "Faltan argumentos en comando, recordar sintaxis: agregarjugador <nombre> <civilización>";
                   
                    _fachada.AgregarJugadorAPartida(argumentos[0], argumentos[1].ToLower());
                    return "Jugador agregado.";
                
                case "construiredificio":
                    
                    if (argumentos.Count < 4)
                        return "Faltan argumentos en comando, recordar sintaxis: construiredificio <nombre> <tipo> <x> <y>";
                        
                    
                    _fachada.ConstruirEdificio(argumentos[0], argumentos[1], new Point(int.Parse(argumentos[2]), int.Parse(argumentos[3])));
                    return "Edificio construido.";
                
                case "entrenarunidad":
                    
                    if (argumentos.Count < 2)
                        return "Faltan argumentos en comando, recordar sintaxis: entrenarunidad <nombre> <tipo>";
                    _fachada.EntrenarUnidad(argumentos[0], argumentos[1]);
                    return "Unidad entrenada.";
                
                case "recolectar":
                    if (argumentos.Count < 4)
                        return "Faltan argumentos: recolectar <nombreJugador> <idAldeano> <x> <y>";
                    _fachada.OrdenarRecolectar(argumentos[0], int.Parse(argumentos[1]), int.Parse(argumentos[2]), int.Parse(argumentos[3]));
                    return "Orden de recolección enviada.";case "moverunidad":
                    
                    if (argumentos.Count < 4)
                        return "Faltan argumentos en comando, recordar sintaxis: moverunidad <nombre> <idUnidad> <x> <y>";
                   
                    _fachada.MoverUnidad(argumentos[0], int.Parse(argumentos[1]), new Point(int.Parse(argumentos[2]), int.Parse(argumentos[3])));
                    return "Unidad movida.";
                
                case "atacarunidad":               //ver...
                    
                    if (argumentos.Count < 3)
                        return "Faltan argumentos en comando, recordar sintaxis: atacarunidad <nombre> <idAtacante> <idObjetivo>";
                    
                    return _fachada.AtacarUnidad(argumentos[0], int.Parse(argumentos[1]), int.Parse(argumentos[2]));
                
                case "atacarudificio":               //ver...
                    
                    if (argumentos.Count < 3)
                        return "Faltan argumentos en comando, recordar sintaxis: atacaredificio <nombre> <idAtacante> <idObjetivo>";
                    
                    return _fachada.AtacarEdificio(argumentos[0], int.Parse(argumentos[1]), int.Parse(argumentos[2]));

                
                case "recursosjugador":   
                    if (argumentos.Count < 1)
                        return "Faltan argumentos en comando, recordar sintaxis: recursosjugador <nombre>";

                    Dictionary<string, int> recursos = _fachada.ObtenerRecursosJugador(argumentos[0]);

                    if (recursos.Count == 0)
                        return "El jugador no existe, cree uno usando el comando correspondiente.";

                    StringBuilder sb = new StringBuilder();
                    foreach (KeyValuePair<string, int> r in recursos)
                        sb.AppendLine($"{r.Key}: {r.Value}");
                    return sb.ToString().TrimEnd();
                
                case "unidadesjugador":   
                    
                    if (argumentos.Count < 1)
                        return "Faltan argumentos en comando, recordar sintaxis: unidadesjugador <nombre>";
                    
                    List<IUnidad> unidades = _fachada.ObtenerUnidadesJugador(argumentos[0]);
                    StringBuilder sbU = new StringBuilder();
                    
                    for (int i = 0; i < unidades.Count; i++)
                        sbU.AppendLine($"{i}: {unidades[i].GetType().Name}");
                    
                    return sbU.ToString().TrimEnd();
                
                case "edificiosjugador":  
                    
                    if (argumentos.Count < 1)
                        return "Faltan argumentos en comando, recordar sintaxis: edificiosjugador <nombre>";
                    
                    List<IEdificio> edificios = _fachada.ObtenerEdificiosJugador(argumentos[0]);
                    StringBuilder sbE = new StringBuilder();
                    
                    for (int i = 0; i < edificios.Count; i++)
                        sbE.AppendLine($"{i}: {edificios[i].GetType().Name}");
                    return sbE.ToString().TrimEnd();
                case "listarrecursos":
                    return _fachada.ListarRecursos();
                
                case "listarjugadores":
                    
                    List<Jugador> jugadores = _fachada.ObtenerJugadores();
                    StringBuilder sbJ = new StringBuilder();
                    
                    if(jugadores == null || jugadores.Count == 0)
                        return "No hay jugadores en la partida.";
                    
                    foreach (Jugador jugador in jugadores)
                        sbJ.AppendLine($"Nombre: {jugador.Nombre}, Civilización: {jugador.Civilizacion?.Nombre ?? "Sin civilización"}");
                    
                    return sbJ.ToString().TrimEnd();
                
                case "mostrarmapa":
                   
                    return _fachada.MostrarMapa();               
                
                case "salir":
                    
                    return "Saliendo...";
                
                case "exit":  // ST se puso ladilla y necesita un exit para salir del programa jajajaajajaj
                    
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


// falta recolectar recursos, ver si se puede hacer con el comando recolectar recurso <nombre> <tipo> <x> <y> o algo así