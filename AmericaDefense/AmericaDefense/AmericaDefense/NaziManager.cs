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
        public Rectangle initialFrame;
        private int frameCount;
        
        public static List<Nazi> Nazis = new List<Nazi>();
        public static List<Nazi> NazisinRange = new List<Nazi>();
        public static Queue<Nazi> naziTargets = new Queue<Nazi>();
        //public ShotManager EnemyShotManager;
       

        public int MinNazisPerWave = 10;
        public int MaxNazisPerWave = 10;
        private float nextWaveTimer = 0.0f;
        private float nextWaveMinTimer = 60.0f;
        private float naziSpawnTimer = 0.0f;
        private float naziSpawnWaitTime = 1;
        

        private List<List<Vector2>> pathWaypoints =
            new List<List<Vector2>>();

        private Dictionary<int, int> waveSpawns = new Dictionary<int, int>();
        public bool Active = true;
        private Random rand = new Random();
        

            private void setUpWaypoints()
        {
            List<Vector2> path = new List<Vector2>();
            path.Add(new Vector2(0, 220));
            path.Add(new Vector2(211, 220));
            path.Add(new Vector2(211, 90));
            path.Add(new Vector2(595, 90));
            path.Add(new Vector2(595, 710));
            path.Add(new Vector2(346, 710));
            path.Add(new Vector2(346, 447));
            path.Add(new Vector2(143, 447));
            path.Add(new Vector2(143, 910));
            path.Add(new Vector2(860, 910));
            path.Add(new Vector2(860, 770));
            path.Add(new Vector2(1215, 770));
            pathWaypoints.Add(path);
            waveSpawns[0] = 0;
        }
        

        public NaziManager(
            Texture2D texture,
            Rectangle initialFrame,
            int frameCount,
            Rectangle screenBounds)
        {
            this.texture = texture;
            this.frameCount = frameCount;

            for (int x = Nazis.Count - 1; x >= 0; x--)
            {
                if (Nazis[x].direction == Direction.RIGHT)
                    this.initialFrame = new Rectangle(79, 51, 24, 26);
                if (Nazis[x].direction == Direction.LEFT)
                    this.initialFrame = new Rectangle(79, 25, 24, 26);
                if (Nazis[x].direction == Direction.UP)
                    this.initialFrame = new Rectangle(79, 77, 24, 26);
                if (Nazis[x].direction == Direction.DOWN)
                    this.initialFrame = new Rectangle(79, 0, 24, 26);
            }
            setUpWaypoints();

            SpawnWave(0);
        }

       

        public void SpawnNazi(int path)
        {
            
            Nazi footSoldier = new Nazi(
                texture,
                pathWaypoints[path][0],
                initialFrame,
                Vector2.Zero);
            for (int x = 0; x < pathWaypoints[path].Count(); x++)
            {
                footSoldier.AddWaypoint(pathWaypoints[path][x]);
            }

            footSoldier.GetEnemyStats(EnemyType.FOOTSOLDIER);
            
            Nazis.Add(footSoldier);
        }

        public void SpawnWave(int waveNumber)
        {
            waveSpawns[waveNumber] +=
                rand.Next(MinNazisPerWave, MaxNazisPerWave + 1);
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
                        SpawnNazi(x);
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
            if (Nazis.Count > 0)
            {
                for (int x = Nazis.Count - 1; x >= 0; x--)
                {

                    if (Nazis[x].Velocity.X > 0 && (Nazis[x].Velocity.Y < 3 && Nazis[x].Velocity.Y > -3))
                    {
                        Nazis[x].ClearFrames();
                        Nazis[x].direction = Direction.RIGHT;
                        Nazis[x].AddFrame(new Rectangle(79, 50, 24, 26));
                        Nazis[x].AddFrame(new Rectangle(130, 50, 24, 26));
                        Nazis[x].AddFrame(new Rectangle(104, 50, 24, 26));
                    }

                    else if (Nazis[x].Velocity.X < 0 && (Nazis[x].Velocity.Y < 3 && Nazis[x].Velocity.Y > -3))
                    {
                        Nazis[x].ClearFrames();
                        Nazis[x].direction = Direction.LEFT;
                        Nazis[x].AddFrame(new Rectangle(79, 25, 24, 26));
                        Nazis[x].AddFrame(new Rectangle(130, 25, 24, 26));
                        Nazis[x].AddFrame(new Rectangle(104, 25, 24, 26));
                    }

                    else if (Nazis[x].Velocity.Y < 0 && (Nazis[x].Velocity.X < 3 && Nazis[x].Velocity.X > -3))
                    {
                        Nazis[x].ClearFrames();
                        Nazis[x].direction = Direction.UP;
                        Nazis[x].AddFrame(new Rectangle(79, 77, 24, 26));
                        Nazis[x].AddFrame(new Rectangle(130, 77, 24, 26));
                        Nazis[x].AddFrame(new Rectangle(104, 77, 24, 26));
                    }
                    else if (Nazis[x].Velocity.Y > 0 && (Nazis[x].Velocity.X < 3 && Nazis[x].Velocity.X > -3))
                    {
                        Nazis[x].ClearFrames();
                        Nazis[x].direction = Direction.DOWN;
                        Nazis[x].AddFrame(new Rectangle(79, 0, 24, 26));
                        Nazis[x].AddFrame(new Rectangle(130, 0, 24, 26));
                        Nazis[x].AddFrame(new Rectangle(104, 0, 24, 26));
                    }

                   

                    if (Nazis[x].inRange == true)
                    {
                        naziTargets.Enqueue(Nazis[x]);
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
                        if (Nazis[x].Location.X >= 1215 || Nazis[x].health <= 0)
                        {
                            naziTargets.Dequeue();
                        }
                    }
                } 
            
            }

            for (int x = NazisinRange.Count - 1; x >= 0; x--)
            {
                
                if (NazisinRange[x].inRange == false || NazisinRange[x].health <= 0)
                {
                    NazisinRange.RemoveAt(x);
                }
            }

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

                for (int i = Nazis.Count - 1; i >= 0; i--)
                {
                    Nazis[i].Update(gameTime);
                }
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
