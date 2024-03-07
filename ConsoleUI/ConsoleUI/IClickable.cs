using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
	interface IClickable
	{
		bool IsHovering(Vector2 point);
		void OnClick();
		void UnFocus();
		void OnHover();
		void OnUnHover();
	}
}
