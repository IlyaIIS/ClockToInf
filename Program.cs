using System;
using System.Threading;

namespace Clock
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime realTime = new DateTime(); 

            cord[0, 0, 0] = 270; cord[0, 0, 1] = 120;
            cord[0, 1, 0] = 40; cord[0, 1, 1] = 100;

            cord[1, 0, 0] = 270; cord[1, 0, 1] = 120;
            cord[1, 1, 0] = 40; cord[1, 1, 1] = 100;

            cord[2, 0, 0] = 270; cord[2, 0, 1] = 120;
            cord[2, 1, 0] = 40; cord[2, 1, 1] = 100;

            ClearFieldSign();
            CheckField();
            DrawField();

            do
            {
                realTime = DateTime.Now;

                Console.SetCursorPosition(0, 0);

                cord[2, 1, 0] = (int)(Math.Cos((Math.PI / 60) * ((realTime.Second - 15) * 2) - (45 / 60)) * 200) + 270;
                cord[2, 1, 1] = (int)(Math.Sin((Math.PI / 60) * ((realTime.Second - 15) * 2) - (45 / 60)) * 100) + 120;

                cord[1, 1, 0] = (int)(Math.Cos((Math.PI / 60) * ((realTime.Minute - 15) * 2) - (45 / 60)) * 200) + 270;
                cord[1, 1, 1] = (int)(Math.Sin((Math.PI / 60) * ((realTime.Minute - 15) * 2) - (45 / 60)) * 100) + 120;

                cord[0, 1, 0] = (int)(Math.Cos((Math.PI / 24) * (((realTime.Hour - 18) * 2) - (6))) * 200) + 270;
                cord[0, 1, 1] = (int)(Math.Sin((Math.PI / 24) * (((realTime.Hour - 18) * 2) - (6))) * 100) + 120;

                CheckField();
                SetClock();
                DrawField();

                Thread.Sleep(1000);
            } while (true);

        }

        static int[] fieldSize = { 60, 28 };

        static int[,,] cord = new int[3, 2, 2];  //[lineNum;pointNum;x|y]
        static int[,] field = new int[fieldSize[0], fieldSize[1]];
        static char[,] fieldSign = new char[fieldSize[0], fieldSize[1]];
        static int step = 100;
        static int[] lineSize = { 8, 7, 6 };
        static dynamic[] colorList = { ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.White };

        static void CheckField()
        {
            for (int y = 0; y < field.GetLength(1); y++)
            {
                for (int x = 0; x < field.GetLength(0); x++)
                {
                    field[x, y] = 0;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                int x1 = cord[i, 0, 0];
                int y1 = cord[i, 0, 1];
                int x2 = cord[i, 1, 0];
                int y2 = cord[i, 1, 1];

                float xse = x1, yse = y1;

                step = (int)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)) / 10;

                float xk = (float)(x2 - x1) / step;
                float yk = (float)(y2 - y1) / step;

                for (int ii = 1; ii <= step; ii++)
                {
                    for (int y = 0; y < field.GetLength(1); y++)
                    {
                        for (int x = 0; x < field.GetLength(0); x++)
                        {
                            if (Math.Abs(xse - x * 10) < lineSize[i] && Math.Abs(yse - y * 10) < lineSize[i])
                            {
                                field[x, y] = i+1;
                            }
                        }
                    }

                    xse += xk;
                    yse += yk;
                }
            }
        }
        static void DrawField()
        {
            for (int y = 0; y < field.GetLength(1); y++)
            {
                for (int x = 0; x < field.GetLength(0); x++)
                {
                    if (fieldSign[x, y] != 'n') 
                      Console.Write(fieldSign[x, y]);
                    else
                    {
                        if (field[x, y] != 0)
                        {
                            Console.ForegroundColor = colorList[field[x, y] - 1];
                            Console.Write('█');
                            //Console.Write('■');
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(' ');
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        static void SetClock ()
        {
            SetFieldSign(24, 0, "╔═════╗");

            SetFieldSign(14, 1, "╔═════════╣ 1 2 ╠═════════╗");

            SetFieldSign(14, 2, "║");
            SetFieldSign(24, 2, "╚═════╝");
            SetFieldSign(40, 2, "║");

            SetFieldSign(10, 3, "╔═══╝");
            SetFieldSign(40, 3, "╚═══╗");

            SetFieldSign(10, 4, "║");
            SetFieldSign(44, 4, "║");

            SetFieldSign(6 , 5, "╔═══╝");
            SetFieldSign(44, 5, "╚═══╗");

            for (int i = 6; i <= 10; i++)
            {
                SetFieldSign(6, i, "║");
                SetFieldSign(48, i, "║");
            }

            SetFieldSign(1 , 11, "╔════╩╗");
            SetFieldSign(25, 11, "╔═══╗");
            SetFieldSign(47, 11, "╔╩════╗");

            SetFieldSign(1, 12, "║  9  ║");
            SetFieldSign(25, 12, "║   ║");
            SetFieldSign(47, 12, "║  3  ║");

            SetFieldSign(1, 13, "╚════╦╝");
            SetFieldSign(25, 13, "╚═══╝");
            SetFieldSign(47, 13, "╚╦════╝");

            for (int i = 14; i <= 18; i++)
            {
                SetFieldSign(6, i, "║");
                SetFieldSign(48, i, "║");
            }

            SetFieldSign(6, 19, "╚═══╗");
            SetFieldSign(44, 19, "╔═══╝");

            SetFieldSign(10, 20, "║");
            SetFieldSign(44, 20, "║");

            SetFieldSign(10, 21, "╚═══╗");
            SetFieldSign(40, 21, "╔═══╝");

            SetFieldSign(14, 22, "║");
            SetFieldSign(24, 22, "╔═════╗");
            SetFieldSign(40, 22, "║");

            SetFieldSign(14, 23, "╚═════════╣  6  ╠═════════╝");

            SetFieldSign(24, 24, "╚═════╝");
        }

        static void SetFieldSign (int x, int y, string str)
        {
            for (int i = 0; i < str.Length && x + i < field.GetLength(0); i++) 
            {
                fieldSign[x + i, y] = str[i];
            }
        }

        static void ClearFieldSign()
        {
            for (int y = 0; y < field.GetLength(1); y++)
            {
                for (int x = 0; x < field.GetLength(0); x++)
                {
                    fieldSign[x, y] = 'n';
                }
            }
        }

    }


}