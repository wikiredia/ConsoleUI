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
		public string text { get; private set; } = "";
		public string placeholder { get; private set; }
		public Vector2 position { get; private set; }

		public bool _isFocused { get; private set; } = false;

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
			ConsoleColor backup = Console.BackgroundColor;
			Console.BackgroundColor = ConsoleColor.Black;
			Console.SetCursorPosition(position.x, position.y-2);
			Console.Write(title);
			Console.BackgroundColor = backup;
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
			RemovePlaceholderToType();
			Console.ResetColor();
			_isFocused = true;
		}

		public void UnFocus()
		{
			_isFocused = false;
			ChangeColor();
		}

		private void RemovePlaceholderToType()
		{
			Console.SetCursorPosition(position.x + 1, position.y);
			ConsoleColor backup = Console.BackgroundColor;
			Console.Write(" ");
			Console.BackgroundColor = ConsoleColor.White;
			Console.Write(" ");
			Console.BackgroundColor = backup;
			for(int i=0;i<placeholder.Length-3;i++) { Console.Write(" "); }
		}

		public void ChangeText(char character)
		{
			text += character;
			Console.SetCursorPosition(position.x+1+text.Length, position.y);
			Console.Write(character);
		}
	}
}
