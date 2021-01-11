#region File Description

//-----------------------------------------------------------------------------
// Level.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description
#region Using Statements
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using GameStateManagement.Starships;
using GameStateManagement.GameItems;
using GameStateManagement.ObjectItem;
using System.Linq;
#endregion Using Statements

namespace GameStateManagement.MapClasses
{
    public class Level : MapStructure
    {
        #region Fields

        public int levelNumber;
        public int mapNumber;
        public Vector2 spawnPosition;
        public bool isCompleted;

        public TheHighlander theHighlander;

        //endboss
        private Enemy endBoss;
        public bool hasEndBoos;
        public bool endBossDefeated;

        public List<Enemy> enemies;
        public List<GameObject> gameObjects;
        public List<GameItem> gameItems;

        private List<Explosion> explosions;
        private Texture2D explosionTexture;

        //borders
        public Rectangle[] rectangles;
        public Rectangle leftB;
        public Rectangle rightB;
        public Rectangle topB;
        public Rectangle bottomB;

        //Position borders
        private float topLeft;
        private float topRight;
        private float bottomLeft;
        private float bottomRight;

        //Items
        public Teleport teleport;

        #endregion Fields

        #region Initialization

        public Level(int lvlNumber, TheHighlander player)
        {
            levelNumber = lvlNumber;
            theHighlander = player;

            rectangles = new Rectangle[2];

            enemies = new List<Enemy>();
            explosions = new List<Explosion>();
            gameObjects = new List<GameObject>();
        }

        public override void LoadContent(ContentManager content) {
            explosionTexture = content.Load<Texture2D>(@"explosion");
        }

        public void SetEndBoss(Enemy enemy, Vector2 EndBossPostion)
        {
            endBoss = enemy;
            endBoss.Position = new Vector2(position.X + 850, position.Y + 500);
            hasEndBoos = true;
        }

        #endregion Initialization

        #region Borders Initialization
        public void MakeBorders()
        {
            if (levelNumber == 5)
                return;

            if (levelNumber == 1)
            {
                /// Map 1 Lvl 1
                /// left: P: x 0  y 0 , Size: w 10 x  h 1500
                /// right: P: x 490  y 0 , Size: w 10 x  h 1500
                rectangles[0] = rightB = new Rectangle((int)position.X + 490, (int)position.Y, 10, 1500);
                rectangles[1] = leftB = new Rectangle((int)position.X, (int)position.Y, 10, 500);
            }

            else if (levelNumber == 2)
            {
                /// Map 1 Lvl 2
                /// left: P: x 0  y 1500 , Size: w 10 x  h 500
                /// top: P: x 500  y 1500 , Size: w 1500 x  h 10
                rectangles[0] = leftB = new Rectangle((int)position.X, (int)position.Y, 10, 500);
                rectangles[1] = topB = new Rectangle((int)position.X, (int)position.Y, 1500, 10);
            }

            else if (this.levelNumber == 3)
            {
                /// Map 1 Lvl 3
                /// left: P: x 1500  y 500 , Size: w 10 x  h 1500
                rectangles[0] = leftB = new Rectangle((int)position.X, (int)position.Y, 10, 1500);
            }

            else if (levelNumber == 4)
            {
                /// Map 1 Lvl 4
                /// bottom: P: x 500  y + 490 , Size: w 1500 x  h 10 
                rectangles[0] = bottomB = new Rectangle((int)position.X, (int)position.Y + 490, 1500, 10);
            }
        }
        #endregion Borders Initialization

        #region Update and Draw

        public override void Draw(SpriteBatch spriteBatch, SpriteFont sprite, GameTime gameTime)
        {
            spriteBatch.DrawString(sprite, new string("Level" + levelNumber.ToString()), position, Color.Black);

            foreach(Enemy e in enemies)
            {
                e.Draw(gameTime, spriteBatch);
            }

            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch, sprite);
            }

            foreach (Explosion ex in explosions)
            {
                ex.Draw(spriteBatch);
            }

            if (true)
            {
                teleport.Draw(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime, TheHighlander theHighlander)
        {

            foreach (Enemy e in enemies)
            {
                e.Update(gameTime, theHighlander.Position);
            }

            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);
            }

            foreach (Explosion ex in explosions)
            {
                ex.Update(gameTime);
            }
            ObserveEnemies();
            ManageExplosions();
        }
        #endregion Update and Draw

        #region Loading & Manage Enemies, Objects and Explosions


        public void CompleteCheck()
        {
                if (hasEndBoos)
                {
                    if (endBossDefeated)
                    {
                        isCompleted = true;
                    }
                }
        }

        public void ObserveEnemies()
        {
            foreach (Enemy e in enemies.ToList())
            {
                if (e.actualShield <= 0)
                {
                    explosions.Add(new Explosion(explosionTexture, new Vector2(e.Position.X - 50, e.Position.Y - 25)));
                    theHighlander.PlayerScore.Value += e.score;
                    enemies.Remove(e);
                }
            }
        }
        /*
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].actualShield <= 0)
            {
                explosions.Add(new Explosion(explosionTexture, new Vector2(enemies[i].Position.X - 50, enemies[i].Position.Y - 25)));
                enemies.Remove(enemies[i]);
                theHighlander.PlayerScore.Value += 150;
            }
        }
        */


        public void ManageExplosions()
        {
            for (int i = 0; i < explosions.Count; i++)
            {
                if (!explosions[i].isVisible)
                    explosions.RemoveAt(i--);
            }
        }

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
        public void LoadEnemies(MovementMode movementMode, int enemiesNumber)
        {

        }
        #endregion Loading Enemy & Manage Explosions 
    }
}
