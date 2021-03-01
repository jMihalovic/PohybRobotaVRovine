using System;
using System.Threading;



namespace Robot_v_rovině
{
    class Program
    {
        static string[,] labyrint = new string[,] {
        {"Z","0","0","0","0","0","0","0","0"},
        {"0","1","1","1","1","1","0","0","0"},
        {"1","0","0","0","0","0","0","1","1"},
        {"0","0","0","0","1","0","0","1","1"},
        {"1","1","1","1","1","1","0","1","1"},
        {"1","1","1","1","1","0","0","0","0"},
        {"0","0","K","0","0","0","0","0","0"}
        };

        static int[] priority = new int[]
            {1,4,0,3};
        //0 = nahoru, 1 = dolů, 3 = doleva, 4 = doprava

        static int lastMove;

        static int xCoord;

        static int yCoord;

        static int xEndCoord;

        static int yEndCoord;

        static void Main(string[] args)
        {
            Setup();
            while (xCoord != xEndCoord || yCoord != yEndCoord)
            {
                Vypis();
                Move();
            }
            Vypis();
            Console.WriteLine("Robot úspěšně dojel do cíle!");
            Console.ReadLine();
        }

        static void Vypis()
        {
            Console.Clear();
            Console.WriteLine(@"Pohyb robota v rovině
");
            for (int i = 0; i < labyrint.GetLength(0); i++)
            {
                for (int j = 0; j < labyrint.GetLength(1); j++)
                {
                    if (i == yCoord && j == xCoord)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("R  ");
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(labyrint[i, j] + "  ");
                    }
                }
                Console.WriteLine(@"
");
            }
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Setup()
        {
            for (int i = 0; i < labyrint.GetLength(0); i++)
            {
                for (int j = 0; j < labyrint.GetLength(1); j++)
                {
                    if (labyrint[i, j] == "Z")
                    {
                        yCoord = i;
                        xCoord = j;
                    }
                    if (labyrint[i, j] == "K")
                    {
                        yEndCoord = i;
                        xEndCoord = j;
                    }
                }
            }
        }

        static void Move()
        {
            bool end = false;
            int memory = 0;
            string s;
            while (end == false)
            {
                for (int i = memory; i < priority.Length; i++)
                {
                    switch (priority[i])
                    {
                        case 0:
                            s = TryUp();
                            if (s == "0" || s == "Z" || s == "K") { yCoord--; lastMove = 0; end = true; }
                            break;
                        case 1:
                            s = TryDown();
                            if (s == "0" || s == "Z" || s == "K") { yCoord++; lastMove = 1; end = true; }
                            break;
                        case 3:
                            s = TryLeft();
                            if (s == "0" || s == "Z" || s == "K") { xCoord--; lastMove = 3; end = true; }
                            break;
                        case 4:
                            s = TryRight();
                            if (s == "0" || s == "Z" || s == "K") { xCoord++; lastMove = 4; end = true; }
                            break;
                        default:
                            break;
                    }
                    memory = i + 1;
                    break;
                }
            }
            SerazeniPriorit();
        }
        static string TryUp()
        {
            try
            {
                return labyrint[yCoord - 1, xCoord];
            }
            catch
            {
                return "1";
            }
        }
        static string TryDown()
        {
            try
            {
                return labyrint[yCoord + 1, xCoord];
            }
            catch
            {
                return "1";
            }
        }
        static string TryLeft()
        {
            try
            {
                return labyrint[yCoord, xCoord - 1];
            }
            catch
            {
                return "1";
            }
        }
        static string TryRight()
        {
            try
            {
                return labyrint[yCoord, xCoord + 1];
            }
            catch
            {
                return "1";
            }
        }

        static void SerazeniPriorit()
        {
            if (lastMove + 1 == 1)
            {
                //last prio dolu    2nd prio nahoru
                if (xCoord < xEndCoord)
                {
                    priority = new int[]
                    {4,0,3,1};
                }
                else
                {
                    priority = new int[]
                    {3,0,4,1};
                }
            }
            if (lastMove + 1 == 2)
            {
                //last prio nahoru  2nd prio dolu
                if (xCoord < xEndCoord)
                {
                    priority = new int[]
                    {4,1,3,0};
                }
                else
                {
                    priority = new int[]
                    {3,1,4,0};
                }
            }
            if (lastMove + 1 == 4)
            {
                //last prio doprava 2nd prio doleva
                if (yCoord < yEndCoord)
                {
                    priority = new int[]
                    {1,3,0,4};
                }
                else
                {
                    priority = new int[]
                    {0,3,1,4};
                }
            }
            if (lastMove + 1 == 5)
            {
                //last prio doleva  2nd prio doprava
                if (yCoord < yEndCoord)
                {
                    priority = new int[]
                    {1,4,0,3};
                }
                else
                {
                    priority = new int[]
                    {0,4,1,3};
                }
            }

        }
    }
}
