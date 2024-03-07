using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
	abstract class Clickable : IClickable
	{
		public virtual string text { get; set; } = "";

		public abstract bool IsHovering(Vector2 point);
		public abstract void OnClick();
		public abstract void UnFocus();
		public abstract void OnHover();
		public abstract void OnUnHover();
	}
}
