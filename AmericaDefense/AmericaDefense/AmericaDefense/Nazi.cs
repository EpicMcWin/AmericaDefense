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
    enum EnemyType
    {
        FOOTSOLDIER,
        TANK,
        PLANE,
        BOMBER,
        TRANSPORT,
        SCOUT
    }

    class Nazi : Sprite
    {


        public Nazi(
            Vector2 location,
            Texture2D texture,
            Rectangle initialFrame,
            Vector2 velocity)
            : base(location, texture, initialFrame, velocity)
        {
            
        }

        
        private List<List<Vector2>> pathWaypoints =
            new List<List<Vector2>>();
        private Dictionary<int, int> waveSpawns = new Dictionary<int, int>();

        public EnemyType type;
        public int health;
        public int speed;
        public int value;
        public bool IsAircraft;

        public virtual void EnemyStats(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.FOOTSOLDIER:
                    health = 100;
                    speed = 50;
                    value = 20;
                    IsAircraft = false;
                    break;

                case EnemyType.SCOUT:
                    health = 50;
                    speed = 100;
                    value = 10;
                    IsAircraft = false;
                    break;

                case EnemyType.TANK:
                    health = 500;
                    speed = 30;
                    value = 50;
                    IsAircraft = false;
                    break;

                case EnemyType.PLANE:
                    health = 250;
                    speed = 150;
                    value = 70;
                    IsAircraft = true;
                    break;

                case EnemyType.BOMBER:
                    health = 400;
                    speed = 75;
                    value = 100;
                    IsAircraft = true;
                    break;

                case EnemyType.TRANSPORT:
                    health = 200;
                    speed = 100;
                    value = 30;
                    IsAircraft = false;
                    break;
            }
        }

        private void setUpWaypoints()
        {
            List<Vector2> path0 = new List<Vector2>();
            path0.Add(new Vector2(850, 300));
            path0.Add(new Vector2(-100, 300));
            pathWaypoints.Add(path0);
            waveSpawns[0] = 0;
        }

        
    }
}
