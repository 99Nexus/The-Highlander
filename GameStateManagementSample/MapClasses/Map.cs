﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.Text;
using GameStateManagement.Starships;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
#endregion Using Statements

namespace GameStateManagement.MapClasses
{
    public class Map : MapStructure
    {
        #region Fields

        // Map have an variable position from the abstract class
        public Level[] levels;
        public int mapNumber;
        public List<Enemy> enemies;
        public Vector2 playerStartPosition;
        public TheHighlander player;
        public Vector2 EnemyStartPosition;
        public Vector2 EnemyEndPosition;
        public List<Vector2> enemiesPositions;
        public MovementMode movementMode;
        public int enemiesNumber;
        private bool isCompleted;

        #endregion Fields

        //vector startposition
        //list von positionen(start,end)
        //(Bewegungsmuster) type als struct
        //enum von enemy class holen

        // max and mini enemies
        //positions from enemies

        #region Initialization
        public Map(TheHighlander player, int mapNumber)
        {
            this.mapNumber = mapNumber;
            this.levels = new Level[5];
            this.player = player;
            this.enemies = new List<Enemy>();
        }

        ///assign the background directly in the Constructor
        public override void LoadContent(ContentManager content) { }

        public void SetPositions(Level lvl)
        {
            //for (int i = 0; i < enemies.Count; i++)
            switch (lvl.levelNumber)
            {
                case 1:
                    lvl.position = new Vector2(this.position.X, this.position.Y);
                    this.playerStartPosition = (lvl.spawnPosition = new Vector2(lvl.position.X + 250, 100 + lvl.position.Y));
                    break;
                case 2:
                    lvl.position = new Vector2(this.position.X, this.position.Y + 1500);
                    lvl.spawnPosition = new Vector2(lvl.position.X + 250, lvl.position.Y + 250);
                    break;
                case 3:
                    lvl.position = new Vector2(1500 + this.position.X, 500 + this.position.Y);
                    lvl.spawnPosition = new Vector2(lvl.position.X + 250, lvl.position.Y + 1400);
                    break;
                case 4:
                    lvl.position = new Vector2(500 + this.position.X, 0 + this.position.Y);
                    lvl.spawnPosition = new Vector2(lvl.position.X + 1400, 250 + this.position.Y);
                    break;
                case 5:
                    lvl.position = new Vector2(500 + this.position.X, 500 + this.position.Y);
                    lvl.spawnPosition = new Vector2(lvl.position.X + 500, 100 + this.position.X);
                    break;
            }

            lvl.MakeBorders();
        }

        //create Levels
        public void CreateLevels()
        {
            int l = 1;
            for (int i = 0; i < this.levels.Length; i++)
            {
                //create new lvl and assign levelNumber
                this.levels[i] = new Level(l++);
                SetPositions(this.levels[i]);
            }
        }

        #endregion Initialization

        public void CompleteCheck()
        {
            foreach (Level le in levels)
            {
                if (le.hasEndBoos)
                {
                    if (le.endBossDefeated)
                    {
                        this.isCompleted = true;
                    }
                }
            }
        }

        #region Loading Enemy & Manage Explosions

        public void ObserveEnemies()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].actualShield <= 0)
                {
                    enemies.Remove(enemies[i]);
                }
            }
        }

        /*
        /*

        public void LoadEnemies()
        {
            /*
            if(enemyList.Count < 1)
            {
                enemyList.Add(new Enemy(theEnemy, einFont, 1, 0, 0, 0));
            }
            

            enemyList.Add(new Enemy(theEnemy, einFont, 1, 0, 0, 0));

            if (!enemyList[0].isVisible)
            {
                enemyList.RemoveAt(0);
                
            }


            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].isVisible)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }

            }
        }


        */

        //load enemies
        public void LoadEnemies(MovementMode movementMode, int enemiesNumber, List<Vector2> enemiesPositions)
        {

        }
        #endregion Loading Enemy & Manage Explosions 



        #region Update and Draw

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, position, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont sprite)
        {
            spriteBatch.DrawString(sprite, new string("Map" + mapNumber.ToString()), new Vector2(position.X + 50, position.Y + 50), Color.Black);
        }

        public void Update(GameTime gameTime, TheHighlander player)
        {
            foreach (Enemy e in enemies)
            {
                e.Update(gameTime, player.Position);
            }

            foreach (Level l in levels)
            {
                //l.Update();
            }

            ObserveEnemies();
        }
        #endregion Update and Draw

    }
}
