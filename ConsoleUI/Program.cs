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

            Console.ReadLine();
        }
    }
}
