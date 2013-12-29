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
        private Texture2D projectileTextures;
        public Rectangle frame;
        MouseState ms;
        public static List<Tower> Towers = new List<Tower>();
        float TimeBetweenPurchases = 0.5f;
        float TimeSinceLastPurchase = 0f;
        public ShotManager TowerShotManager;
        Vector2 shotDirection;
        

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
               2,
               250f,
               screenBounds);
            
        }

        


        public void SpawnTower()
        {
            Tower tower = new Tower(
                new Vector2(ms.X, ms.Y) -
                    new Vector2(frame.Width / 2, frame.Height / 2),
                texture,
                frame,
                Vector2.Zero);

            Towers.Add(tower);
        }

        



        public void Update(GameTime gametime)
        {
            TowerShotManager.Update(gametime);

            TimeSinceLastPurchase += (float)gametime.ElapsedGameTime.TotalSeconds;
            ms = Mouse.GetState();
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.D1) && TimeSinceLastPurchase > TimeBetweenPurchases)
                {
                    this.frame = new Rectangle(4, 3, 33, 46);
                    SpawnTower();
                    TimeSinceLastPurchase = 0f;
                    
                }

            else if (keyState.IsKeyDown(Keys.D2) && TimeSinceLastPurchase > TimeBetweenPurchases)
                {
                    this.frame = new Rectangle(45, 9, 35, 40);
                   
                    SpawnTower();
                    TimeSinceLastPurchase = 0f;
                }

            else if (keyState.IsKeyDown(Keys.D3) && TimeSinceLastPurchase > TimeBetweenPurchases)
                {
                    this.frame = new Rectangle(87, 8, 40, 55);
                    
                    SpawnTower();
                    TimeSinceLastPurchase = 0f;
                }

            else if (keyState.IsKeyDown(Keys.D4) && TimeSinceLastPurchase > TimeBetweenPurchases)
                {
                    this.frame = new Rectangle(135, 7, 19, 47);
                   
                    SpawnTower();
                    TimeSinceLastPurchase = 0f;
                }

            else if (keyState.IsKeyDown(Keys.D5) && TimeSinceLastPurchase > TimeBetweenPurchases)
                {
                    this.frame = new Rectangle(161, 11, 37, 39);
                    SpawnTower();
                    TimeSinceLastPurchase = 0f;
                }

            else if (keyState.IsKeyDown(Keys.D6) && TimeSinceLastPurchase > TimeBetweenPurchases)
                {
                    this.frame = new Rectangle(206, 8, 32, 48);
                    SpawnTower();
                    TimeSinceLastPurchase = 0f;
                }

            for (int x = Towers.Count - 1; x >= 0; x--)
            {
                for (int y = NaziManager.Nazis.Count - 1; y >= 0; y--)
                {
                    Tower thisTower = Towers[x];
                    Nazi thisNazi = NaziManager.Nazis[y];

                    if (NaziManager.Nazis.Count == 0)
                    {

                    }
                    else
                    {
                        if (Math.Pow((thisTower.Center.X - thisNazi.Center.X), 2) + Math.Pow((thisTower.Center.Y - thisNazi.Center.Y), 2) <= Math.Pow((thisTower.range), 2))
                        {
                            Vector2 enemyPos = thisNazi.Center;
                            float distance = Vector2.Distance(thisNazi.Center, thisTower.Center);
                            float timeShot = distance / 250;
                            float sNazi = timeShot * thisNazi.speed;
                            Vector2 enemyDirection = thisNazi.Velocity;
                            enemyDirection.Normalize();
                            enemyPos += (sNazi * enemyDirection) / 2;

                            shotDirection = (enemyPos - thisTower.Location);
                            shotDirection.Normalize();
                            TowerShotManager.FireShot(
                                thisTower.Center,
                                shotDirection);
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
