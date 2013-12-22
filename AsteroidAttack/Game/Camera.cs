using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace AsteroidAttack.Game
{
    class Camera
    {
        private Vector3 _origin;
        public Vector3 Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }

        public Camera(Vector3 origin)
        {
            Origin = origin;
        }

        public void Follow(Vector2 follow)
        {
            Vector2 difference = new Vector2(Origin.X, Origin.Y) + follow;
            if (difference.Length > GameOptions.Camera_Tolerance)
            {
                difference = difference.Normalized() * (difference.Length - GameOptions.Camera_Tolerance);
                Origin -= new Vector3(difference.X, difference.Y, 0);
            }
        }


    }
}
