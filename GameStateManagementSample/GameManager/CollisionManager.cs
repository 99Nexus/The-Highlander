using GameStateManagement.Starships;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using GameStateManagement.MapClasses;

namespace GameStateManagement.GameManager
{
    class CollisionManager
    {
        int damageBuffer = 0;
        int maxDamageBuffer = 30;

        public CollisionManager()
        {

        }

        public void CollisionBetweenPlayerAndEnemy(TheHighlander player, List<Enemy> enemyList)
        {
            damageBuffer++;

            foreach(Enemy e in enemyList)
            {
                if (player.Rectangle.Intersects(e.Rectangle))
                {
                    // Set position backwards if player collides with an enemy
                    player.Position -= player.direction * 4f;

                    // Decrease shield only after damage buffer has reached a few milliseconds
                    if(damageBuffer >= maxDamageBuffer)
                    {
                        player.DecreaseShieldValue(1);
                        e.UpdateActualShieldValue(1);
                        damageBuffer = 0;
                    }

                    break;
                }
            }
        }

        public void CollisionBetweenPlayerAndLaser(TheHighlander player, List<Enemy> enemyList)
        {
            damageBuffer++;

            foreach(Enemy e in enemyList)
            {
                foreach(Laser l in e.laserList)
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

        public void CollissionBetweenEnemyAndLaser(TheHighlander player, List<Enemy> enemyList)
        {
            if (player.laserList.Count > 0)
            {
                for (int i = 0; i < player.laserList.Count; i++)
                {
                    foreach (Enemy e in enemyList)
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
        
        public void CollissionBetweenPlayerAndMapObject(TheHighlander player, List<Level> levelList)
        {
            damageBuffer++;

            foreach (Level l in levelList)
            {
                if (player.Rectangle.Intersects(l.topB) ||
                    player.Rectangle.Intersects(l.rightB) ||
                    player.Rectangle.Intersects(l.bottomB) ||
                    player.Rectangle.Intersects(l.leftB))
                {
                    // Set position backwards if player collides with an enemy
                    player.Position -= player.direction * 4f;

                    // Decrease shield only after damage buffer has reached a few milliseconds
                    if (damageBuffer >= maxDamageBuffer)
                    {
                        player.DecreaseShieldValue(1);
                        damageBuffer = 0;
                    }

                    break;
                }
            }
        }

        public void CollisionBetweenLaserAndMapObject(List<Level> levelList, TheHighlander player, List<Enemy> enemyList)
        {
            foreach (Level level in levelList)
            {

                //Enemy laser
                foreach (Enemy e in enemyList)
                {
                    for (int i = 0; i < e.laserList.Count; i++)
                    {
                        if (e.laserList[i].Rectangle.Intersects(level.topB) ||
                            e.laserList[i].Rectangle.Intersects(level.rightB) ||
                            e.laserList[i].Rectangle.Intersects(level.bottomB) ||
                            e.laserList[i].Rectangle.Intersects(level.leftB))
                        {
                            e.laserList[i].isVisible = false;
                            e.laserList.Remove(player.laserList[i]);
                            break;
                        }
                    }
                }

                //Player laser
                for (int i = 0; i < player.laserList.Count; i++)
                {
                    if (player.laserList[i].Rectangle.Intersects(level.topB) ||
                        player.laserList[i].Rectangle.Intersects(level.rightB) ||
                        player.laserList[i].Rectangle.Intersects(level.bottomB) ||
                        player.laserList[i].Rectangle.Intersects(level.leftB))
                    {
                        player.laserList[i].isVisible = false;
                        player.laserList.Remove(player.laserList[i]);
                        break;
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
