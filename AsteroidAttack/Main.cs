using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using AsteroidAttack.ScreenManagement;

namespace AsteroidAttack
{
    class Main : GameWindow
    {
        BaseScreen _ActualScreen;
        public Main()
            : base(GameOptions.Window_Width, GameOptions.Window_Height)
        {
            Title = "Asteroid Attack";
        }

        /// <summary>
        /// Executes on the Game's first load
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            WindowBorder = OpenTK.WindowBorder.Fixed;

            GameOptions.Mouse = Mouse;

            this.KeyPress += GameConsole.KeyPressEvent;
            Keyboard.KeyDown += GameConsole.KeyDownEvent;

            SetGameActions();

            SetupViewPort();
            OpenMenu();
        }

        /// <summary>
        /// Sets the game actions
        /// </summary>
        private void SetGameActions()
        {
            GameActions.StartGame = StartGame;
            GameActions.Exit = Exit;
            GameActions.OpenMenu = OpenMenu;
        }
        /// <summary>
        /// Sets up the 2D viewport
        /// </summary>
        private void SetupViewPort()
        {
            WindowState = GameOptions.Window_state;

            GL.ClearColor(Color.Black);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.Viewport(0, 0, GameOptions.Window_Width, GameOptions.Window_Height);

            float nearClip = 0f;
            float farClip = 1000.0f;

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.Ortho(0, GameOptions.Window_Width, GameOptions.Window_Height, 0, nearClip, farClip);
        }
        

        TimeSpan _gameTime;
        KeyboardState oldKs;
        /// <summary>
        /// Game logic here
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            _ActualScreen.Update(_gameTime);

            KeyboardState ks = OpenTK.Input.Keyboard.GetState();
            // ALT + F4
            if ((ks.IsKeyDown(Key.AltLeft) || ks.IsKeyDown(Key.AltRight)) && ks.IsKeyDown(Key.F4))
                GameActions.Exit();
            // Console
            else if (ks.IsKeyDown(Key.Tilde) && oldKs.IsKeyUp(Key.Tilde))
                GameConsole.Opened = !GameConsole.Opened;



            oldKs = ks;
        }
        
        /// <summary>
        /// Game drawing here
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            _ActualScreen.Draw();
            if(GameConsole.Opened)
                GameConsole.Draw();

            SwapBuffers();
        }

        
        /// <summary>
        /// Opens the menu
        /// </summary>
        private void OpenMenu()
        {
            MenuScreen menu = new MenuScreen();

            _ActualScreen = menu;
            _gameTime = TimeSpan.Zero;
        }
        /// <summary>
        /// Starts a game
        /// </summary>
        private void StartGame()
        {
            GameScreen game = new GameScreen();

            _ActualScreen = game;
            _gameTime = TimeSpan.Zero;
        }
    }
}
