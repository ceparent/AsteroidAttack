using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsteroidAttack.Game;
using AsteroidAttack.Game.Spaceships;
using AsteroidAttack.Game.Environment.Projectiles;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;

namespace AsteroidAttack.ScreenManagement.Game
{
    class PlayScreen:BaseScreen
    {
        Camera _camera;
        SpaceShip ship;
        EnvironmentManager _environmentManager;

        public override void Load()
        {
            _camera = new Camera(Vector3.Zero);

            ship = new BasicShip(Vector2.Zero);
            ship.shipDied += playerDied;

            _environmentManager = new EnvironmentManager();

        }

        public override void Update(TimeSpan totalElapsedTime)
        {

            _environmentManager.Update(ship);

            UserControls();

            ship.Update(totalElapsedTime);
            _camera.Follow(ship.Position);
        }


        KeyboardState oldKs;
        DateTime _lastAttack;
        private void UserControls()
        {
            KeyboardState ks = Keyboard.GetState();

            if (GameConsole.Opened)
                return;

            if (ks.IsKeyDown(Key.A))
                ship.Turn(TurnDirection.Left);
            if (ks.IsKeyDown(Key.D))
                ship.Turn(TurnDirection.Right);

            if (ks.IsKeyDown(Key.W))
                ship.Throttle(ThrottleChange.Up);
            if (ks.IsKeyDown(Key.S))
                ship.Throttle(ThrottleChange.Down);

            if (ks.IsKeyDown(Key.Escape))
                GameActions.OpenMenu();

            if (ks.IsKeyDown(Key.Space) && DateTime.Now - _lastAttack > TimeSpan.FromMilliseconds(GameOptions.Control_attack_delay))
            {
                _environmentManager.AddPlayerProjectile(ship.MainAttack());
                _lastAttack = DateTime.Now;
            }


            oldKs = ks;
        }


        public override void Draw()
        {
            DrawWorld();
            DrawHUD();
        }

        private void DrawHUD()
        {

        }

        private void DrawWorld()
        {
            Vector3 translation = _camera.Origin + new Vector3(GameOptions.Window_Width / 2, GameOptions.Window_Height / 2, 0);
            DrawHelper.DrawGrid(translation, new Vector2(GameOptions.Window_Width, GameOptions.Window_Height), Color.FromArgb(100, 30, 35, 35));

            GL.Translate(translation);

            _environmentManager.DrawUnderPlayer();
            ship.Draw();
            _environmentManager.DrawOVerPlayer();

            GL.Translate(-translation);
        }


        //Actions
        private void playerDied(object sender, EventArgs e)
        {
            GameActions.GameOver();
        }
    }
}
