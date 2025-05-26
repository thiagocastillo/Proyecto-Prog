namespace Library;

public class Game
{
    public Map Map { get; set; }
    public List<Player> Players { get; set; }
    public int CurrentPlayerIndex { get; set; }
    
    public Game(Map map, List<Player> players)
    {
        Map = map;
        Players = players;
        CurrentPlayerIndex = 0;
    }

    public void StartGame(string[] playerNames, string[] civilizations);
    public string ExcecuteCommand(string command);
    public string GetMap();
    
    public void PlayTurn();
    public bool IsGameOver();
    
    
}