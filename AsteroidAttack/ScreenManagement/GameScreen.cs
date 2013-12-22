using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsteroidAttack.ScreenManagement.Game;

namespace AsteroidAttack.ScreenManagement
{
    public enum GameState { Playing, Pause, GameOver }
    class GameScreen:BaseScreen
    {
        GameState _state;
        BaseScreen _actualScreen;
        PlayScreen _playScreen;
        public override void Load()
        {
            //GameActions
            GameActions.GameOver = GameOver;

            NewGame();
        }

        private void NewGame()
        {
            _playScreen = new PlayScreen();
            _actualScreen = _playScreen;
            _state = GameState.Playing;
        }

        public override void Update(TimeSpan totalElapsedTime)
        {
            _actualScreen.Update(totalElapsedTime);
        }

        public override void Draw()
        {
            switch (_state)
            {
                case GameState.Pause:
                case GameState.GameOver:
                    _playScreen.Draw();
                    break;
            }

            _actualScreen.Draw();
        }

        private void GameOver()
        {
            _state = GameState.GameOver;
            _actualScreen = new GameOverScreen();

        }
    }
}
