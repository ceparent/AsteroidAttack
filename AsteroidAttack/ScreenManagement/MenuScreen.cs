using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsteroidAttack.ScreenManagement.Menu;

namespace AsteroidAttack.ScreenManagement
{

    class MenuScreen:BaseScreen
    {

        BaseScreen _lastScreen;
        BaseScreen _actualScreen;
        public override void Load()
        {
            OpenMainMenu();
        }

        public override void Update(TimeSpan totalElapsedTime)
        {
            _actualScreen.Update(totalElapsedTime);
        }

        public override void Draw()
        {
            _actualScreen.Draw();   
        }

        public void OpenMainMenu()
        {
            _lastScreen = _actualScreen;
            MainMenuScreen menu = new MainMenuScreen();
            menu.OpenOptions = OpenOptions;
            menu.OpenHowToplay = OpenHowToPlay;
            menu.OpenHighScores = OpenHighScores;
            menu.Load();
            _actualScreen = menu;
        }

        public void OpenOptions()
        {
            _lastScreen = _actualScreen;
            OptionScreen options = new OptionScreen();
            options.goBack = GoBack;
            options.Load();
            _actualScreen = options;
        }
        public void OpenHighScores()
        {
            _lastScreen = _actualScreen;
            HighScoresScreen options = new HighScoresScreen();
            options.goBack = GoBack;
            options.Load();
            _actualScreen = options;
        }
        public void OpenHowToPlay()
        {
            _lastScreen = _actualScreen;
            HowToScreen options = new HowToScreen();
            options.goBack = GoBack;
            options.Load();
            _actualScreen = options;
        }

        public void GoBack()
        {
            BaseScreen temp = _lastScreen;
            _lastScreen = _actualScreen;
            _actualScreen = temp;
        }




    }
}
