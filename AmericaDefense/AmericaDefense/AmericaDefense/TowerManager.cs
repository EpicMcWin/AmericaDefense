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
        public static int funds = 200;
        
        private Vector2 offScreen = new Vector2(-500, -500);
        

        public TowerManager(
            Texture2D texture,
            Rectangle frame)
        {
            this.texture = texture;
            this.frame = frame;
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

                case TowerType.SUPERTOWER:
                    tower.GetTowerStats(TowerType.SUPERTOWER);
                    break;

                case TowerType.FLAMETHROWER:
                    tower.GetTowerStats(TowerType.FLAMETHROWER);
                    break;
            }

                Towers.Add(tower);
            
        }

        


        public void Update(GameTime gametime)
        {
            
            TimeSinceLastPurchase += (float)gametime.ElapsedGameTime.TotalSeconds;
            ms = Mouse.GetState();
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.D1) && TimeSinceLastPurchase > TimeBetweenPurchases && funds >= 50)
                {
                    this.frame = new Rectangle(4, 3, 33, 46);
                    SpawnTower(TowerType.RIFLE);
                    TimeSinceLastPurchase = 0f;
                    funds -= 50;
                    
                }

            else if (keyState.IsKeyDown(Keys.D2) && TimeSinceLastPurchase > TimeBetweenPurchases && funds >= 300)
                {
                    this.frame = new Rectangle(45, 9, 35, 40);
                    SpawnTower(TowerType.MG);
                    TimeSinceLastPurchase = 0f;
                    funds -= 300;   
                }

            else if (keyState.IsKeyDown(Keys.D3) && TimeSinceLastPurchase > TimeBetweenPurchases && funds >= 400)
                {
                    this.frame = new Rectangle(87, 8, 40, 55);
                    SpawnTower(TowerType.BAZOOKA);
                    TimeSinceLastPurchase = 0f;
                    funds -= 400;
                }

            else if (keyState.IsKeyDown(Keys.D4) && TimeSinceLastPurchase > TimeBetweenPurchases && funds >= 300)
                {
                    this.frame = new Rectangle(135, 7, 19, 47);
                    SpawnTower(TowerType.SNIPER);
                    TimeSinceLastPurchase = 0f;
                    funds -= 300;
                }

            else if (keyState.IsKeyDown(Keys.D5) && TimeSinceLastPurchase > TimeBetweenPurchases && funds >= 300)
                {
                    this.frame = new Rectangle(161, 11, 37, 39);
                    SpawnTower(TowerType.FLAMETHROWER);
                    TimeSinceLastPurchase = 0f;
                    funds -= 300;
                }

            else if (keyState.IsKeyDown(Keys.D6) && TimeSinceLastPurchase > TimeBetweenPurchases && funds >= 5000)
                {
                    this.frame = new Rectangle(206, 8, 32, 48);
                    SpawnTower(TowerType.SUPERTOWER);
                    TimeSinceLastPurchase = 0f;
                    funds -= 5000;
                }



            for (int x = Towers.Count - 1; x >= 0; x--)
            {
                Towers[x].Update(gametime);

                for (int y = Tower.naziTargets.Count - 1; y >= 0; y--)
                {
                    Towers[x].Shoot(Tower.naziTargets.Peek());
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tower tower in Towers)
            {
                tower.Draw(spriteBatch);
            }
        }
    }
}
