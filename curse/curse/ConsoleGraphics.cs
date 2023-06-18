using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curse
{
    internal class ConsoleGraphics
    {
        public static void ShowMap(Map map)
        {
            MapElement[,] elementsMap = map.GetElementsMap();

            for (int y = 0; y < map.GetHeight(); y++)
            {
                for (int x = 0; x < map.GetWidth(); x++)
                {
                    if (elementsMap[x, y] != null)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(elementsMap[x, y].ToString());
                    }
                }
            }
        }
    }
}
