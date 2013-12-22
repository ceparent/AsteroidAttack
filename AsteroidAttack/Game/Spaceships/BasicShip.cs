using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using AsteroidAttack.Game.Environment.Projectiles;

namespace AsteroidAttack.Game.Spaceships
{
    class BasicShip : SpaceShip
    {
        private const int FIRESIZE_FULL = 40;

        private static int _texture;
        private static int _fireTexture;

        static BasicShip()
        {
            _texture = DrawHelper.LoadTexture("materials/ship/ship.png", null);
            _fireTexture = DrawHelper.LoadTexture("materials/ship/fire.png", null);
        }

        public BasicShip(Vector2 position)
            :base(position)
        {
            MaxSpeed = 10;
            MaxHealth = 4;
            Health = MaxHealth;
            ThrottleSpeed = 0.1f;
            TurnSpeed = 0.025f;
            Radius = 57;      
        }

        public override void Draw()
        {
            base.Draw();

            Vector2 fireSize = new Vector2(FIRESIZE_FULL, FIRESIZE_FULL * Speed / MaxSpeed);

            float radiusOffset = -2;

            Rectangle fireRec = new Rectangle((int)(Position.X - fireSize.X / 2), (int)(Position.Y + fireSize.Y / 2 - radiusOffset), (int)fireSize.X, (int)fireSize.Y);
            DrawHelper.DrawTexture(_fireTexture, fireRec, Angle, new Vector2(0, -fireSize.Y + radiusOffset));

            Rectangle rectangle = new Rectangle((int)(Position.X - Radius / 2), (int)(Position.Y - Radius / 2), (int)Radius, (int)Radius);
            DrawHelper.DrawTexture(_texture, rectangle, Angle, Vector2.Zero);

        }

        public override void Update(TimeSpan totalElapsedTime)
        {
            base.Update(totalElapsedTime);


        }


        public override Environment.Projectiles.Projectile MainAttack()
        {
            return new Laser(Position, Angle,Color);
        }
    }
}
