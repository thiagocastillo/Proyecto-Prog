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

    public void StartGame();
    public void PlayTurn();
    public bool IsGameOver();
}