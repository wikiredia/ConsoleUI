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
        public void OnClick()
        {

        }

        public bool IsHovering(Vector2 point)
        {
            return false;
        }

        public void UnFocus()
        {

        }

        public void OnHover()
        {

        }

        public void OnUnHover()
        {

        }

        public void Render()
        {

        }

        public void Clear()
        {

        }
    }
}
