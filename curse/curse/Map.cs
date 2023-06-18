using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curse
{

    enum Direction { LEFT, RIGHT, UP, DOWN}
    internal class Map
    {
        private int height;
        private int width;
        private MapElement[,] elementsMap;
        private MapElement[,] bufferMap;
        private Direction ballDirection = Direction.RIGHT;
        private int collectedTreats;
        private int treatsAmount = 0;

        public void InitBufferMap()
        {
            bufferMap = (MapElement[,]) elementsMap.Clone();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (bufferMap[x, y].GetType() == typeof(Treat))
                    {
                        bufferMap[x, y] = new EmptySpace();
                    }
                }
            }

            bufferMap[1,1] = new EmptySpace();
        }
        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            elementsMap = new MapElement[width, height];
        }

        public void SetElement(MapElement element, int x, int y)
        {
            elementsMap[x, y] = element;
        }

        public int[] FindBallCoords()
        {
            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    if(elementsMap[x, y].GetType() == typeof(Ball))
                    {
                        return new int[] { x, y };
                    }
                }
            }
            return null;
        }

        public int CountTreats()
        {
            int amount = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (elementsMap[x, y].GetType() == typeof(Treat))
                    {
                        amount++;
                    }
                }
            }
            return amount;
        }

        public bool NextFrame()
        {
            int[] ballCoords = FindBallCoords();
            if(treatsAmount == 0)
            {
                treatsAmount = treatsAmount = this.CountTreats();
            }
            if (ballCoords[0] == width - 2 && ballCoords[1] == height - 2)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("reached finish");
                Console.WriteLine("collected " + collectedTreats + " out of " + treatsAmount + " points");
                return true;
            }
            Console.WriteLine("collected points: " + collectedTreats + "/" + treatsAmount);
            Console.WriteLine("press 'Enter' for force quit");

            switch (ballDirection)
            {
                case Direction.LEFT:
                    if (elementsMap[ballCoords[0] - 1, ballCoords[1]].GetType() == typeof(Wall))
                    {
                        ballDirection = Direction.RIGHT;
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0] - 1, ballCoords[1]);
                    }
                    if (elementsMap[ballCoords[0]-1, ballCoords[1]].GetType() == typeof(EmptySpace))
                    {
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0] - 1, ballCoords[1]);
                    }
                    if (elementsMap[ballCoords[0] - 1, ballCoords[1]].GetType() == typeof(Treat))
                    {
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0] - 1, ballCoords[1]);
                        collectedTreats++;
                    }
                    if (elementsMap[ballCoords[0] - 1, ballCoords[1]].GetType() == typeof(BackSlashWall))
                    {
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0] - 1, ballCoords[1]);
                        ballDirection = Direction.UP;
                    }
                    if (elementsMap[ballCoords[0] - 1, ballCoords[1]].GetType() == typeof(SlashWall))
                    {
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0] - 1, ballCoords[1]);
                        ballDirection = Direction.DOWN;
                    }
                    break;
                case Direction.RIGHT:
                    if (elementsMap[ballCoords[0] + 1, ballCoords[1]].GetType() == typeof(Wall))
                    {
                        ballDirection = Direction.LEFT;
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0] + 1, ballCoords[1]);
                    }
                    if (elementsMap[ballCoords[0] + 1, ballCoords[1]].GetType() == typeof(EmptySpace))
                    {
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0] + 1, ballCoords[1]);
                    }
                    if (elementsMap[ballCoords[0] + 1, ballCoords[1]].GetType() == typeof(Treat))
                    {
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0] + 1, ballCoords[1]);
                        collectedTreats++;
                    }
                    if (elementsMap[ballCoords[0] + 1, ballCoords[1]].GetType() == typeof(BackSlashWall))
                    {
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0] + 1, ballCoords[1]);
                        ballDirection = Direction.DOWN;
                    }
                    if (elementsMap[ballCoords[0] + 1, ballCoords[1]].GetType() == typeof(SlashWall))
                    {
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0] + 1, ballCoords[1]);
                        ballDirection = Direction.UP;
                    }

                    break;
                case Direction.UP:
                    if (elementsMap[ballCoords[0], ballCoords[1] - 1].GetType() == typeof(Wall))
                    {
                        ballDirection = Direction.DOWN;
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0], ballCoords[1] - 1);
                    }
                    if (elementsMap[ballCoords[0], ballCoords[1] - 1].GetType() == typeof(EmptySpace))
                    {
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0], ballCoords[1] - 1);
                    }
                    if (elementsMap[ballCoords[0], ballCoords[1] - 1].GetType() == typeof(Treat))
                    {
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0], ballCoords[1] - 1);
                        collectedTreats++;
                    }
                    if (elementsMap[ballCoords[0], ballCoords[1] - 1].GetType() == typeof(SlashWall))
                    {
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0], ballCoords[1] - 1);
                        ballDirection = Direction.RIGHT;
                    }
                    if (elementsMap[ballCoords[0], ballCoords[1] - 1].GetType() == typeof(BackSlashWall))
                    {
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0], ballCoords[1] - 1);
                        ballDirection = Direction.LEFT;
                    }

                    break;
                case Direction.DOWN:
                    if (elementsMap[ballCoords[0], ballCoords[1]+1].GetType() == typeof(Wall))
                    {
                        ballDirection = Direction.UP;
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0], ballCoords[1]+1);
                    }
                    if (elementsMap[ballCoords[0], ballCoords[1] + 1].GetType() == typeof(EmptySpace))
                    {
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0], ballCoords[1] + 1);
                    }
                    if (elementsMap[ballCoords[0], ballCoords[1] + 1].GetType() == typeof(Treat))
                    {
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0], ballCoords[1] + 1);
                        collectedTreats++;
                    }
                    if (elementsMap[ballCoords[0], ballCoords[1] + 1].GetType() == typeof(SlashWall))
                    {
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0], ballCoords[1] + 1);
                        ballDirection = Direction.LEFT;
                    }
                    if (elementsMap[ballCoords[0], ballCoords[1] + 1].GetType() == typeof(BackSlashWall))
                    {
                        this.SetElement(bufferMap[ballCoords[0], ballCoords[1]], ballCoords[0], ballCoords[1]);
                        this.SetElement(new Ball(), ballCoords[0], ballCoords[1] + 1);
                        ballDirection = Direction.RIGHT;
                    }
                    break;
            }
            return false;
        }

        public MapElement[,] GetElementsMap()
        {
            return elementsMap;
        }

        public int GetHeight()
        {
            return height;
        }

        public int GetWidth()
        {
            return width;
        }
    }
}
