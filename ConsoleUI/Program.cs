using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static ConsoleUI.PositionConsoleWindow;


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
            IntPtr hWnd = PositionConsoleWindow.GetConsoleWindow();
            var mi = MONITORINFO.Default;
            GetMonitorInfo(MonitorFromWindow(hWnd, MONITOR_DEFAULTTOPRIMARY), ref mi);

            // Get information about this window's current placement.
            var wp = WINDOWPLACEMENT.Default;
            GetWindowPlacement(hWnd, ref wp);
            int fudgeOffset = 0;
            wp.NormalPosition = new RECT()
            {
                Left = -fudgeOffset,
                Top = mi.rcWork.Bottom - (wp.NormalPosition.Bottom - wp.NormalPosition.Top),
                Right = (wp.NormalPosition.Right - wp.NormalPosition.Left),
                Bottom = fudgeOffset + mi.rcWork.Bottom
            };

            // Place the window at the new position.
            SetWindowPlacement(hWnd, ref wp);


            Console.SetWindowSize(CONSOLE_WIDTH, CONSOLE_HEIGHT);
            Console.SetBufferSize(CONSOLE_WIDTH, CONSOLE_HEIGHT);

            Console.CursorVisible = false;
            Console.Title = TITLE;
            #endregion

            Text test = new Text("Test", new Vector2(0, 0), true);
            InputField input = new InputField("Name", new Vector2(1, 10), "Enter your name...");

            input.SetPosition(new Vector2(1, 20));

            Text mousePos = new Text($"({Input.GetMousePosition().x}, {Input.GetMousePosition().y})", new Vector2(30, 0), false);
            Thread TMainLoop = new Thread(new ThreadStart(PrintCursorPosition));
            TMainLoop.Start();
            Console.ReadLine();

            void PrintCursorPosition()
            {
                while (true)
                {
                    mousePos.ChangeText($"({Input.GetMousePosition().x}, {Input.GetMousePosition().y})");
                    Thread.Sleep(100);
                }
            }
        }
    }
}
