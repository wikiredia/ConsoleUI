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
				if(Input.MouseButtonState==1)
				{
					if (AllInputFieldItems[i].IsHovering(Input.MousePos))
					{
						AllInputFieldItems[i].OnClick();
					} else
					{
						AllInputFieldItems[i].UnFocus();
					}
				}
			}
		}
	}
}
