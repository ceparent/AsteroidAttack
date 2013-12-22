using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsteroidAttack.Game.Environment;
using AsteroidAttack.Game.Environment.Projectiles;
using AsteroidAttack.Game.Spaceships;
using OpenTK;


namespace AsteroidAttack.Game
{
    class EnvironmentManager
    {

        public EnvironmentManager()
        {
            Load();
        }

        List<Projectile> _playerProjectiles;
        List<Asteroid> _asteroids;
        public void Load()
        {
            _asteroids = new List<Asteroid>();
            _playerProjectiles = new List<Projectile>();
        }


        public void Update(SpaceShip player)
        {
            UpdateAsteroids(player);
            UpdateProjectiles(player);

        }
        private void UpdateProjectiles(SpaceShip player)
        {
            List<Projectile> templist = new List<Projectile>();
            foreach (Projectile p in _playerProjectiles)
            {
                p.Update();

                bool isNear = (p.Position - player.Position).Length < Projectile.DistanceMax;
                if (isNear)
                    templist.Add(p);
            }
            _playerProjectiles = templist;
        }

        DateTime _lastAsteroidAdded;
        private void UpdateAsteroids(SpaceShip player)
        {
            List<Asteroid> tempListAsteroid = new List<Asteroid>();
            foreach (Asteroid a in _asteroids)
            {
                a.Update();

                if (!player.Invincible)
                {
                    if (a.IsColliding(player.Position,player.Radius) && !player.Invincible)
                        player.Hit();
                }

                // Projectiles
                CheckPlayerProjectiles(a);

                bool isNear = (a.Position - player.Position).Length < Asteroid.DistanceMax;

                if (isNear && a.NB_HITS < Asteroid.NB_MAX_HITS)
                    tempListAsteroid.Add(a);
            }

            _asteroids = tempListAsteroid;

            if (_asteroids.Count < GameOptions.Environment_min_asteroids && DateTime.Now - _lastAsteroidAdded > TimeSpan.FromMilliseconds(200))
            {
                float astDistance = Asteroid.DistanceMax  * 3 / 4;
                float astAngle = (float)GameOptions.Random.NextDouble() * MathHelper.TwoPi;
                Vector2 astPosition = new Vector2(player.Position.X + (float)Math.Sin(astAngle) * astDistance, player.Position.Y - (float)Math.Cos(astAngle) * astDistance);
                _asteroids.Add(new Asteroid(astPosition));
                _lastAsteroidAdded = DateTime.Now;
            }
        }


        private void CheckPlayerProjectiles(Asteroid a)
        {
            List<Projectile> tempProjectiles = new List<Projectile>(); 
            foreach (Projectile p in _playerProjectiles)
            {
                

                bool collides = a.IsColliding(p.Position, p.Radius);


                if (collides)
                    a.Hit();
                else
                    tempProjectiles.Add(p);
            }
            _playerProjectiles = tempProjectiles;
        }

        public void AddPlayerProjectile(Projectile p)
        {
            _playerProjectiles.Add(p);
        }

        public void DrawUnderPlayer()
        {
            DrawProjectiles();
        }
        public void DrawOVerPlayer()
        {
            DrawAsteroids();
        }

        private void DrawProjectiles()
        {
            foreach (Projectile p in _playerProjectiles)
            {
                p.Draw();
            }
        }

        private void DrawAsteroids()
        {
            foreach (Asteroid a in _asteroids)
            {
                a.Draw();
            }
        }


    }
}
