using System;

namespace Game
{
    class Program
    {
        static bool endFlag = false;
        static int x = 1;
        static int y = 1;
        static char sprite = 'P';
        static int life = 10;
        static int[] endCoordinates;

        static void Main()
        {
            Console.CursorVisible = false;
            Random rand = new Random();
            y = rand.Next(1, 11);
            DrawField();
            int[] trapCoordinates = new int[20];
            for (int i = 0; i < 20; i++)
            {
                trapCoordinates[i] = rand.Next(1, 11);
            }

            while (true)
            {
                UpdatePlayerPosition(sprite);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (x <= 1)
                        {
                            continue;
                        }
                        UpdatePlayerPosition(' ');
                        x--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (x >= 10)
                        {
                            continue;
                        }
                        UpdatePlayerPosition(' ');
                        x++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (y <= 1)
                        {
                            continue;
                        }
                        UpdatePlayerPosition(' ');
                        y--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (y >= 10)
                        {
                            continue;
                        }
                        UpdatePlayerPosition(' ');
                        y++;
                        break;
                }

                for (int i = 0, j = 1; i < 20; i += 2, j += 2)
                {
                    if (x == trapCoordinates[i] && y == trapCoordinates[j])
                    {
                        life -= rand.Next(1, 11);
                        Console.SetCursorPosition(0, 14);
                        Console.Write($"You're trapped. Remaining life: {life}");
                    }

                    if (life < 1)
                    {
                        End();
                    }
                }

                if (x == endCoordinates[0] && y == endCoordinates[1])
                {
                    endFlag = true;
                    End();
                }
            }

            static void DrawField()
            {
                char ch = '*';
                for (int i = 0, j = 0; i < 12; i++, j++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write(ch);
                    Console.SetCursorPosition(i, 11);
                    Console.Write(ch);
                    Console.SetCursorPosition(0, j);
                    Console.Write(ch);
                    Console.SetCursorPosition(11, j);
                    Console.Write(ch);
                }

                if (y >= 1 && y < 5)
                {
                    endCoordinates = new int[] { 10, 10 };
                    Console.SetCursorPosition(10, 10);
                }
                else if (y > 5 && y <= 10)
                {
                    endCoordinates = new int[] { 10, 1 };
                    Console.SetCursorPosition(10, 1);
                }
                else
                {
                    Random rand = new Random();

                    if (rand.Next(1, 3) == 1)
                    {
                        endCoordinates = new int[] { 10, 1 };
                        Console.SetCursorPosition(10, 1);
                    }
                    else
                    {
                        endCoordinates = new int[] { 10, 10 };
                        Console.SetCursorPosition(10, 10);
                    }
                }
                Console.WriteLine("?");
            }

            static void UpdatePlayerPosition(char sprite)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(sprite);
            }

            static void End()
            {
                Console.Clear();
                if (endFlag)
                {
                    Console.WriteLine("Congratulations, you won!\nWant to play again? (y/n)");
                }
                else
                {
                    Console.WriteLine("Sorry, you lost :(\nWant to try again? (y/n)");
                }

                string tryAgain = Console.ReadLine();
                switch (tryAgain)
                {
                    case "y":
                        Console.Clear();
                        x = 1;
                        life = 10;
                        endFlag = false;
                        Main();
                        break;

                    case "n":
                        Environment.Exit(0);
                        break;

                    default:
                        End();
                        break;
                }
            }
        }
    }
}
