using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleUI
{
	static class ConsoleItems
	{
		public static List<Text> AllTextItems = new List<Text>();
		public static List<InputField> AllInputFieldItems = new List<InputField>();

		public static void MainLoop()
		{
			for(int i=0;i<AllInputFieldItems.Count;i++)
			{
				if(Input.record.MouseEvent.dwButtonState==1)
				{
					if (AllInputFieldItems[i].IsHovering(new Vector2(Input.record.MouseEvent.dwMousePosition.X, Input.record.MouseEvent.dwMousePosition.Y)))
					{
						AllInputFieldItems[i].OnClick();
					} else
					{
						AllInputFieldItems[i].UnFocus();
					}
				}

                if (AllInputFieldItems[i].IsHovering(new Vector2(Input.record.MouseEvent.dwMousePosition.X, Input.record.MouseEvent.dwMousePosition.Y)))
				{
					AllInputFieldItems[i].OnHover();
				} else
				{
					AllInputFieldItems[i].OnUnHover();
				}


                if (AllInputFieldItems[i]._isFocused && Input.record.KeyEvent.bKeyDown)
				{
					if(Input.record.KeyEvent.wVirtualKeyCode >= 65 && Input.record.KeyEvent.wVirtualKeyCode <= 90)
					{
						AllInputFieldItems[i].ChangeText(Input.record.KeyEvent.UnicodeChar);
					} else if(Input.record.KeyEvent.wVirtualKeyCode == 32)
					{
						// Space
						AllInputFieldItems[i].ChangeText(' ');
					} else if(Input.record.KeyEvent.wVirtualKeyCode == 8)
					{
                        // Backspace
						try
						{
							AllInputFieldItems[i].text = AllInputFieldItems[i].text.Remove(AllInputFieldItems[i].text.Length - 1, 1);
							AllInputFieldItems[i].ChangeText();
						} catch { }
                    }
                }
			}
		}
	}
}
