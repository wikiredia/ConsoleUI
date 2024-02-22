using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
	class InputField : IRenderable, IClickable
	{
		public string title { get; private set; }
		public string text { get; set; } = "";
		public string placeholder { get; private set; }
		public Vector2 position { get; private set; }

		public bool _isFocused { get; private set; } = false;

		public static ConsoleColor defaultBGcolor { get; } = ConsoleColor.Black;
		public static ConsoleColor defaultFGcolor { get; } = ConsoleColor.White;

		private ConsoleColor _backgroundcolor { get; set; }
		private ConsoleColor _foregroundcolor { get; set; }
		public ConsoleColor backgroundcolor
		{
			get
			{
				return _backgroundcolor;
			}

			set
			{
				_backgroundcolor = value;
				Console.BackgroundColor = _backgroundcolor;
			}
		}
		public ConsoleColor foregroundcolor
		{
			get
			{
				return _foregroundcolor;
			}

			set
			{
				_foregroundcolor = value;
				Console.ForegroundColor = _foregroundcolor;
			}
		}

		public InputField(string title, Vector2 position, string placeholder="Type Here...")
		{
			this.title = title;
			this.placeholder = $" {placeholder} ";
			this.position = position;
			foregroundcolor = ConsoleColor.White;
			backgroundcolor = ConsoleColor.Black;
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
			backgroundcolor = defaultBGcolor;
			Console.SetCursorPosition(position.x, position.y-2);
			Console.Write(title);
			foregroundcolor = backup;
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
			this.foregroundcolor = ForegroundColor;
			this.backgroundcolor = BackgroundColor;	
			Render();
		}

		public void OnClick()
		{
			ChangeColor(ConsoleColor.White, ConsoleColor.DarkGray);
			RemovePlaceholderToType();
			Console.ResetColor();
			_isFocused = true;
            Console.SetCursorPosition(position.x + 2, position.y);
			backgroundcolor = ConsoleColor.DarkGray;
			foregroundcolor = ConsoleColor.White;
			Console.Write(text);
        }

        public void UnFocus()
		{
			_isFocused = false;
			ChangeColor();
		}

		private void RemovePlaceholderToType()
		{
			Console.SetCursorPosition(position.x + 1, position.y);
			backgroundcolor = ConsoleColor.DarkGray;
			Console.Write(" ");
			backgroundcolor = ConsoleColor.White;
			Console.Write(" ");
			backgroundcolor = ConsoleColor.DarkGray;
			for(int i=0;i<placeholder.Length-3;i++) { Console.Write(" "); }
		}

		public void ChangeText(char character)
		{
			text += character;
			Console.SetCursorPosition(position.x+1+text.Length, position.y);
			Console.Write(character);
		}

		public void ChangeText()
		{
			Console.SetCursorPosition(position.x + 2 + text.Length, position.y);
			Console.Write(" ");
		}

        public void OnHover()
		{
			Console.SetCursorPosition(position.x, position.y);
			backgroundcolor = ConsoleColor.White;
			foregroundcolor = ConsoleColor.Black;
			Console.Write("#");
            Console.SetCursorPosition(position.x + placeholder.Length + 1, position.y);
            Console.Write("#");
        }

        public void OnUnHover()
		{
			foregroundcolor = ConsoleColor.Black;
			backgroundcolor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(position.x, position.y);
            Console.Write("#");
            Console.SetCursorPosition(position.x + placeholder.Length + 1, position.y);
            Console.Write("#");
        }
    }
}
