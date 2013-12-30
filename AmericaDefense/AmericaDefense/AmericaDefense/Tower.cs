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
        public int cost;
        public int damage;
        public bool CanShootAircraft;

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
    }
}
