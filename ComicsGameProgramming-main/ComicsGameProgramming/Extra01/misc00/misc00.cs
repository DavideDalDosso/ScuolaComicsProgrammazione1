using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

class misc00
{
    public static void Main(string[] args)
    {
        Console.WriteLine("B");

        int charX = 1;
        int charY = 2;

        Thread t = new Thread(new ThreadStart(() =>
        {
            while (true)
            {
                ConsoleDraw(' ', 0, 0, Console.WindowWidth, Console.WindowHeight);
                ConsoleWrite("O", charX, charY);
                Console.SetCursorPosition(0, 0);
                Thread.Sleep(1000/60);
            }
        }));

        t.Start();

        while (true)
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                    charY--;
                    break;
                case ConsoleKey.A:
                    charX--;
                    break;
                case ConsoleKey.S:
                    charY++;
                    break;
                case ConsoleKey.D:
                    charX++;
                    break;
            }

            if (charX < 0) charX++;
            if (charX > Console.WindowWidth-1) charX--;
            if (charY < 0) charY++;
            if (charY > Console.WindowHeight-1) charY--;

            Thread.Sleep(1000/60);
        }
        
    }

    private static void ConsoleDraw(char character, int x, int y, int width, int height)
    {
        int consoleTop = Console.CursorTop;
        int consoleLeft = Console.CursorLeft;
        for (; height > 0; ){
            --height;
            Console.SetCursorPosition(x, y + height);
            Console.Write(new String(character, width));
        }
        Console.SetCursorPosition(consoleLeft, consoleTop);
    }

    private static void ConsoleWrite(string message, int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(message);
    }
}
