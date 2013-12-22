using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.Drawing;
using AsteroidAttack.Game.Environment.Projectiles;

namespace AsteroidAttack.Game.Spaceships
{
    public enum ThrottleChange { Up, Down }
    public enum TurnDirection { Left, Right }

    abstract class SpaceShip
    {
        public event EventHandler shipDied;

        public abstract Projectile MainAttack();

        public SpaceShip(Vector2 position)
        {
            Position = position;
            Angle = 0;
            Speed = 0;

            Invincible = false;
            CircleVisible = true;
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

        private float _maxSpeed;
        protected float MaxSpeed
        {
            get { return _maxSpeed; }
            set { _maxSpeed = value; }
        }

        private float _maxHealth;
        public float MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }

        private float _throttleSpeed;
        public float ThrottleSpeed
        {
            get { return _throttleSpeed; }
            set { _throttleSpeed = value; }
        }

        private float _turnSpeed;
        public float TurnSpeed
        {
            get { return _turnSpeed; }
            set { _turnSpeed = value; }
        }

        private float _speed;
        public float Speed
        {
            get { return _speed; }
            set 
            {

                if (value > MaxSpeed)
                    _speed = MaxSpeed;
                else if (value < 0)
                    _speed = 0;
                else
                    _speed = value; 
            }
        }

        private float _radius;
        public float Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        private float _health;
        public float Health
        {
            get { return _health; }
            set 
            {
                if (Invincible)
                    return;

                if (value <= 0)
                {

                    // TODO: add death effect (explosion)
                    if(shipDied != null)
                        shipDied(this, EventArgs.Empty);

                    _health = 0;
                }
                else if (value > MaxHealth)
                    _health = MaxHealth;
                else
                    _health = value; 
            }
        }

        public void Hit()
        {
            Health--;
            Invincible = true;
            lastHit = DateTime.Now;
        }

        DateTime lastHit;
        DateTime lastFlick;
        private void CircleFlick()
        {
            if (Invincible && DateTime.Now - lastHit > TimeSpan.FromMilliseconds(GameOptions.Ship_invincibility_time))
            {
                Invincible = false;
                CircleVisible = true;
            }
            else if (Invincible && DateTime.Now - lastFlick > TimeSpan.FromMilliseconds(GameOptions.Ship_circle_flick))
            {
                CircleVisible = !CircleVisible;
                lastFlick = DateTime.Now;
            }   
        }

        private bool _circleVisible;
        public bool CircleVisible
        {
            get 
            {
                if (Health <= 0)
                    _circleVisible = false;
                return _circleVisible; 
            }
            set { _circleVisible = value; }
        }


        private bool _invincible;
        public bool Invincible
        {
            get { return _invincible; }
            set { _invincible = value; }
        }

        Color[] _Colors = { Color.Black, Color.DarkRed, Color.DarkOrange, Color.DarkGreen, Color.DarkBlue };
        public Color Color
        {
            get { return _Colors[(int)Health]; }
        }

        public virtual void Draw()
        {
            if(CircleVisible)
                DrawHelper.DrawCircle(60, Position, Color, false);

        }

        public virtual void Update(TimeSpan totalElapsedTime)
        {
            Position += new Vector2((float)Math.Sin(Angle), (float)-Math.Cos(Angle)) * Speed;

            CircleFlick();
        }

        public void Throttle(ThrottleChange change)
        {
            switch (change)
            {
                case ThrottleChange.Up:
                    Speed += ThrottleSpeed;
                    break;
                case ThrottleChange.Down:
                    Speed -= ThrottleSpeed;
                    break;
            }
        }

        public void Turn(TurnDirection direction)
        {
            switch (direction)
            {
                case TurnDirection.Left:
                    Angle -= TurnSpeed;
                    break;
                case TurnDirection.Right:
                    Angle += TurnSpeed;
                    break;
            }
        }


        

    }
}
