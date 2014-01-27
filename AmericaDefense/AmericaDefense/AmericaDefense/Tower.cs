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
            SUPERTOWER
        }

    class Tower : Sprite
    {
        Rectangle screenBounds;
        public ShotManager TowerShotManager;
        public int range;
        public float fireRate;
        public int projectileSpeed;
        public int cost;
        public int damage;
        public bool CanShootAircraft;
        Vector2 shotDirection;
        float TimeSinceLastShot;
        public static Queue<Nazi> naziTargets = new Queue<Nazi>();
        private Vector2 offScreen = new Vector2(-500, -500);
        public static int count = 0;

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
               6,
               250f,
               screenBounds);
        }
        


        

        public void Shoot(Nazi target)
        {
            if (Vector2.Distance(this.Center, target.Center) <= range && TimeSinceLastShot > fireRate)
            {
                Vector2 enemyPos = target.Center;
                float distance = Vector2.Distance(target.Center, this.Center);
                float timeShot = distance / this.projectileSpeed;
                float distNazi = timeShot * target.speed;
                Vector2 enemyDirection = target.Velocity;
                enemyDirection.Normalize();
                enemyPos += (distNazi * enemyDirection) / 2;

                shotDirection = (enemyPos - Location);
                shotDirection.Normalize();

                TimeSinceLastShot = 0;
                TowerShotManager.shotSpeed = projectileSpeed;
                TowerShotManager.FireShot(
                    this.Center,
                    shotDirection);

                Game1.gunshot.Play();
                
            }
        }

        public virtual void GetTowerStats(TowerType type)
        {
            switch (type)
            {
                case TowerType.RIFLE:
                    cost = 50;
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
                    fireRate = 2.2f;
                    damage = 150;
                    range = 750;
                    projectileSpeed = 750;
                    CanShootAircraft = false;
                    break;

                case TowerType.FLAMETHROWER:
                    cost = 500;
                    fireRate = .1f;
                    damage = 10;
                    range = 100;
                    projectileSpeed = 100;
                    CanShootAircraft = false;
                    break;

                case TowerType.SUPERTOWER:
                    cost = 5000;
                    fireRate = .1f;
                    damage = 30;
                    range = 400;
                    projectileSpeed = 404; //projectile speed not found
                    CanShootAircraft = true;
                    break;
            }
        }

        public override void Update(GameTime gametime)
        {
            
            TimeSinceLastShot += (float)gametime.ElapsedGameTime.TotalSeconds;
            TowerShotManager.Update(gametime);

            foreach (Sprite shot in TowerShotManager.Shots)
            {
                foreach (Nazi nazi in NaziManager.Nazis)
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
                        //_         _
                        // \_(o_o)_/    LOL I DUNNO
                        //     |   
                        //     |
                        //    / \
                    }
                }


            }

            for (int x = NaziManager.Nazis.Count - 1; x >= 0; x--)
            {
                if (NaziManager.Nazis[x].alreadyQueued == false && Vector2.Distance(NaziManager.Nazis[x].Center, this.Center) <= this.range)
                {
                    naziTargets.Enqueue(NaziManager.Nazis[x]);
                    NaziManager.Nazis[x].alreadyQueued = true;
                    
                }
                else
                {
                        //_         _
                        // \_(o_o)_/    LOL I DUNNO
                        //     |   
                        //     |
                        //    / \
                }


                

                if (naziTargets.Count > 0)
                {
                    if (Vector2.Distance(naziTargets.Peek().Center, this.Center) > this.range || naziTargets.Peek().health <= 0)
                    {
                        naziTargets.Peek().alreadyQueued = false;
                        naziTargets.Dequeue();
                        count += 1;
                    }
                }
            }
        }
        

        public override void Draw(SpriteBatch spriteBatch)
        {
            TowerShotManager.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
