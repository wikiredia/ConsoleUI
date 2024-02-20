using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
	class InputField
	{
		public string title { get; private set; }
		public string placeholder { get; private set; }
		public Vector2 position { get; private set; }

		public InputField(string title, Vector2 position, string placeholder="Type Here...")
		{
			this.title = title;
			this.placeholder = $" {placeholder} ";
			this.position = position;

			DrawInputField();
		}

		public void DrawInputField()
		{
			ClearInputField();
			//  Type Here...
			Console.SetCursorPosition(position.x + 1, position.y);
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.Write(placeholder);
			Console.ResetColor();
			
			// Name
			//
			//  Type Here...
			Console.SetCursorPosition(position.x, position.y-2);
			Console.Write(title);

			// Name
			// --------------
			//  Type Here...
			// --------------
			Console.SetCursorPosition(position.x, position.y-1);
			for(int i=0;i<placeholder.Length+2;i++) { Console.Write("-"); }
			Console.SetCursorPosition(position.x, position.y+1);
			for(int i=0;i<placeholder.Length+2;i++) { Console.Write("-"); }

			// Name
			// ----------------
			// # Type Here... #
			// ----------------
			Console.SetCursorPosition(position.x, position.y);
			Console.Write("#");
			Console.SetCursorPosition(position.x+placeholder.Length+1, position.y);
			Console.Write("#");
		}

		private void ClearInputField()
		{
			Console.SetCursorPosition(position.x, position.y);
			for(int i=0;i<placeholder.Length+2;i++) { Console.Write(" "); }
			
			Console.SetCursorPosition(position.x, position.y-2);
			for(int i=0;i<title.Length;i++) { Console.Write(" "); }

			Console.SetCursorPosition(position.x, position.y-1);
			for(int i=0;i<placeholder.Length+2;i++) { Console.Write(" "); }
			Console.SetCursorPosition(position.x, position.y+1);
			for(int i=0;i<placeholder.Length+2;i++) { Console.Write(" "); }

		}

		public void SetPosition(Vector2 newPosition)
		{
			ClearInputField();
			this.position = newPosition;
			DrawInputField();
		}
	}
}
