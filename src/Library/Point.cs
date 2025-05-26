namespace Library;

   public class Point
   {
       public int X { get; set; }
       public int Y { get; set; }

       public Point() { } // Constructor sin parámetros (opcional, pero común en clases)

       public Point(int x, int y)
       {
           X = x;
           Y = y;
       }
   }
