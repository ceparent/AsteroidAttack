using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidAttack
{
    public delegate void GameAction();
    static class GameActions
    {
        //Main
        public static GameAction StartGame;
        public static GameAction Exit;
        public static GameAction OpenMenu;

        //Game
        public static GameAction GameOver;

    }
}
