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

				if(AllInputFieldItems[i]._isFocused && Input.record.KeyEvent.bKeyDown)
				{
					AllInputFieldItems[i].ChangeText(Input.record.KeyEvent.UnicodeChar);
				}
			}
		}
	}
}
