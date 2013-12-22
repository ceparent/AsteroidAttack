using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace AsteroidAttack.Game.Environment.Projectiles
{
    class Laser:Projectile
    {
        public const float LASER_LENGTH = 25;
        public const float SPEED = 1f;

        private float _angle;
        public float Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        private const int DAMAGE = 1;

        public Laser(Vector2 position, float angle, Color color)
        {
            Position = position;
            Color = color;
            Angle = angle;
        }

        public override void Load()
        {
            _damage = DAMAGE;
            Radius = 10;
        }

        private Color _color;
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public override void Draw()
        {
            GL.Color3(Color);
            GL.LineWidth(5);

            GL.Begin(PrimitiveType.Lines);

            GL.Vertex2(Position);
            GL.Vertex2(Position.X + Math.Sin(Angle) * LASER_LENGTH, Position.Y - Math.Cos(Angle) * LASER_LENGTH);

            GL.End();

        }

        public override void Update()
        {
            Position += new Vector2((float)Math.Sin(Angle) * LASER_LENGTH, -(float)Math.Cos(Angle) * LASER_LENGTH);
        }

    }
}
