using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.Drawing;
using QuickFont;

namespace AsteroidAttack.ScreenManagement.Controls
{
    class MenuButton
    {
        private Rectangle _rectangle;
        public Rectangle Rectangle
        {
            get { return _rectangle; }
        }
        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        public Color Color
        {
            get
            {
                if (!hover)
                    return Color.FromArgb(100, 30, 35, 35);
                else
                    return Color.DarkRed;
            }
        }
        public Color TextColor
        {
            get
            {
                if (hover)
                    return Color.Black;
                else
                    return Color.DarkRed;
            }
        }

        GameAction _action;
        public MenuButton(Rectangle rectangle, string text, GameAction action)
        {
            _rectangle = rectangle;
            _text = text;
            _action += action;
        }

        static Font font = new Font(FontFamily.GenericSansSerif, 16.0f, FontStyle.Bold);
        static QFont qfont = new QFont(font);
        public void Draw()
        {
            DrawHelper.DrawRectangle(Rectangle, Color, true);
            DrawHelper.DrawString(Text, qfont, TextColor, new Vector2((Rectangle.Left + Rectangle.Right) / 2, (Rectangle.Top + Rectangle.Bottom) / 2), true);
        }

        private bool hover;
        public void Update(int mouseX, int mouseY, bool mousePress)
        {
            hover = Rectangle.Contains(mouseX, mouseY);
            if (_action != null && hover && mousePress)
                _action();
        }


    }
}
