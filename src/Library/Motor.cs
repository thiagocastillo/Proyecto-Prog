using System.Collections.Generic;
using System.Text;
namespace Library;

// Clase principal que procesa los comandos del usuario y coordina la lógica del juego
public class Motor
{
    // Fachada que expone las operaciones de alto nivel del juego
    private readonly JuegoFachada _fachada = new JuegoFachada();
    // Indica si la partida ha finalizado
    private bool partidaFinalizada = false;

    // Procesa un comando recibido junto con sus argumentos y devuelve el resultado como string
    public string ProcesarComando(string comando, List<string> argumentos)
    {
        if (partidaFinalizada)
        {
            return "La partida ha finalizado. No se pueden procesar más comandos.";
        }
        
        try
        {
            switch (comando.ToLower())
            {
                // Muestra la lista de comandos disponibles
                case "ayuda":
                    
                    return Ayuda.ObtenerComandos();

                // Crea una nueva partida
                case "crearpartida":
                    
                    _fachada.CrearNuevaPartida();
                    return "Partida creada.";

                // Lista las civilizaciones disponibles
                case "civilizaciones":
                    
                    List<string> civs = _fachada.ObtenerCivilizacionesDisponibles();
                    return "Civilizaciones disponibles:\n" + string.Join("\n", civs);

                // Agrega un jugador a la partida con la civilización elegida
                case "agregarjugador":
                    
                    if (argumentos.Count < 2)
                        return "Faltan argumentos en comando, recordar sintaxis: agregarjugador <nombreJugador> <civilización>";
                    
                    _fachada.AgregarJugadorAPartida(argumentos[0], argumentos[1].ToLower());
                    return $"Jugador '{argumentos[0]}' agregado con civilización '{argumentos[1]}'.";

                // Construye un edificio en la posición indicada para el jugador
                case "construiredificio":
                    
                    if (argumentos.Count < 4)
                        return "Faltan argumentos en comando, recordar sintaxis: construiredificio <nombreJugador> <tipo> <x> <y>";
                   
                    _fachada.ConstruirEdificio(argumentos[0], argumentos[1], new Point(int.Parse(argumentos[2]), int.Parse(argumentos[3])));
                    return $"Edificio '{argumentos[1]}' construido en ({argumentos[2]}, {argumentos[3]}) para el jugador '{argumentos[0]}'.";

                // Entrena una unidad en la posición indicada para el jugador
                case "entrenarunidad":
                    
                    if (argumentos.Count < 4)
                        return "Faltan argumentos en comando, recordar sintaxis: entrenarunidad <nombreJugador> <tipo> <x> <y>";
                    
                    _fachada.EntrenarUnidad(argumentos[0], argumentos[1], new Point(int.Parse(argumentos[2]), int.Parse(argumentos[3])));
                    return $"Unidad '{argumentos[1]}' entrenada en ({argumentos[2]}, {argumentos[3]}) para el jugador '{argumentos[0]}'.";

                // Ordena a un aldeano recolectar recursos en una posición
                case "recolectar":
                    
                    if (argumentos.Count < 4)
                        return "Faltan argumentos: recolectar <nombreJugador> <idAldeano> <x> <y>";
                    
                    _fachada.OrdenarRecolectar(argumentos[0], int.Parse(argumentos[1]), int.Parse(argumentos[2]), int.Parse(argumentos[3]));
                    return $"Aldeano {argumentos[1]} del jugador '{argumentos[0]}' recolectando en ({argumentos[2]}, {argumentos[3]}).";

                // Mueve una unidad a una nueva posición
                case "moverunidad":
                    
                    if (argumentos.Count < 4)
                        return "Faltan argumentos en comando, recordar sintaxis: moverunidad <nombreJugador> <idUnidad> <x> <y>";
                    
                    _fachada.MoverUnidad(argumentos[0], int.Parse(argumentos[1]), new Point(int.Parse(argumentos[2]), int.Parse(argumentos[3])));
                    return $"Unidad {argumentos[1]} del jugador '{argumentos[0]}' movida a ({argumentos[2]}, {argumentos[3]}).";

                // Ordena a una unidad militar atacar unidades enemigas en una coordenada
                case "atacarunidad":
                    
                    if (argumentos.Count < 4)
                        return "Faltan argumentos en comando, recordar sintaxis: atacarunidad <nombreJugadorAtacante> <idUnidadAtacante> <nombreJugadorObjetivo> <idUnidadObjetivo>";
                    
                    return _fachada.AtacarUnidad(
                        argumentos[0], int.Parse(argumentos[1]), argumentos[2], int.Parse(argumentos[3]));

                // Ordena a una unidad militar atacar un edificio enemigo
                case "atacaredificio":
                    
                    if (argumentos.Count < 4)
                        return "Faltan argumentos en comando, recordar sintaxis: atacaredificio <nombreJugadorAtacante> <idAtacante> <nombreJugadorObjetivo> <idObjetivo>";
                    
                    string resultado = _fachada.AtacarEdificio(
                        argumentos[0], int.Parse(argumentos[1]), argumentos[2], int.Parse(argumentos[3]));
                    
                    if (resultado.Contains("ganó la partida"))
                        partidaFinalizada = true;
                    
                    return resultado;

                // Muestra los recursos del jugador
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

                // Lista las unidades del jugador
                case "unidadesjugador":
                    
                    if (argumentos.Count < 1)
                        return "Faltan argumentos en comando, recordar sintaxis: unidadesjugador <nombre>";
                    
                    List<IUnidad> unidades = _fachada.ObtenerUnidadesJugador(argumentos[0]);
                    StringBuilder sbU = new StringBuilder();
                    
                    for (int i = 0; i < unidades.Count; i++)
                        sbU.AppendLine($"{i}: {unidades[i].GetType().Name}");
                    
                    return sbU.ToString().TrimEnd();

                // Lista los edificios del jugador
                case "edificiosjugador":
                    
                    if (argumentos.Count < 1)
                        return "Faltan argumentos en comando, recordar sintaxis: edificiosjugador <nombre>";
                   
                    List<IEdificio> edificios = _fachada.ObtenerEdificiosJugador(argumentos[0]);
                    StringBuilder sbE = new StringBuilder();
                   
                    for (int i = 0; i < edificios.Count; i++)
                        sbE.AppendLine($"{i}: {edificios[i].GetType().Name}");
                    
                    return sbE.ToString().TrimEnd();

                // Lista todos los recursos en el mapa
                case "listarrecursos":
                    
                    return _fachada.ListarRecursos();

                // Lista todos los jugadores de la partida
                case "listarjugadores":
                    
                    List<Jugador> jugadores = _fachada.ObtenerJugadores();
                    StringBuilder sbJ = new StringBuilder();
                   
                    if (jugadores == null || jugadores.Count == 0)
                        return "No hay jugadores en la partida.";
                    
                    foreach (Jugador jugador in jugadores)
                        sbJ.AppendLine($"Nombre: {jugador.Nombre}, Civilización: {jugador.Civilizacion?.Nombre ?? "Sin civilización"}");
                    
                    return sbJ.ToString().TrimEnd();

                // Muestra el mapa actual
                case "mostrarmapa":
                   
                    return _fachada.MostrarMapa();
                // Muestra el mapa con colores en la consola
                case "mostrarmapacolores":
                    _fachada.MostrarMapaConColores();
                    return "Mapa mostrado en consola con colores.";
                // Sale del juego
                case "salir":
                case "exit":
                    return "Saliendo...";

                // Comando no reconocido
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