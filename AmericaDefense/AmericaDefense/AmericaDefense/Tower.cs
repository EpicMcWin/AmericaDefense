using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace AmericaDefense
{        
    public enum TowerType
        {
            RIFLE,
            MG,
            BAZOOKA,
            SNIPER,
            FLAMETHROWER,
            AAGUN
        }

    class Tower : Sprite
    {
        Rectangle screenBounds;
        public TowerType type;
        public ShotManager TowerShotManager;
        public int range;
        public float fireRate;
        public int projectileSpeed;
        public int cost;
        public int damage;
        public bool CanShootAircraft;
        Vector2 shotDirection;
        float TimeSinceLastShot;
        private Vector2 offScreen = new Vector2(-500, -500);

        public Tower(
            Vector2 location,
            Texture2D texture,
            Rectangle initialFrame,
            Vector2 velocity)
            : base(location, texture, initialFrame, velocity)
        {
            screenBounds = new Rectangle(0, 0, 1920, 1280);

            TowerShotManager = new ShotManager(
               Game1.projectiles,
               new Rectangle(18, 9, 6, 6),
               4,
               20,
               250f,
               screenBounds);
        }
        


        

        public void Shoot(Nazi target)
        {
            if (Vector2.Distance(this.Center, target.Center) <= range)
            {
                Vector2 enemyPos = target.Center;
                float distance = Vector2.Distance(target.Center, Center);
                float timeShot = distance / projectileSpeed;
                float distNazi = timeShot * target.speed;
                Vector2 enemyDirection = target.Velocity;
                enemyDirection.Normalize();
                enemyPos += (distNazi * enemyDirection) / 2;

                shotDirection = (enemyPos - Location);
                shotDirection.Normalize();

                TimeSinceLastShot = 0;
                TowerShotManager.shotSpeed = projectileSpeed;
                TowerShotManager.FireShot(
                    Center,
                    shotDirection);

                foreach (Sprite shot in TowerShotManager.Shots)
                {
                    foreach (Nazi nazi in NaziManager.NazisinRange)
                    {
                        if (shot.IsCircleColliding(
                            nazi.Center,
                            nazi.CollisionRadius) == true)
                        {
                            shot.Location = offScreen;
                            shot.Velocity = new Vector2(0, 0);
                            nazi.health -= damage;
                            //playerManager.PlayerScore += NaziPointValue;
                        }
                        else
                        {
                            //lol i dunno
                        }
                    }
                }
            }
        }

        public virtual void GetTowerStats(TowerType type)
        {
            switch (type)
            {
                case TowerType.RIFLE:
                    cost = 100;
                    fireRate = 1;
                    damage = 40;
                    range = 250;
                    projectileSpeed = 250;
                    CanShootAircraft = false;
                    break;

                case TowerType.MG:
                    cost = 300;
                    fireRate = .2f;
                    damage = 15;
                    range = 250;
                    projectileSpeed = 250;
                    CanShootAircraft = false;
                    break;

                case TowerType.BAZOOKA:
                    cost = 400;
                    fireRate = 2;
                    damage = 250;
                    range = 250;
                    projectileSpeed = 150;
                    CanShootAircraft = false;
                    break;

                case TowerType.SNIPER:
                    cost = 300;
                    fireRate = 3;
                    damage = 150;
                    range = 750;
                    projectileSpeed = 750;
                    CanShootAircraft = false;
                    break;

                case TowerType.AAGUN:
                    cost = 300;
                    fireRate = .1f;
                    damage = 10;
                    range = 350;
                    projectileSpeed = 400;
                    CanShootAircraft = true;
                    break;

                case TowerType.FLAMETHROWER:
                    cost = 500;
                    fireRate = .1f;
                    damage = 10;
                    range = 100;
                    projectileSpeed = 100;
                    CanShootAircraft = false;
                    break;
            }
        }
        public void Update(GameTime gametime)
        {
            TimeSinceLastShot += (float)gametime.ElapsedGameTime.TotalSeconds;
            TowerShotManager.Update(gametime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TowerShotManager.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
