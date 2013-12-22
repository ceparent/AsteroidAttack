using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace AsteroidAttack.Game.Environment
{
    class Asteroid
    {
        private const string TEXTURE_PATH = "materials/asteroid/";
        public const int MAX_MOVEMENT_SPEED = 3;
        public const int MAX_WIDTH = 150;
        public const int MIN_WIDTH = 50;
        public const int NB_MAX_HITS = 2;

        public static int DistanceMax
        {
            get { return GameOptions.Window_Width; }
        }

        static Random rand;
        static int[] textures;
        static Asteroid()
        {
            rand = new Random();
            textures = new int[9];
            for (int i = 0; i < textures.Length; i++)
            {
                textures[i] = DrawHelper.LoadTexture(TEXTURE_PATH + "ast_" + (i + 1) + ".png", null);
            }
        }

        private int _texture;
        public int Texture
        {
            get { return _texture; }
        }

        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private float _angle;
        public float Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        private Vector2 _movement;
        public Vector2 Movement
        {
            get { return _movement; }
            set { _movement = value; }
        }

        private float _radius;
        public float Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        private Color _color;
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public Asteroid(Vector2 position, float angle = 0)
        {
            _position = position;
            _angle = angle;
            do
            {
                _movement = new Vector2(rand.Next(-MAX_MOVEMENT_SPEED, MAX_MOVEMENT_SPEED), rand.Next(-MAX_MOVEMENT_SPEED, MAX_MOVEMENT_SPEED));
            } while (_movement == Vector2.Zero);
            _texture = textures[rand.Next(1, 9)];
            _color = Color.DarkGreen;
            _radius = rand.Next(MIN_WIDTH, MAX_WIDTH);
        }

        public void Update()
        {
            Position += Movement;
        }

        public void Draw()
        {
            Rectangle rectangle = new Rectangle((int)(Position.X - Radius / 2), (int)(Position.Y - Radius / 2), (int)Radius, (int)Radius);
            DrawHelper.DrawTexture(Texture, rectangle, Angle, Vector2.Zero);

            GL.LineWidth(1);
            DrawHelper.DrawCircle(Radius * 0.55f, Position, Color, false);
        }

        public bool IsColliding(Vector2 position)
        {
            return Math.Pow(Position.X - position.X, 2) + Math.Pow(Position.Y - position.Y, 2) < Math.Pow(Radius * 0.55f, 2);
        }
        public bool IsColliding(Vector2 position, float radius)
        {
            return Math.Pow(Position.X - position.X, 2) + Math.Pow(Position.Y - position.Y, 2) < Math.Pow((Radius * 0.55f) + radius, 2);
        }


        private int nb_hits = 0;
        public int NB_HITS
        {
            get { return nb_hits; }
        }


        public void Hit()
        {
            nb_hits++;
            Color = Color.DarkRed;
        }

    }
}
