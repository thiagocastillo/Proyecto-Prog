using System.Diagnostics;

namespace Library;

public class Map
{
    private int width;
    private int height;
    private Random random = new Random();

    public Map()
    {
        width = random.Next(1, 100);
        height = random.Next(1, 100);
    }
    
}