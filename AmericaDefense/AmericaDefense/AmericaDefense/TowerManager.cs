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
    class TowerManager
    {
        private Texture2D texture;
        public Rectangle frame;
        MouseState ms;
        public static List<Tower> Towers = new List<Tower>();
        float TimeBetweenPurchases = 0.5f;
        float TimeSinceLastPurchase = 0f;
        float TimeSinceLastShot;
        public ShotManager TowerShotManager;
        Vector2 shotDirection;
        private Vector2 offScreen = new Vector2(-500, -500);
        Nazi target;
        

        public TowerManager(
            Texture2D texture,
            Rectangle frame,
            Rectangle screenBounds)
        {
            this.texture = texture;
            this.frame = frame;

            TowerShotManager = new ShotManager(
               Game1.projectiles,
               new Rectangle(18, 9, 6, 6),
               4,
               20,
               250f,
               screenBounds);
            
        }




        public void SpawnTower(TowerType type)
        {
            Tower tower = new Tower(
                new Vector2(ms.X, ms.Y) -
                    new Vector2(frame.Width / 2, frame.Height / 2),
                texture,
                frame,
                Vector2.Zero);

            switch (type)
            {
                case TowerType.RIFLE:
                    tower.GetTowerStats(TowerType.RIFLE);
                    break;

                case TowerType.MG:
                    tower.GetTowerStats(TowerType.MG);
                    break;

                case TowerType.BAZOOKA:
                    tower.GetTowerStats(TowerType.BAZOOKA);
                    break;

                case TowerType.SNIPER:
                    tower.GetTowerStats(TowerType.SNIPER);
                    break;

                case TowerType.AAGUN:
                    tower.GetTowerStats(TowerType.AAGUN);
                    break;

                case TowerType.FLAMETHROWER:
                    tower.GetTowerStats(TowerType.FLAMETHROWER);
                    break;
            }

                Towers.Add(tower);
            
        }

        
        int counter = 0;


        public void Update(GameTime gametime)
        {
            TowerShotManager.Update(gametime);
            TimeSinceLastShot += (float)gametime.ElapsedGameTime.TotalSeconds;
            TimeSinceLastPurchase += (float)gametime.ElapsedGameTime.TotalSeconds;
            ms = Mouse.GetState();
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.D1) && TimeSinceLastPurchase > TimeBetweenPurchases)
                {
                    this.frame = new Rectangle(4, 3, 33, 46);
                    SpawnTower(TowerType.RIFLE);
                    TimeSinceLastPurchase = 0f;
                    
                }

            else if (keyState.IsKeyDown(Keys.D2) && TimeSinceLastPurchase > TimeBetweenPurchases)
                {
                    this.frame = new Rectangle(45, 9, 35, 40);
                    SpawnTower(TowerType.MG);
                    TimeSinceLastPurchase = 0f;
                }

            else if (keyState.IsKeyDown(Keys.D3) && TimeSinceLastPurchase > TimeBetweenPurchases)
                {
                    this.frame = new Rectangle(87, 8, 40, 55);
                    SpawnTower(TowerType.BAZOOKA);
                    TimeSinceLastPurchase = 0f;
                }

            else if (keyState.IsKeyDown(Keys.D4) && TimeSinceLastPurchase > TimeBetweenPurchases)
                {
                    this.frame = new Rectangle(135, 7, 19, 47);
                    SpawnTower(TowerType.SNIPER);
                    TimeSinceLastPurchase = 0f;
                }

            else if (keyState.IsKeyDown(Keys.D5) && TimeSinceLastPurchase > TimeBetweenPurchases)
                {
                    this.frame = new Rectangle(161, 11, 37, 39);
                    SpawnTower(TowerType.FLAMETHROWER);
                    TimeSinceLastPurchase = 0f;
                }

            else if (keyState.IsKeyDown(Keys.D6) && TimeSinceLastPurchase > TimeBetweenPurchases)
                {
                    this.frame = new Rectangle(206, 8, 32, 48);
                    SpawnTower(TowerType.AAGUN);
                    TimeSinceLastPurchase = 0f;
                }



            for (int x = Towers.Count - 1; x >= 0; x--)
            {
                for (int y = NaziManager.Nazis.Count - 1; y >= 0; y--)
                {
                    Tower thisTower = Towers[x];
                    Nazi thisNazi = NaziManager.Nazis[y];
                    target = null;
                    
                    if (NaziManager.Nazis.Count == 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        if (target == null)
                        {
                            if (thisNazi.inRange && TimeSinceLastShot > thisTower.fireRate)
                            {
                                NaziManager.NazisinRange.Add(thisNazi);
                                target = NaziManager.NazisinRange.First();
                                Vector2 enemyPos = target.Center;
                                float distance = Vector2.Distance(target.Center, thisTower.Center);
                                float timeShot = distance / thisTower.projectileSpeed;
                                float sNazi = timeShot * target.speed;
                                Vector2 enemyDirection = target.Velocity;
                                enemyDirection.Normalize();
                                enemyPos += (sNazi * enemyDirection) / 2;

                                shotDirection = (enemyPos - thisTower.Location);
                                shotDirection.Normalize();

                                TowerShotManager.shotSpeed = thisTower.projectileSpeed;
                                TowerShotManager.FireShot(
                                    thisTower.Center,
                                    shotDirection);

                                TimeSinceLastShot = 0;


                                foreach (Sprite shot in TowerShotManager.Shots)
                                {
                                    foreach (Nazi nazi in NaziManager.NazisinRange)
                                    {
                                        if (shot.IsCircleColliding(
                                            nazi.Center,
                                            nazi.CollisionRadius) == true)
                                        {
                                            shot.Location = offScreen;
                                            nazi.health -= thisTower.damage;
                                            counter += 1;
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
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TowerShotManager.Draw(spriteBatch);
            foreach (Tower tower in Towers)
            {
                tower.Draw(spriteBatch);
            }
        }
    }
}
