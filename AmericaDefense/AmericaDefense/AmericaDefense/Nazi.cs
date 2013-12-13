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
        TRANSPORTNAZI,
        SCOUT
    }

    class Nazi : Sprite
    {
        public Sprite NaziSprite;
        Texture2D FootSoldiers;
        Texture2D Tanks;

        private int naziRadius = 15;
        private Vector2 previousLocation = Vector2.Zero;
        private Vector2 currentWaypoint = Vector2.Zero;
        private Queue<Vector2> waypoints = new Queue<Vector2>();

        public Nazi(
            Texture2D texture,
            Vector2 location,
            Rectangle initialFrame,
            Vector2 velocity)
            : base(location, texture, initialFrame, velocity)
        {
            NaziSprite = new Sprite(
                location,
                texture,
                initialFrame,
                Vector2.Zero);

            previousLocation = location;
            currentWaypoint = location;
            NaziSprite.CollisionRadius = naziRadius;
        }
        
        
        private List<List<Vector2>> pathWaypoints =
            new List<List<Vector2>>();
        private Dictionary<int, int> waveSpawns = new Dictionary<int, int>();
        
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
        public void AddWaypoint(Vector2 waypoint)
        {
            waypoints.Enqueue(waypoint);
        }

        public bool WaypointReached()
        {
            if (Vector2.Distance(NaziSprite.Location, currentWaypoint) <
                (float)NaziSprite.Source.Width / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
        

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

        

        
    }
}
