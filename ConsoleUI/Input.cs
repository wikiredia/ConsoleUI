using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices;

namespace ConsoleUI
{
	public static class Input
    {
		[DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT point);
        [DllImport("user32.dll")]
        public static extern bool GetAsyncKeyState(int button);

        public static NativeMethods.INPUT_RECORD record;

		public static bool IsMouseButtonPressed(MouseButton button)
        {
            return GetAsyncKeyState((int)button);
        }

        public enum MouseButton
        {
            LeftMouseButton = 0x01,
            RightMouseButton = 0x02,
            MiddleMouseButton = 0x04,
        }

        public struct POINT
        {
            public int x;
            public int y;

			public override string ToString()
			{
				return $"({x}, {y})";
			}
		}

        public static POINT GetMousePosition()
        {
            POINT pos;
            GetCursorPos(out pos);
            return pos;
        }
    }
}
