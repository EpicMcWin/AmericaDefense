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
    class Nazi : Sprite
    {
        enum EnemyType
        {
            FOOTSOLDIER,
            TANK,
            PLANE,
            BOMBER,
            TRANSPORT,
            SCOUT
        }

        public Nazi(
            Vector2 location,
            Texture2D texture,
            Rectangle initialFrame,
            Vector2 velocity)
            : base(location, texture, initialFrame, velocity)
        {
            
        }

        public EnemyType type;
        public int health;
        public int speed;
        public int value;

        public virtual void EnemyStats()
        {
            switch (type)
            {
                case EnemyType.FOOTSOLDIER:
                    health = 100;
                    speed = 50;
                    value = 20;
                    break;

                case EnemyType.SCOUT:
                    health = 50;
                    speed = 100;
                    value = 10;
                    break;

                case EnemyType.TANK:
                    health = 500;
                    speed = 30;
                    value = 50;
                    break;

                case EnemyType.PLANE:
                    health = 250;
                    speed = 150;
                    value = 70;
                    break;

                case EnemyType.BOMBER:
                    health = 400;
                    speed = 75;
                    value = 100;
                    break;

                case EnemyType.TRANSPORT:
                    health = 200;
                    speed = 100;
                    value = 30;
                    break;
            }
        }

        public virtual void SpawnNazi()
        {

        }
    }
}
