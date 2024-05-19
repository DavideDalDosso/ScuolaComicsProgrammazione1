using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Renderer
{
    private BufferedStream stream = new BufferedStream(Console.OpenStandardOutput(), 0x15000);
    public void Rect(char character, int x, int y, int width, int height)
    {
        int consoleTop = Console.CursorTop;
        int consoleLeft = Console.CursorLeft;

        int behindX = -x;
        if (behindX > 0)
        {
            if (behindX >= width) return;
            x = 0;
            width -= behindX;
        }

        int pastX = (x + width) - Console.WindowWidth;
        if (pastX > 0)
        {
            if (x >= Console.WindowWidth) return;
            width -= pastX;
        }

        int behindY = -y;
        if (behindY > 0)
        {
            if (behindY >= height) return;
            y = 0;
            height -= behindY;
        }

        int pastY = (y + height) - Console.WindowHeight;
        if (pastY > 0)
        {
            if (y >= Console.WindowHeight) return;
            height -= pastY;
        }

        for (; height > 0;)
        {
            --height;
            Console.SetCursorPosition(x, y + height);
            Console.Write(new String(character, width));
        }
        Console.SetCursorPosition(consoleLeft, consoleTop);
    }

    public void Text(string message, int x, int y)
    {
        if (y < 0 || y >= Console.WindowHeight)
        {
            return;
        }

        int behindX = -x;
        if (behindX > 0)
        {
            if (behindX >= message.Length) return;
            x = 0;
            message = message.Substring(behindX);
        }

        int pastX = (x + message.Length) - Console.WindowWidth;
        if(pastX > 0)
        {
            if(x >= Console.WindowWidth) return;
            message = message.Substring(0, message.Length - pastX);
        }

        Console.SetCursorPosition(x, y);
        Console.Write(message);
    }

    public void Text(char character, int x, int y)
    {
        if (x < 0 || x >= Console.WindowWidth || y < 0 || y >= Console.WindowHeight) return;
        Console.SetCursorPosition(x, y);
        Console.Write(character);
    }

    public void Circle(char character, float x, float y, float radius)
    {

        float a = radius * 2;
        float b = radius;

        for (int i = 0; i < radius * 4 + 0.5f; i++)
        {
            for(int j = 0; j < radius * 2 + 0.5f; j++)
            {
                var circleX = i - a;
                var circleY = j - b;

                if ( (circleX * circleX) / (a * a) + (circleY * circleY) / (b * b) <= 1) {
                    Text(character, (int)MathF.Round(circleX + x), (int)MathF.Round(circleY + y));
                }/* else {
                    Text(' ', (int)MathF.Round(circleX + x), (int)MathF.Round(circleY + y));
                } USELESS */
            }
        }
    }

    public void LineHigh(char character, int x1, int y1, int x2, int y2)
    {
        int dx = x2 - x1;
        int dy = y2 - y1;
        int xi = 1;
        if (dx < 0) {
            xi = -1;
            dx = -dx;
        }
        int D = (2 * dx) - dy;
        int x = x1;

        for (int y = y1; y < y2; y++)
        {
            Text(character, x, y);
            if (D > 0) {
                x = x + xi;
                D = D + (2 * (dx - dy));
            } else {
                D = D + 2 * dx;
            }
        }

    }

    public void LineLow(char character, int x1, int y1, int x2, int y2)
    {
        int dx = x2 - x1;
        int dy = y2 - y1;
        int yi = 1;
        if (dy < 0) {
            yi = -1;
            dy = -dy;
        }
        int D = (2 * dy) - dx;
        int y = y1;

        for (int x = x1; x < x2; x++)
        {
            Text(character, x, y);
            if (D > 0)
            {
                y = y + yi;
                D = D + (2 * (dy - dx));
            }
            else
            {
                D = D + 2 * dy;
            }
        }
    }

    public void Line(char character, int x1, int y1, int x2, int y2)
    {
        if ( MathF.Abs(y2 - y1) < MathF.Abs(x2 - x1) ) {
            if (x1 > x2) {
                LineLow(character, x2, y2, x1, y1);
            } else {
                LineLow(character, x1, y1, x2, y2);
            }
        } else {
            if (y1 > y2) {
                LineHigh(character, x2, y2, x1, y1);
            } else {
                LineHigh(character, x1, y1, x2, y2);
            }
        }
    }   
}
