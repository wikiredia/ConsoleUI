using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
	class Text
	{
		/*
         *      Text
         * 
         *      ------
         *      #Text#
         *      ------
         * 
         */
        public string text { get; private set; }
        public Vector2 position { get; private set; }
        public bool hasBorder { get; private set; }
        string isWrong = "";

        public Text(string text, Vector2 position, bool hasBorder=false)
        {
            this.text = text;
            this.position = position;
            this.hasBorder = hasBorder;
            DrawText();
        }

        public void ChangeText(string newText)
        {
            ClearText();
            text = newText;
            DrawText();
        }

        public void SetPosition(Vector2 newPosition)
        {
            ClearText();
            position = newPosition;
            DrawText();
        }

        public void ChangeBorder(bool newBorderStatus)
        {
            hasBorder = newBorderStatus;
            try
            {
                Console.SetCursorPosition(position.x - 1, position.y);
                Console.Write("#");
                Console.SetCursorPosition(position.x + text.Length, position.y);
                Console.Write("#");
                Console.SetCursorPosition(position.x - 1, position.y - 1);
                for (int i = 0; i < text.Length + 2; i++) { Console.Write("-"); }
                Console.SetCursorPosition(position.x - 1, position.y + 1);
                for (int i = 0; i < text.Length + 2; i++) { Console.Write("-"); }
            } catch
            {
                Vector2 offset = Vector2.zero;
                if(position.x-1 < 0)
                {
                    offset.x++;
                }
                if(position.x+text.Length > Console.WindowWidth)
                {
                    offset.x--;
                }

                if(position.y-1 < 0)
                {
                    offset.y++;
                }
                if(position.y+1 > Console.WindowHeight)
                {
                    offset.y--;
                }
                SetPosition(offset+position);
            }
            
        }

        public void DrawText()
        {
            Console.SetCursorPosition(position.x, position.y);
            Console.Write(text);
            if (hasBorder) { ClearBorder(); }
            ChangeBorder(hasBorder);
        }

        public void ClearText()
        {
            if(hasBorder)
            {
                ClearBorder();
            }
            Console.SetCursorPosition(position.x, position.y);
            for(int i=0;i<text.Length;i++)
            {
                Console.Write(" ");
            }
        }

        public void ClearBorder()
        {
            string previousText = text;
            try
            {
                Console.SetCursorPosition(position.x - 1, position.y);
                Console.Write(" ");
                Console.SetCursorPosition(position.x + text.Length, position.y);
                Console.Write(" ");
                Console.SetCursorPosition(position.x - 1, position.y - 1);
                for (int i = 0; i < text.Length + 2; i++) { Console.Write(" "); }
                Console.SetCursorPosition(position.x - 1, position.y + 1);
                for (int i = 0; i < text.Length + 2; i++) { Console.Write(" "); }
            }
            catch
            {
                hasBorder = false;
            }

        }

        public void ChangeColor(ConsoleColor ForegroundColor=ConsoleColor.White, ConsoleColor BackgroundColor=ConsoleColor.Black)
        {
            Console.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;
            DrawText();
            Console.ResetColor();
        }
	}
}
