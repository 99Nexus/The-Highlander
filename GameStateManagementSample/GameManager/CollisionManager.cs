using GameStateManagement.Starships;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

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
                        player.DecreaseShieldValue();
                        e.UpdateActualShieldValue();
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
                        player.DecreaseShieldValue();
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
                            e.UpdateActualShieldValue();
                            e.damageBuffer = 0;
                            break;
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

        public bool CollissionBetweenPlayerAndMapObject(TheHighlander player, MapObject mapObject)
        {
            return player.Rectangle.Intersects(mapObject.Rectangle);
        }

        public bool CollisionBetweenLaserAndMapObject(Laser laser, MapObject mapObject)
        {
            return laser.Rectangle.Intersects(mapObject.Rectangle);
        }
        */
    }
}
