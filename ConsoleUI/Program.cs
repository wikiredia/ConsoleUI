using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Basic Info
            const string TITLE = "Test Window";

            const int CONSOLE_WIDTH = 120;
            const int CONSOLE_HEIGHT = 30;
            #endregion

            #region Console Initialization
            Console.SetWindowSize(CONSOLE_WIDTH, CONSOLE_HEIGHT);
            Console.SetBufferSize(CONSOLE_WIDTH, CONSOLE_HEIGHT);

            Console.CursorVisible = false;
            Console.Title = TITLE;
            #endregion

            Text test = new Text("Test", new Vector2(0, 0), true);
            InputField input = new InputField("Name", new Vector2(1, 10), "Enter your name...");

            input.SetPosition(new Vector2(1, 20));

            Text mousePos = new Text($"({Input.GetMousePosition().x}, {Input.GetMousePosition().y})", new Vector2(30, 0), false);
            
            while(true)
            {
                mousePos.ChangeText($"{Input.GetMousePosition()}");
                Thread.Sleep(100);
            }

            Console.ReadLine();
        }
    }
}
