using System.Text.Json;
using System.Text.Json.Serialization;
using Library.Services;

namespace Library.Domain;

public class Partida: IJsonConvertible
{
    public Mapa Mapa { get; private set; }  //Se representa el mapa
       
    [JsonInclude]
    public List<Jugador> Jugadores { get; private set; } = new List<Jugador>();  // Lista de jugadores que participan en la partida.

    [JsonConstructor]
    public Partida()  // Constructor de la clase Partida.
    {
        Mapa = new Mapa();
    }

    public string AgregarJugador(Jugador jugador)  // Método que permite agregar un jugador a la partida.
    {
        Jugadores.Add(jugador);
        
        if (Jugadores.Count > 2)
        {
           return "Solo se permiten dos jugadores";  // el maximo son dos jugadores.
        }
        return "Jugador agregado correctamente";   // Retorna un mensaje indicando si fue agregado correctamente
    }
    public Jugador VerificarGanador()   // Método que verifica si hay un ganador de la partida.
    {
        foreach (Jugador jugador in Jugadores)  // Recorre todos los jugadores para verificar si perdieron su centro cívico
        {
            bool tieneCentroCivico = jugador.Edificios.Any(e => e is CentroCivico);  // Comprueba si el jugador tiene al menos un edificio que sea un Centro Cívico
         
            if (!tieneCentroCivico)  // Si no tiene centro cívico, el otro jugador es el ganador
            {
                return Jugadores.FirstOrDefault(j => j != jugador);
            }
        }
        return null; // Si ambos tienen su centro cívico, aún no hay ganador
    }
    
    public string ConvertToJson()
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.Preserve
        };
    
        return JsonSerializer.Serialize(this, options);
    }
    
    public void LoadFromJson(string json)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };

        Partida deserialized = JsonSerializer.Deserialize<Partida>(json, options);

        this.Mapa = deserialized.Mapa;
        this.Jugadores = deserialized.Jugadores;
    }
}