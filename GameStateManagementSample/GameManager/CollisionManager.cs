using GameStateManagement.Starships;
using Microsoft.Xna.Framework;
using GameStateManagement.MapClasses;
using GameStateManagement.ObjectItem;
using System.Linq;

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

            CollisionBetweenPlayerAndEnemy();
            //CollisionBetweenPlayerAndLaser();
            CollissionBetweenEnemyAndLaser();
            //CollissionBetweenPlayerAndMapObject();
            //CollisionBetweenPlayerLaserAndMapObject();
            //CollisionBetweenEnemyLaserAndMapObject();
            CollissionBetweenPlayerAndGameObject();
            CollisionBetweenPlayerLaserAndGameObject();
            CollisionBetweenEnemyLaserAndGameObject();
        }

        public void CollisionBetweenPlayerAndEnemy()
        {
            damageBuffer++;

            foreach (Map m in mainMap.maps)
            {
                foreach (Level lv in m.levels)
                {
                    foreach (Enemy e in lv.enemies)
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
        }

        public void CollisionBetweenPlayerAndLaser()
        {
            damageBuffer++;

            foreach (Map m in mainMap.maps)
            {
                foreach (Level lv in m.levels)
                {
                    foreach (Enemy e in lv.enemies)
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
        }

        public void CollissionBetweenEnemyAndLaser()
        {
            foreach (Map m in mainMap.maps)
            {
                foreach (Level lv in m.levels)
                {
                    foreach (Enemy e in lv.enemies)
                    {
                        foreach (Laser l in player.laserList)
                        {
                            if (l.Rectangle.Intersects(e.Rectangle) && e.damageBuffer >= e.maxDamageBuffer)
                            {
                                l.isVisible = false;
                                player.laserList.Remove(l);
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
            foreach (Rectangle r in mainMap.rectangles)
            {
                if (player.Rectangle.Intersects(r))
                {
                    // Set position backwards if player collides with an map object
                    player.Position -= player.direction * 4f;
                    break;
                }
            }
            foreach (Map m in mainMap.maps)
            {
                foreach (Level l in m.levels)
                {
                    foreach (Rectangle r in l.rectangles)
                    {

                        if (player.Rectangle.Intersects(r))
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
                foreach (Map m in mainMap.maps)
                {
                    foreach (Level lv in m.levels)
                    {
                        foreach (Enemy e in lv.enemies)
                        {
                            // Enemy laser
                            foreach (Laser l in e.laserList)
                            {
                                if (l.Rectangle.Intersects(r))
                                {
                                    l.isVisible = false;
                                    e.laserList.Remove(l);
                                    break;
                                }
                            }
                        }
                    }
                }

            }

            foreach (Map m in mainMap.maps)
            {
                foreach (Level lv in m.levels)
                {
                    foreach (Rectangle r in lv.rectangles)
                    {
                        foreach (Enemy e in lv.enemies)
                        {
                            // Enemy laser
                            foreach (Laser l in e.laserList)
                            {
                                if (l.Rectangle.Intersects(r))
                                {
                                    l.isVisible = false;
                                    e.laserList.Remove(l);
                                    break;
                                }
                            }
                        }

                    }
                }
            }
        }

        public void CollissionBetweenPlayerAndGameObject()
        {
            foreach (Map m in mainMap.maps)
            {
                foreach (Level lv in m.levels)
                {
                    foreach (GameObject go in lv.gameObjects)
                    {
                        if (player.Rectangle.Intersects(go.rectangle))
                        {
                            // Set position backwards if player collides with an map object
                            player.Position -= player.direction * 4f;
                            break;
                        }
                        if (lv.levelNumber == 4 && player.Rectangle.Intersects(lv.generator.rectangle))
                        {
                            if (lv.generator.damageBuffer <= lv.generator.maxDamageBuffer)
                            {
                                lv.generator.UpdateActualShieldValue(1);
                                lv.generator.damageBuffer = 0;
                            }

                            if (damageBuffer >= maxDamageBuffer)
                            {
                                player.DecreaseShieldValue(1);
                                damageBuffer = 0;
                            }

                            player.Position -= player.direction * 4f;
                            break;
                        }
                    }
                }
            }
        }

        public void CollisionBetweenPlayerLaserAndGameObject()
        {
            foreach (Map m in mainMap.maps)
            {
                foreach (Level lv in m.levels)
                {
                    foreach (GameObject go in lv.gameObjects)
                    {
                        foreach (Laser l in player.laserList)
                        {
                            if (l.Rectangle.Intersects(go.rectangle))
                            {

                                l.isVisible = false;
                                player.laserList.Remove(l);
                                break;
                            }
                            if (lv.levelNumber == 4 && l.Rectangle.Intersects(lv.generator.rectangle))
                            {
                                if (lv.generator.damageBuffer <= lv.generator.maxDamageBuffer)
                                {
                                    lv.generator.UpdateActualShieldValue(1);
                                    lv.generator.damageBuffer = 0;
                                }
                                l.isVisible = false;
                                player.laserList.Remove(l);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void CollisionBetweenEnemyLaserAndGameObject()
        {
            foreach (Map m in mainMap.maps)
            {
                foreach (Level lv in m.levels)
                {
                    foreach (GameObject go in lv.gameObjects)
                    {
                        foreach (Enemy e in lv.enemies)
                        {
                            foreach (Laser l in e.laserList)
                            {
                                if (l.Rectangle.Intersects(go.rectangle))
                                {
                                    l.isVisible = false;
                                    e.laserList.Remove(l);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
