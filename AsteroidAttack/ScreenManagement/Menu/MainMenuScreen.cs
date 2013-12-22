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

namespace AsteroidAttack.ScreenManagement.Menu
{
    
    class MainMenuScreen:BaseScreen
    {

        public GameAction OpenOptions;
        public GameAction OpenHowToplay;
        public GameAction OpenHighScores;

        private static Dictionary<string, int> _textures;

        public MainMenuScreen()
            : base(false)
        {
        }

        static MainMenuScreen()
        {
            LoadTextures();
        }

        private static void LoadTextures()
        {
            int texture;
            _textures = new Dictionary<string, int>();
            texture = DrawHelper.LoadTexture("materials/menu/menu.png", null);
            _textures.Add("MenuAsteroidAttack", texture);
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
            int x = GameOptions.Window_Width / 2 - buttonWidth / 2;
            int y = GameOptions.Window_Height / 2 + (int)(buttonHeight * 2.5f);
            int offset = buttonHeight + 25;
            int cpt = 0;
            _menuButtons.Add(new MenuButton(new Rectangle(x, y + cpt++ * offset, buttonWidth, buttonHeight), "Start game", GameActions.StartGame));
            _menuButtons.Add(new MenuButton(new Rectangle(x, y + cpt++ * offset, buttonWidth, buttonHeight), "High scores", OpenHighScores));
            _menuButtons.Add(new MenuButton(new Rectangle(x, y + cpt++ * offset, buttonWidth, buttonHeight), "How to play", OpenHowToplay));
            _menuButtons.Add(new MenuButton(new Rectangle(x, y + cpt++ * offset, buttonWidth, buttonHeight), "Options", OpenOptions));
            _menuButtons.Add(new MenuButton(new Rectangle(x, y + cpt++ * offset, buttonWidth, buttonHeight), "Exit", GameActions.Exit));
        }

        MouseState oldMs;
        KeyboardState oldKs;
        public override void Update(TimeSpan totalElapsedTime)
        {
            KeyboardState ks = OpenTK.Input.Keyboard.GetState();
            MouseState ms = OpenTK.Input.Mouse.GetState();

            foreach (MenuButton b in _menuButtons)
            {
                b.Update(GameOptions.Mouse.X, GameOptions.Mouse.Y, ms.LeftButton == ButtonState.Pressed && oldMs.LeftButton == ButtonState.Released);
            }


            oldKs = ks;
            oldMs = ms;
        }
        public override void Draw()
        {
            Vector2 windowSize = new Vector2(GameOptions.Window_Width, GameOptions.Window_Height);
            DrawHelper.DrawGrid(new Vector3(windowSize.X/2,windowSize.Y/2,0), windowSize, Color.FromArgb(100, 30, 35, 35));

            //Draw image
            Vector2 imageSize = new Vector2(670, 354);
            int topOffset = 50;
            DrawHelper.DrawTexture(_textures["MenuAsteroidAttack"], new Rectangle((int)(GameOptions.Window_Width / 2 - imageSize.X / 2), topOffset, (int)imageSize.X, (int)imageSize.Y), 0, Vector2.Zero);

            foreach (MenuButton b in _menuButtons)
            {
                b.Draw();
            }
        }

    }
}
