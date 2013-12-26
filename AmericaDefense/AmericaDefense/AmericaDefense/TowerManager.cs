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
        public List<Tower> Towers = new List<Tower>();
        float TimeBetweenPurchases = 0.5f;
        float TimeSinceLastPurchase = 0f;

        public TowerManager(
            Texture2D texture,
            Rectangle frame)
        {
            this.texture = texture;
            this.frame = frame;
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
