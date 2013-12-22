using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using QuickFont;

namespace AsteroidAttack.ScreenManagement.Game
{
    class GameOverScreen:BaseScreen
    {
        public override void Load()
        {
            font = new Font(FontFamily.GenericSansSerif, 56.0f);
            qfont = new QFont(font);
        }

        public override void Update(TimeSpan totalElapsedTime)
        {
            
        }

        Font font;
        QFont qfont;
        public override void Draw()
        {
            DrawHelper.DrawString("GAME OVER", qfont, Color.DarkRed, new Vector2(GameOptions.Window_Width / 2, GameOptions.Window_Height / 2), true);
        }
    }
}
