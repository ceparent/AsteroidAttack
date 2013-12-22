using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;
using AsteroidAttack.ScreenManagement.Controls;
using QuickFont;

namespace AsteroidAttack.ScreenManagement.Menu
{
    class HowToScreen:BaseScreen
    {
        public GameAction goBack;

        public HowToScreen()
            : base(false)
        {
        }

        public override void Load()
        {
            LoadButtons();
        }

        List<MenuButton> _menuButtons;
        private void LoadButtons()
        {
            _menuButtons = new List<MenuButton>();

            int buttonWidth = 200;
            int buttonHeight = 25;

            _menuButtons.Add(new MenuButton(new Rectangle(GameOptions.Window_Width / 2 - buttonWidth / 2, GameOptions.Window_Height * 5 / 6, buttonWidth, buttonHeight), "Back to menu", goBack));
        }

        MouseState oldMs;
        KeyboardState oldKs;
        public override void Update(TimeSpan totalElapsedTime)
        {
            KeyboardState ks = OpenTK.Input.Keyboard.GetState();
            MouseState ms = OpenTK.Input.Mouse.GetState();

            foreach (MenuButton b in _menuButtons)
            {
                b.Update(GameOptions.Mouse.X,GameOptions.Mouse.Y, ms.LeftButton == ButtonState.Pressed && oldMs.LeftButton == ButtonState.Released);
            }


            oldKs = ks;
            oldMs = ms;
        }

        static Font bigfont = new Font(FontFamily.GenericSansSerif, 66.0f, FontStyle.Bold);
        static QFont qfont = new QFont(bigfont);
        public override void Draw()
        {
            Vector2 windowSize = new Vector2(GameOptions.Window_Width, GameOptions.Window_Height);
            DrawHelper.DrawGrid(new Vector3(windowSize.X / 2, windowSize.Y / 2, 0), windowSize, Color.FromArgb(100, 30, 35, 35));

            string message = "How to play";
            DrawHelper.DrawString(message, qfont, Color.DarkRed, new Vector2(GameOptions.Window_Width / 2, GameOptions.Window_Height / 6), true);

            foreach (MenuButton b in _menuButtons)
            {
                b.Draw();
            }
        }
    }
}
