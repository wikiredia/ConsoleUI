using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
	class InputField : IRenderable, IClickable
	{
		public string title { get; private set; }
		public string placeholder { get; private set; }
		public Vector2 position { get; private set; }

		public InputField(string title, Vector2 position, string placeholder="Type Here...")
		{
			this.title = title;
			this.placeholder = $" {placeholder} ";
			this.position = position;
			ConsoleItems.AllInputFieldItems.Add(this);
			Render();
		}

		public void Render()
		{
			Clear();			

			// --------------
			//  
			// --------------
			Console.SetCursorPosition(position.x, position.y-1);
			for(int i=0;i<placeholder.Length+2;i++) { Console.Write("-"); }
			Console.SetCursorPosition(position.x, position.y+1);
			for(int i=0;i<placeholder.Length+2;i++) { Console.Write("-"); }

			// ----------------
			// #              #
			// ----------------
			Console.SetCursorPosition(position.x, position.y);
			Console.Write("#");
			Console.SetCursorPosition(position.x+placeholder.Length+1, position.y);
			Console.Write("#");

			// ----------------
			// # Type Here... #
			// ----------------
			Console.SetCursorPosition(position.x + 1, position.y);
			Console.Write(placeholder);

			// Name
			// ----------------
			// # Type Here... #
			// ----------------
			Console.BackgroundColor = ConsoleColor.Black;
			Console.SetCursorPosition(position.x, position.y-2);
			Console.Write(title);
		}

		public void Clear()
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
			Clear();
			this.position = newPosition;
			Render();
		}

		public bool IsHovering(Vector2 point)
		{
			// POINTvsAABB collision check
			return (point.x >= position.x && point.x <= position.x+placeholder.Length+1 && point.y >= position.y-1 && point.y <= position.y+1);
		}

		public void ChangeColor(ConsoleColor ForegroundColor=ConsoleColor.White, ConsoleColor BackgroundColor=ConsoleColor.Black)
		{
			Console.ForegroundColor = ForegroundColor;
			Console.BackgroundColor = BackgroundColor;
			Render();
		}

		public void OnClick()
		{
			ChangeColor(ConsoleColor.White, ConsoleColor.DarkGray);
			Console.ResetColor();
		}

		public void UnFocus()
		{
			ChangeColor();
		}
	}
}
