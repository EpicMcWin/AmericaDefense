using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AmericaDefense
{
    class NaziManager
    {
        private Texture2D texture;
        private Rectangle initialFrame;
        private int frameCount;

        public List<Nazi> Nazis = new List<Nazi>();

        //public ShotManager EnemyShotManager;
       

        //public int MinShipsPerWave = 6;
        //public int MaxShipsPerWave = 10;
        private float nextWaveTimer = 0.0f;
        private float nextWaveMinTimer = 30.0f;
        private float naziSpawnTimer = 0.0f;
        private float naziSpawnWaitTime = 1;

        private List<List<Vector2>> pathWaypoints =
            new List<List<Vector2>>();

        private Dictionary<int, int> waveSpawns = new Dictionary<int, int>();

        public bool Active = false;

        private Random rand = new Random();
        private Texture2D FootSoldiers;
        private Rectangle rectangle;
        private int p;
        private Rectangle rectangle_2;

        public NaziManager(Texture2D FootSoldiers, Rectangle rectangle, int p, Rectangle rectangle_2)
        {
            // TODO: Complete member initialization
            this.FootSoldiers = FootSoldiers;
            this.rectangle = rectangle;
            this.p = p;
            this.rectangle_2 = rectangle_2;
        }

        
            private void setUpWaypoints()
        {
            List<Vector2> path0 = new List<Vector2>();
            path0.Add(new Vector2(284, 284));
            path0.Add(new Vector2(284, 142));
            path0.Add(new Vector2(781, 142));
            path0.Add(new Vector2(781, 710));
            path0.Add(new Vector2(568, 710));
            path0.Add(new Vector2(426, 710));
            path0.Add(new Vector2(426, 923));
            path0.Add(new Vector2(852, 923));
            path0.Add(new Vector2(852, 1065));
            path0.Add(new Vector2(994, 1065));
            path0.Add(new Vector2(994, 497));
            path0.Add(new Vector2(1278, 497));
            pathWaypoints.Add(path0);
            waveSpawns[0] = 0;
        }
        

        public void NaziManager(
            Texture2D texture,
            Rectangle initialFrame,
            int frameCount,
            Rectangle screenBounds)
        {
            this.texture = texture;
            this.initialFrame = initialFrame;
            this.frameCount = frameCount;
            
            setUpWaypoints();
        }

        public void SpawnEnemy(int path)
        {
            Nazi thisEnemy = new Nazi(
                texture,
                pathWaypoints[path][0],
                initialFrame,
                Vector2.Zero);
            for (int x = 0; x < pathWaypoints[path].Count(); x++)
            {
                thisEnemy.AddWaypoint(pathWaypoints[path][x]);
            }
            Nazis.Add(thisEnemy);
        }

        public void SpawnWave(int waveNumber)
        {
            waveSpawns[waveNumber] += 1;
        }

        private void updateWaveSpawns(GameTime gameTime)
        {
            naziSpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (naziSpawnTimer > naziSpawnWaitTime)
            {
                for (int x = waveSpawns.Count - 1; x >= 0; x--)
                {
                    if (waveSpawns[x] > 0)
                    {
                        waveSpawns[x]--;
                        SpawnEnemy(x);
                    }
                }
                naziSpawnTimer = 0f;
            }

            nextWaveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (nextWaveTimer > nextWaveMinTimer)
            {
                SpawnWave(rand.Next(0, pathWaypoints.Count));
                nextWaveTimer = 0f;
            }
        }

        public void Update(GameTime gameTime)
        {
            

            //for (int x = Nazis.Count - 1; x >= 0; x--)
            //{
            //    Nazis[x].Update(gameTime);
            //    if (Nazis[x].IsActive() == false)
            //    {
            //        Nazis.RemoveAt(x);
            //    }
            //    else
            //    {
            //        if ((float)rand.Next(0, 1000) / 10 <= shipShotChance)
            //        {
            //            Vector2 fireLoc = Nazis[x].EnemySprite.Location;
            //            fireLoc += Nazis[x].gunOffset;

            //            Vector2 shotDirection =
            //                playerManager.playerSprite.Center -
            //                fireLoc;

            //            shotDirection.Normalize();

            //            //EnemyShotManager.FireShot(
            //            //    fireLoc,
            //            //    shotDirection,
            //            //    false);
            //        }
            //    }
            //}

            if (Active)
            {
                updateWaveSpawns(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //EnemyShotManager.Draw(spriteBatch);

            foreach (Nazi nazi in Nazis)
            {
                nazi.Draw(spriteBatch);
            }
        }

    }
}
