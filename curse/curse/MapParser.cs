using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curse
{
    internal class MapParser
    {
        
        public static Map ReadMap(String path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            

            int max = lines[0].Length;
            for(int i = 1; i < lines.Length; i++)
            {
                if(lines[i].Length > max)
                    max = lines[i].Length;
            }
            Map map = new Map(max, lines.Length);

            for (int i = 0; i< lines.Length; i++)
            {
                for(int j = 0; j< lines[i].Length; j++)
                {
                    if(lines[i][j].Equals('/'))
                    {
                        map.SetElement(new SlashWall(), j, i);
                    }
                    if (lines[i][j].Equals('\\'))
                    {
                        map.SetElement(new BackSlashWall(), j, i);
                    }
                    if (lines[i][j].Equals('#'))
                    {
                        map.SetElement(new Wall(), j, i);
                    }
                    if (lines[i][j].Equals('_'))
                    {
                        map.SetElement(new EmptySpace(), j, i);
                    }
                    if (lines[i][j].Equals('*'))
                    {
                        map.SetElement(new Treat(), j, i);
                    }
                }
            }

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < max; x++)
                {
                    if (map.GetElementsMap()[x,y] == null)
                    {
                        map.SetElement(new EmptySpace(), x, y);
                    }
                }
            }

            map.InitBufferMap();
            return map;
        }
    }
}
