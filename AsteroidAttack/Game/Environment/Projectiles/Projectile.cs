using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace AsteroidAttack.Game.Environment.Projectiles
{
    abstract class Projectile
    {
        public static int DistanceMax
        {
            get { return GameOptions.Window_Width; }
        }
        public abstract void Load();
        public abstract void Draw();
        public abstract void Update();

        protected float _damage;
        public float Damage
        {
            get { return _damage; }
        }

        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private float _radius = 0;
        public float Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }


    }
}
