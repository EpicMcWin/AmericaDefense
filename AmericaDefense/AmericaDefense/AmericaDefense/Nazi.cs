﻿using System;
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
    enum Direction
    {
        RIGHT,
        UP,
        DOWN,
        LEFT
    }

    class Nazi : Sprite
    {
        private int naziRadius = 30;
        private Vector2 previousLocation = Vector2.Zero;
        private Vector2 currentWaypoint = Vector2.Zero;
        private Queue<Vector2> waypoints = new Queue<Vector2>();
        

        public Direction direction;
        public Nazi(
            Texture2D texture,
            Vector2 location,
            Rectangle initialFrame,
            Vector2 velocity)
            : base(location, texture, initialFrame, velocity)
        {
            this.speed = 100;
            previousLocation = location;
            currentWaypoint = location;
            CollisionRadius = this.naziRadius;

        }

       
        
        public void AddWaypoint(Vector2 waypoint)
        {
            waypoints.Enqueue(waypoint);
        }

        public bool WaypointReached()
        {
            if (Vector2.Distance(this.Location, currentWaypoint) < 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsActive()
        {
            if (Destroyed)
            {
                return false;
            }

            if (waypoints.Count > 0)
            {
                return true;
            }

            if (WaypointReached())
            {
                return false;
            }

            return true;
        }

        public bool alreadyQueued;
        public bool inRange;
        public int health;
        public int speed;
        public int value;
        public bool IsAircraft;

        public void GetEnemyStats(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.FOOTSOLDIER:
                    health = 100 * (1 + NaziManager.counter/3);
                    speed = 150;
                    value = 10 + (5 * NaziManager.counter);
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
        public override void Update(GameTime gameTime)
        {
            
            if (IsActive())
            {
                Vector2 heading = currentWaypoint - this.Location;
                if (heading != Vector2.Zero)
                {
                    heading.Normalize();
                }
                heading *= speed;
                this.Velocity = heading;
                previousLocation = this.Location;
               
                this.Rotation =
                    (float)Math.Atan2(
                    this.Location.Y - previousLocation.Y,
                    this.Location.X - previousLocation.X);

                if (WaypointReached())
                {
                    if (waypoints.Count > 0)
                    {
                        currentWaypoint = waypoints.Dequeue();
                    }
                }

                base.Update(gameTime);
            }


              for (int x = TowerManager.Towers.Count - 1; x >= 0; x--)
              {
                for (int y = NaziManager.Nazis.Count - 1; y >= 0; y--)
                {
                    if (Vector2.Distance(TowerManager.Towers[x].Center, this.Center) <= TowerManager.Towers[x].range)
                    {
                        inRange = true;
                    }
                    else
                    {
                        inRange = false;
                    }

                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive())
            {
                base.Draw(spriteBatch);
            }
        }

        
    }
}
