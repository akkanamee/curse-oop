namespace curse
{
    class Program
    {
        public static void Main() {
            Console.CursorVisible = false;
            Map map = MapParser.ReadMap(@"C:\Users\Akkanamee\Desktop\curse1\map1.txt");
            map.SetElement(new Ball(), 1, 1);

            bool hasFinished = false;
            while(!hasFinished)
            {
                //check if Enter is pressed
                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
                    
                ConsoleGraphics.ShowMap(map);
                Console.WriteLine();
                Thread.Sleep(1);
                hasFinished = map.NextFrame();
            }
        }
    }
}
