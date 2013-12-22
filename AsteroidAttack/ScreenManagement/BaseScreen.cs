using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidAttack.ScreenManagement
{
    abstract class BaseScreen
    {
        public BaseScreen(bool autoload = true)
        {
            if(autoload)
                Load();
        }
        public abstract void Load();
        public abstract void Update(TimeSpan totalElapsedTime);
        public abstract void Draw();

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}
