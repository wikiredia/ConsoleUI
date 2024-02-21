using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
	class Text : IRenderable
	{
		/*
                 Text            | No Border

                ------
                #Text#           | Border
                ------
         */

        public string text { get; private set; }
        public Vector2 position { get; private set; }
        public bool hasBorder { get; private set; }

        public Text(string text, Vector2 position, bool hasBorder=false)
        {
            this.hasBorder = hasBorder;
            this.text = this.hasBorder ? $" {text} " : text;
            this.position = position;
            ConsoleItems.AllTextItems.Add(this);
            Render();
        }

        public void ChangeText(string newText)
        {
            Clear();
            this.text = newText;
            Render();
        }

        public void SetPosition(Vector2 newPosition)
        {
            Clear();
            position = newPosition;
            Render();
        }

        public void ChangeBorder(bool newBorderStatus)
        {
            if(!hasBorder) { ClearBorder(); return; }
            // Drawing the border around the text here
            try
            {
                Console.SetCursorPosition(position.x - 1, position.y);
                Console.Write("#");
            } catch { }
            try
            {
                Console.SetCursorPosition(position.x + text.Length, position.y);
                Console.Write("#");
            } catch { }
            try
            {
                Console.SetCursorPosition(position.x - 1, position.y - 1);
                for (int i = 0; i < text.Length + 2; i++) { Console.Write("-"); }
            } catch { }
            try
            {
                Console.SetCursorPosition(position.x - 1, position.y + 1);
                for (int i = 0; i < text.Length + 2; i++) { Console.Write("-"); }
            } catch { }
        }

        public void Render()
        {
            Clear();
            Console.SetCursorPosition(position.x, position.y);
            Console.Write(text);
            ChangeBorder(hasBorder);
        }

        public void Clear()
        {
            ClearBorder();
            Console.SetCursorPosition(position.x, position.y);
            for(int i=0;i<text.Length;i++)
            {
                Console.Write(" ");
            }
        }

        private void ClearBorder()
        {
            try
            {
                Console.SetCursorPosition(position.x - 1, position.y);
                Console.Write(" ");
            } catch { }

            try
            {
                Console.SetCursorPosition(position.x + text.Length, position.y);
                Console.Write(" ");
            } catch { }

            try
            {
                Console.SetCursorPosition(position.x - 1, position.y - 1);
                for (int i = 0; i < text.Length + 2; i++) { Console.Write(" "); }
            } catch { }

            try
            {
                Console.SetCursorPosition(position.x - 1, position.y + 1);
                for (int i = 0; i < text.Length + 2; i++) { Console.Write(" "); }
            } catch { }
        }

        public void ChangeColor(ConsoleColor ForegroundColor=ConsoleColor.White, ConsoleColor BackgroundColor=ConsoleColor.Black)
        {
            Console.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;
            Render();
            Console.ResetColor();
        }
	}
}
