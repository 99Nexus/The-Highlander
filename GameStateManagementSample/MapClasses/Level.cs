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
using System;
using System.Collections.Generic;
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

        public struct positionElement
        {
            public Vector2 start;
            public Vector2 end;
            public MovementMode MovementMode;
            public positionElement(Vector2 s, Vector2 e, MovementMode mMode)
            {
                start = s;
                end = e;
                MovementMode = mMode;
            }
        }

        public int levelNumber;
        public int mapNumber;
        public bool isCompleted;

        //borders
        public Rectangle[] rectangles;
        public Rectangle leftB;
        public Rectangle rightB;
        public Rectangle topB;
        public Rectangle bottomB;

        public TheHighlander theHighlander;
        public Vector2 spawnPosition;

        public Enemy endBoss;
        public List<Enemy> enemies;
        public List<positionElement> positionElements;
        static Random rnd = new Random();

        public List<GameObject> gameObjects;
        public Generator generator;

        //Items
        public List<GameItem> gameItems;
        public Teleport teleport;

        private List<Explosion> explosions;
        private Texture2D explosionTexture;

        #endregion Fields

        #region Initialization

        public Level(int lvlNumber, TheHighlander player, int mNumber)
        {
            levelNumber = lvlNumber;
            mapNumber = mNumber;
            theHighlander = player;
            rectangles = new Rectangle[2];
            enemies = new List<Enemy>();
            explosions = new List<Explosion>();
            gameObjects = new List<GameObject>();
            positionElements = new List<positionElement>();
        }

        public override void LoadContent(ContentManager content)
        {
            explosionTexture = content.Load<Texture2D>(@"explosion");
        }

        #endregion Initialization

        #region Enemies Initialization

        public void SetEnemiesPositions()
        {
            switch (levelNumber)
            {
                case 1:
                    positionElements.Add(new positionElement(new Vector2(position.X + 142, position.Y + 984), new Vector2(position.X + 305, position.Y + 984), MovementMode.HORIZONTAL));

                    positionElements.Add(new positionElement(new Vector2(position.X + 375, position.Y + 435), new Vector2(position.X + 375, position.Y + 635), MovementMode.VERTICAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 53, position.Y + 489), new Vector2(position.X + 53, position.Y + 705), MovementMode.VERTICAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 284, position.Y + 1263), new Vector2(position.X + 284, position.Y + 1059), MovementMode.VERTICAL));

                    positionElements.Add(new positionElement(new Vector2(position.X + 228, position.Y + 633), new Vector2(position.X + 228, position.Y + 633), MovementMode.PATROL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 434, position.Y + 1419), new Vector2(position.X + 434, position.Y + 1419), MovementMode.PATROL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 71, position.Y + 1419), new Vector2(position.X + 71, position.Y + 1419), MovementMode.PATROL));
                    break;

                case 2:
                    positionElements.Add(new positionElement(new Vector2(position.X + 860, position.Y + 1554), new Vector2(position.X + 1248, position.Y + 1554), MovementMode.HORIZONTAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 786, position.Y +  1695), new Vector2(position.X + 434, position.Y + 1695), MovementMode.HORIZONTAL));

                    positionElements.Add(new positionElement(new Vector2(position.X + 1347, position.Y + 1680), new Vector2(position.X + 1347, position.Y + 1884), MovementMode.VERTICAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 712, position.Y + 1936), new Vector2(position.X + 712, position.Y + 1844), MovementMode.VERTICAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 1163, position.Y + 1882), new Vector2(position.X + 1163, position.Y + 1754), MovementMode.VERTICAL));

                    positionElements.Add(new positionElement(new Vector2(position.X + 803, position.Y + 1702), new Vector2(position.X + 803, position.Y + 1702), MovementMode.PATROL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 638, position.Y + 1556), new Vector2(position.X + 638, position.Y + 1556), MovementMode.PATROL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 431, position.Y + 1876), new Vector2(position.X + 431, position.Y + 1876), MovementMode.PATROL));
                    break;

                case 3:
                    positionElements.Add(new positionElement(new Vector2(position.X + 119, position.Y + 359), new Vector2(position.X + 303, position.Y + 359), MovementMode.HORIZONTAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 142, position.Y + 984), new Vector2(position.X + 305, position.Y + 984), MovementMode.HORIZONTAL));

                    positionElements.Add(new positionElement(new Vector2(position.X + 375, position.Y + 435), new Vector2(position.X + 375, position.Y + 635), MovementMode.VERTICAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 53, position.Y + 489), new Vector2(position.X + 53, position.Y + 705), MovementMode.VERTICAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 284, position.Y + 1263), new Vector2(position.X + 284, position.Y + 1059), MovementMode.VERTICAL));

                    positionElements.Add(new positionElement(new Vector2(position.X + 228, position.Y + 633), new Vector2(position.X + 228, position.Y + 633), MovementMode.PATROL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 434, position.Y + 1419), new Vector2(position.X + 434, position.Y + 1419), MovementMode.PATROL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 71, position.Y + 1419), new Vector2(position.X + 71, position.Y + 1419), MovementMode.PATROL));
                    break;

                case 4:
                    positionElements.Add(new positionElement(new Vector2(position.X + 119, position.Y + 359), new Vector2(position.X + 303, position.Y + 359), MovementMode.HORIZONTAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 142, position.Y + 984), new Vector2(position.X + 305, position.Y + 984), MovementMode.HORIZONTAL));

                    positionElements.Add(new positionElement(new Vector2(position.X + 375, position.Y + 435), new Vector2(position.X + 375, position.Y + 635), MovementMode.VERTICAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 53, position.Y + 489), new Vector2(position.X + 53, position.Y + 705), MovementMode.VERTICAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 284, position.Y + 1263), new Vector2(position.X + 284, position.Y + 1059), MovementMode.VERTICAL));

                    positionElements.Add(new positionElement(new Vector2(position.X + 228, position.Y + 633), new Vector2(position.X + 228, position.Y + 633), MovementMode.PATROL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 434, position.Y + 1419), new Vector2(position.X + 434, position.Y + 1419), MovementMode.PATROL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 71, position.Y + 1419), new Vector2(position.X + 71, position.Y + 1419), MovementMode.PATROL));
                    break;

                case 5:
                    positionElements.Add(new positionElement(new Vector2(position.X + 119, position.Y + 359), new Vector2(position.X + 303, position.Y + 359), MovementMode.HORIZONTAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 142, position.Y + 984), new Vector2(position.X + 305, position.Y + 984), MovementMode.HORIZONTAL));

                    positionElements.Add(new positionElement(new Vector2(position.X + 375, position.Y + 435), new Vector2(position.X + 375, position.Y + 635), MovementMode.VERTICAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 53, position.Y + 489), new Vector2(position.X + 53, position.Y + 705), MovementMode.VERTICAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 284, position.Y + 1263), new Vector2(position.X + 284, position.Y + 1059), MovementMode.VERTICAL));

                    positionElements.Add(new positionElement(new Vector2(position.X + 228, position.Y + 633), new Vector2(position.X + 228, position.Y + 633), MovementMode.PATROL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 434, position.Y + 1419), new Vector2(position.X + 434, position.Y + 1419), MovementMode.PATROL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 71, position.Y + 1419), new Vector2(position.X + 71, position.Y + 1419), MovementMode.PATROL));
                    break;

            }
            SpawnEnemies();
        }

        public void FullEnemiesList(int numberOfEnemies)
        {
            int randomPosition;

            int[] orderOfEnemiesObj = OrderOfEnemies();

            // list [1,1,1,1,1]
            // list [2, 1, 2, 2, 2]
            // list [1, 3, 2, 3, 3]
            // list [3, 2, 1, 3, 2]

            for (int k = 0; k < numberOfEnemies; k++)
            {
                randomPosition = rnd.Next(positionElements.Count);

                if (orderOfEnemiesObj[k] == 1)
                {
                    enemies.Add(new TankerShip(positionElements[randomPosition].start, positionElements[randomPosition].end, theHighlander.Position, 20.0, positionElements[randomPosition].MovementMode));
                }

                if (orderOfEnemiesObj[k] == 2)
                {
                    enemies.Add(new SprinterShip(positionElements[randomPosition].start, positionElements[randomPosition].end, theHighlander.Position, 20.0, positionElements[randomPosition].MovementMode));
                }

                if (orderOfEnemiesObj[k] == 3)
                {
                    enemies.Add(new GunnerShip(positionElements[randomPosition].start, positionElements[randomPosition].end, theHighlander.Position, 20.0, positionElements[randomPosition].MovementMode));
                }
                positionElements.RemoveAt(randomPosition);
            }
        }

        public int[] OrderOfEnemies()
        {
            switch (mapNumber)
            {
                // list [1,1,1,1,1]
                // list [2, 1, 2, 2, 2]
                // list [1, 3, 2, 3, 3]
                // list [3, 2, 1, 3, 2]

                case 2:
                    return new int[] { 2, 1, 2, 2, 2 };
                case 3:
                    return new int[] { 1, 3, 2, 3, 3 };
                case 4:
                    return new int[] { 3, 2, 1, 3, 2 };
                default:
                    return new int[] { 1, 1, 1, 1, 1 };
            }
        }

        public void SpawnEnemies()
        {
            int numberOfEnemies;

            switch (levelNumber)
            {
                case 3:
                    numberOfEnemies = 5;
                    FullEnemiesList(numberOfEnemies);
                    break;

                case 5:
                    numberOfEnemies = 3;
                    FullEnemiesList(numberOfEnemies);
                    break;

                default:
                    numberOfEnemies = 4;
                    FullEnemiesList(numberOfEnemies);
                    break;
            }
        }

        public void SetEndBoss(int mapNumber)
        {
            switch (mapNumber)
            {
                case 1:
                    enemies.Add((endBoss = new Tanker(new Vector2(position.X + 500, position.Y + 900), new Vector2(position.X + 800, position.Y + 500), theHighlander.Position, 20.0, MovementMode.VERTICAL)));
                    break;

                case 2:
                    enemies.Add((endBoss = new Sprinter(new Vector2(position.X + 500, position.Y + 900), new Vector2(position.X + 800, position.Y + 500), theHighlander.Position, 20.0, MovementMode.VERTICAL)));
                    break;
                case 3:
                    enemies.Add((endBoss = new Gunner(new Vector2(position.X + 500, position.Y + 900), new Vector2(position.X + 800, position.Y + 500), theHighlander.Position, 20.0, MovementMode.VERTICAL)));
                    break;
                case 4:
                    enemies.Add((endBoss = new Doomer(new Vector2(position.X + 500, position.Y + 900), new Vector2(position.X + 800, position.Y + 500), theHighlander.Position, 20.0, MovementMode.VERTICAL)));
                    break;
            }
        }

        #endregion Enemies Initialization

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

            foreach (Enemy e in enemies)
            {
                e.Draw(gameTime, spriteBatch);
            }

            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch, sprite);
            }
            if (levelNumber == 4)
                generator.Draw(spriteBatch, sprite);

            foreach (Explosion ex in explosions)
            {
                ex.Draw(spriteBatch);
            }

            if (CheckIfCompleted())
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
            if (levelNumber == 4)
                generator.Update(gameTime);

            foreach (Explosion ex in explosions)
            {
                ex.Update(gameTime);
            }
            ObserveEnemies();
            ManageExplosions();
        }
        #endregion Update and Draw

        #region Loading & Manage Enemies

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

        #endregion Loading & Manage Enemies


        #region Manage Level and Explosions


        public bool CheckIfCompleted()
        {
            switch (levelNumber)
            {
                case 1:
                    foreach (GameObject go in gameObjects)
                    {
                        if (!go.keyPressed)
                            return false;
                    }
                    break;
                case 2:
                    foreach (GameObject go in gameObjects)
                    {
                        if (!go.keyPressed)
                            return false;
                    }
                    break;
                case 3:
                    if (enemies.Any())
                        return false;
                    break;
                case 4:
                    if (!generator.damaged)
                        return false;
                    break;
                case 5:
                    if (!(endBoss is null))
                        return false;
                    break;
            }
            return (isCompleted = true);
        }


        public void ManageExplosions()
        {
            for (int i = 0; i < explosions.Count; i++)
            {
                if (!explosions[i].isVisible)
                    explosions.RemoveAt(i--);
            }
        }

        #endregion Manage Level and Explosions
    }
}
