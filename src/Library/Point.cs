namespace Library;

   public class Point
   {
       public int X { get; set; }  //Coordenadea del eje de las abscisas
       public int Y { get; set; }  //Coordenada del eje de las ordenadas

       public Point() { }  //Constructor vacio, para iniciar en (0,0)

       public Point(int x, int y)  //Constructor que recibe parametro, las coordenadas
       {
           X = x;
           Y = y;
       }
   }
