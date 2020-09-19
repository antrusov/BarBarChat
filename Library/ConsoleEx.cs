using System;

namespace Library
{
    public class ConsoleEx
    {
        public static void Print(int x, int y, string msg)
        {
            Console.CursorLeft = x;
            Console.CursorTop = y;
            Console.Write(msg);
        }
    }
}
