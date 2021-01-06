using GameStateManagement.Starships;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using GameStateManagement.MapClasses;

namespace GameStateManagement.GameManager
{
    public class CollisionManager
    {
        public int damageBuffer = 0;
        public int maxDamageBuffer = 30;
        public MainMap mainMap;
        public TheHighlander player;

        public CollisionManager(MainMap mainMap, TheHighlander player)
        {
            this.mainMap = mainMap;
            this.player = player;
        }

        public void ManageCollisions()
        {
            
            //CollisionBetweenPlayerAndEnemy();
            CollisionBetweenPlayerAndLaser();
            CollissionBetweenEnemyAndLaser();
            CollissionBetweenPlayerAndMapObject();
            CollisionBetweenPlayerLaserAndMapObject();
            CollisionBetweenEnemyLaserAndMapObject();
        }

        public void CollisionBetweenPlayerAndEnemy()
        {
            damageBuffer++;

            foreach (Map m in mainMap.maps)
            {
                foreach (Enemy e in m.enemies)
                {
                    if (player.Rectangle.Intersects(e.Rectangle))
                    {
                        // Set position backwards if player collides with an enemy
                        player.Position -= player.direction * 4f;

                        // Decrease shield only after damage buffer has reached a few milliseconds
                        if (damageBuffer >= maxDamageBuffer)
                        {
                            player.DecreaseShieldValue(1);
                            e.UpdateActualShieldValue(1);
                            damageBuffer = 0;
                        }

                        break;
                    }
                }
            }
        }

        public void CollisionBetweenPlayerAndLaser()
        {
            damageBuffer++;

            foreach (Map m in mainMap.maps)
            {
                foreach (Enemy e in m.enemies)
                {
                    foreach (Laser l in e.laserList)
                    {
                        if (player.Rectangle.Intersects(l.Rectangle) && damageBuffer >= maxDamageBuffer)
                        {
                            l.isVisible = false;
                            e.laserList.Remove(l);
                            player.DecreaseShieldValue(e.weaponPower);
                            damageBuffer = 0;
                            break;
                        }
                    }
                }
            }
        }

        public void CollissionBetweenEnemyAndLaser()
        {
            if (player.laserList.Count > 0)
            {
                for (int i = 0; i < player.laserList.Count; i++)
                {

                    foreach (Map m in mainMap.maps)
                    {
                        foreach (Enemy e in m.enemies)
                        {
                            if (player.laserList[i].Rectangle.Intersects(e.Rectangle) && e.damageBuffer >= e.maxDamageBuffer)
                            {
                                player.laserList[i].isVisible = false;
                                player.laserList.Remove(player.laserList[i]);
                                e.UpdateActualShieldValue(player.weaponPower);
                                e.damageBuffer = 0;
                                break;
                            }
                        }
                    }
                }
            }
        }
        
        public void CollissionBetweenPlayerAndMapObject()
        {
            foreach(Rectangle r in mainMap.rectangles)
            {
                if(player.Rectangle.Intersects(r)) 
                {
                    // Set position backwards if player collides with an map object
                    player.Position -= player.direction * 4f;
                    break;
                }
            }


            foreach(Map m in mainMap.maps)
            {
                foreach(Level l in m.levels)
                {
                    foreach(Rectangle r in l.rectangles) {

                        if(player.Rectangle.Intersects(r))
                        {
                            // Set position backwards if player collides with an map object
                            player.Position -= player.direction * 4f;
                            break;
                        }
                    }
                }
            }
        }

        public void CollisionBetweenPlayerLaserAndMapObject()
        {
            foreach (Rectangle r in mainMap.rectangles)
            {
                // Player laser
                for (int i = 0; i < player.laserList.Count; i++)
                {
                    if (player.laserList[i].Rectangle.Intersects(r))
                    {
                        player.laserList[i].isVisible = false;
                        player.laserList.Remove(player.laserList[i]);
                        break;
                    }
                }
            }


            foreach (Map m in mainMap.maps)
            {
                foreach (Level l in m.levels)
                {
                    foreach (Rectangle r in l.rectangles)
                    {
                        // Player laser
                        for (int i = 0; i < player.laserList.Count; i++)
                        {
                            if (player.laserList[i].Rectangle.Intersects(r))
                            {
                                player.laserList[i].isVisible = false;
                                player.laserList.Remove(player.laserList[i]);
                                break;
                            }
                        }
                    }
                }
            }
        }


        public void CollisionBetweenEnemyLaserAndMapObject()
        {
            foreach (Rectangle r in mainMap.rectangles)
            {
                foreach(Map m in mainMap.maps)
                {
                    foreach(Enemy e in m.enemies)
                    {
                        // Enemy laser
                        for (int i = 0; i < e.laserList.Count; i++)
                        {
                            if (e.laserList[i].Rectangle.Intersects(r))
                            {
                                e.laserList[i].isVisible = false;
                                e.laserList.Remove(e.laserList[i]);
                                break;
                            }
                        }
                    }
                }

            }


            foreach (Map m in mainMap.maps)
            {
                foreach (Level l in m.levels)
                {
                    foreach (Rectangle r in l.rectangles)
                    {
                        foreach (Enemy e in m.enemies)
                        {
                            // Enemy laser
                            for (int i = 0; i < e.laserList.Count; i++)
                            {
                                if (e.laserList[i].Rectangle.Intersects(r))
                                {
                                    e.laserList[i].isVisible = false;
                                    e.laserList.Remove(e.laserList[i]);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        /*
        public bool CollissionBetweenPlayerAndGameObject(TheHighlander player, GameObject gameObject)
        {
            return player.Rectangle.Intersects(gameObject.Rectangle);
        }
        */
    }
}
