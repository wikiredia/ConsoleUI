using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static ConsoleUI.PositionConsoleWindow;
using Microsoft.Win32.SafeHandles;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Basic Info
            const string TITLE = "CS50X Hospital!";

            const int CONSOLE_WIDTH = 120;
            const int CONSOLE_HEIGHT = 30;
            #endregion

            #region Console Initialization
            // Initialization code for Mouse Input
            var handle = NativeMethods.GetStdHandle(NativeMethods.STD_INPUT_HANDLE);

            int mode = 0;
            if (!(NativeMethods.GetConsoleMode(handle, ref mode))) { throw new Win32Exception(); }

            mode |= NativeMethods.ENABLE_MOUSE_INPUT;
            mode &= ~NativeMethods.ENABLE_QUICK_EDIT_MODE;
            mode |= NativeMethods.ENABLE_EXTENDED_FLAGS;

            if (!(NativeMethods.SetConsoleMode(handle, mode))) { throw new Win32Exception(); }

            var record = new NativeMethods.INPUT_RECORD();
            uint recordLen = 0;

            // PositionConsoleWindow.cs & Line 26 - 40 | Credit: https://stackoverflow.com/a/42334329
            IntPtr hWnd = PositionConsoleWindow.GetConsoleWindow();
            var mi = MONITORINFO.Default;
            GetMonitorInfo(MonitorFromWindow(hWnd, MONITOR_DEFAULTTOPRIMARY), ref mi);

            // Get information about this window's current placement.
            var wp = WINDOWPLACEMENT.Default;
            GetWindowPlacement(hWnd, ref wp);
            int fudgeOffset = -300;
            wp.NormalPosition = new RECT()
            {
                Left = -fudgeOffset,
                Top = fudgeOffset+mi.rcWork.Bottom - (wp.NormalPosition.Bottom - wp.NormalPosition.Top),
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

            Text test = new Text(TITLE, new Vector2(CONSOLE_WIDTH/2-TITLE.Length/2, 2), true);
            InputField firstName = new InputField("         First Name", new Vector2(CONSOLE_WIDTH/2-"Enter your first name...".Length/2, 10), "Enter your first name...");
            InputField lastName = new InputField("         Last Name", new Vector2(CONSOLE_WIDTH/2-"Enter your last name...".Length/2-1, 15), "Enter your last name...");

            Thread TInputHandler = new Thread(new ThreadStart(InputHandler));
            TInputHandler.Start();

            while(true)
            {
                Input.record = record;
            }

            void InputHandler()
            {
                while (true) {
                    if (!(NativeMethods.ReadConsoleInput(handle, ref record, 1, ref recordLen))) { throw new Win32Exception(); }
                    Thread.Sleep(34);
                    ConsoleItems.MainLoop();
                }
            }
        }
    }
}
