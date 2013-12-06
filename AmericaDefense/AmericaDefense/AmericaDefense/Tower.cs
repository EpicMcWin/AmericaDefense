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
    class Tower : Sprite
    {
        enum TowerType
        {
            RIFLE,
            MG,
            BAZOOKA,
            SNIPER,
            FLAMETHROWER,
            AAGUN
        }
        public Tower(
            Vector2 location,
            Texture2D texture,
            Rectangle initialFrame,
            Vector2 velocity)
            : base(location, texture, initialFrame, velocity)
        {
            
        }
        public TowerType type;

        public int range;
        public float fireRate;
        public int projectileSpeed;
        public int distance;
        public int cost;

        public virtual void TowerStats(TowerType type)
        {
            switch (type)
            {
                case TowerType.RIFLE:
                    cost = 100;
                    fireRate = 
                    break;

                case TowerType.MG:
                    
                    break;

                case TowerType.BAZOOKA:
                    
                    break;

                case TowerType.SNIPER:
                    
                    break;

                case TowerType.AAGUN:
                    
                    break;

                case TowerType.FLAMETHROWER:
                    
                    break;
            }
        }

        public bool CanShoot(int range, int distance)
        {
            if (range - distance >= 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
        
    }
}
