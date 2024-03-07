using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleUI
{
	static class ConsoleItems
	{
		public static List<Text> AllTextItems = new List<Text>();
		public static List<InputField> AllInputFieldItems { get; set; } = new List<InputField>();
		public static List<Button> AllButtonItems { get; set; } = new List<Button>();

		private static List<IClickable> _allClickableItems { get; set; } = new List<IClickable>();
		public static List<IClickable> AllClickableItems
		{
			get
			{
				_allClickableItems.Clear();
				_allClickableItems.AddRange(AllInputFieldItems);
				_allClickableItems.AddRange(AllButtonItems);
				return _allClickableItems;
			}
		}

		public static void MainLoop()
		{
			for(int i=0;i<AllClickableItems.Count;i++)
			{
				if(Input.record.MouseEvent.dwButtonState==1)
				{
					if (AllClickableItems[i].IsHovering(new Vector2(Input.record.MouseEvent.dwMousePosition.X, Input.record.MouseEvent.dwMousePosition.Y)))
					{
						AllClickableItems[i].OnClick();
					} else
					{
						AllClickableItems[i].UnFocus();
					}
				}

				try
				{
					AllClickableItems[i].GetHashCode(); // Executing random command to check if Item is accessible
				} catch
				{
					return;
				}

				if (AllClickableItems[i].IsHovering(new Vector2(Input.record.MouseEvent.dwMousePosition.X, Input.record.MouseEvent.dwMousePosition.Y)))
				{
					AllClickableItems[i].OnHover();
				} else
				{
					AllClickableItems[i].OnUnHover();
				}
			}

			// Check for input in InputField
			for(int i=0;i<AllInputFieldItems.Count;i++)
			{
				if (AllInputFieldItems[i]._isFocused && Input.record.KeyEvent.bKeyDown)
				{
					if((Input.record.KeyEvent.wVirtualKeyCode >= 48 && Input.record.KeyEvent.wVirtualKeyCode <= 90) || (Input.record.KeyEvent.wVirtualKeyCode >= 187 && Input.record.KeyEvent.wVirtualKeyCode <= 190))
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
