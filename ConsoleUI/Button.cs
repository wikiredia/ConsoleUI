using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Button : IClickable, IRenderable
    {
        public Vector2 position { get; private set; }
        public string text { get; set; }
        public event MyEventHandler OnClickEvent;

        public bool _isFocused { get; private set; } = false;

        private ConsoleColor _bgcolor { get; set; } = ConsoleColor.Black;
        private ConsoleColor _fgcolor { get; set; } = ConsoleColor.White;

        public ConsoleColor foregroundcolor
        {
            get
            {
                return _fgcolor;
            }

            set
            {
                _fgcolor = value;
                Console.ForegroundColor = _fgcolor;
            }
        }

        public ConsoleColor backgroundcolor
        {
            get
            {
                return _bgcolor;
            }

            set
            {
                _bgcolor = value;
                Console.BackgroundColor = _bgcolor;
            }
        }

        
        public Button(string text, Vector2 position)
        {
            this.text = $" {text} ";
            this.position = position;
            ConsoleItems.AllButtonItems.Add(this);
            Console.ResetColor();
            Render();
        }

        public void ChangeColor(ConsoleColor ForegroundColor=ConsoleColor.White, ConsoleColor BackgroundColor=ConsoleColor.Black)
		{
			this.foregroundcolor = ForegroundColor;
			this.backgroundcolor = BackgroundColor;	
			Render();
		}


        // IRenderable
        public void Render()
        {
            Clear();

            // --------------
			//  
			// --------------
			Console.SetCursorPosition(position.x, position.y-1);
			for(int i=0;i<text.Length+2;i++) { Console.Write("-"); }
			Console.SetCursorPosition(position.x, position.y+1);
			for(int i=0;i<text.Length+2;i++) { Console.Write("-"); }

			// ----------------
			// #              #
			// ----------------
			Console.SetCursorPosition(position.x, position.y);
			Console.Write("#");
			Console.SetCursorPosition(position.x+text.Length+1, position.y);
			Console.Write("#");

			// --------------
			// # Click Here #
			// --------------
			Console.SetCursorPosition(position.x + 1, position.y);
			Console.Write(text);
        }
        public void Clear()
        {
            // --------------
			//  
			// --------------
			Console.SetCursorPosition(position.x, position.y-1);
			for(int i=0;i<text.Length+2;i++) { Console.Write(" "); }
			Console.SetCursorPosition(position.x, position.y+1);
			for(int i=0;i<text.Length+2;i++) { Console.Write(" "); }

            // --------------
			// # Click Here #
			// --------------
			Console.SetCursorPosition(position.x, position.y);
			for(int i=0;i<text.Length+2;i++) { Console.Write(" "); }
        }

        // IClickable
        public bool IsHovering(Vector2 point)
        {
            // POINTvsAABB collision check
			return (point.x >= position.x && point.x <= position.x+text.Length+1 && point.y >= position.y-1 && point.y <= position.y+1);
        }
        public void UnFocus()
		{
			_isFocused = false;
			ChangeColor();
		}
        public void OnHover()
		{
			Console.SetCursorPosition(position.x, position.y);
			backgroundcolor = ConsoleColor.White;
			foregroundcolor = ConsoleColor.Black;
			Console.Write("#");
            Console.SetCursorPosition(position.x + text.Length + 1, position.y);
            Console.Write("#");
        }
        public void OnUnHover()
		{
			foregroundcolor = ConsoleColor.Black;
			backgroundcolor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(position.x, position.y);
            Console.Write("#");
            Console.SetCursorPosition(position.x + text.Length + 1, position.y);
            Console.Write("#");
        }
		public void OnClick()
		{
			ChangeColor(ConsoleColor.White, ConsoleColor.DarkGray);
            Render();
			_isFocused = true;
            OnClickEvent?.Invoke(this, EventArgs.Empty);
        }

        public delegate void MyEventHandler(object sender, EventArgs e);
    }
}
