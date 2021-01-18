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

        // rectangle for position check 
        public Rectangle levelArea;

        public TheHighlander theHighlander;
        public Vector2 spawnPosition;

        public Enemy endBoss;
        public bool isEndBossDestroyed;
        public List<Enemy> enemies;
        public List<positionElement> positionElements;
        static Random rnd = new Random();

        public List<GameObject> gameObjects;
        public Generator generator;

        //Items
        public List<GameItem> mapPieces;
        public UpdateShield updateShield;
        public Teleport teleport;

        private List<Explosion> explosions;
        private Texture2D explosionTexture;

        // Mission
        public Mission mission;


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
            mission = new Mission(lvlNumber, player);
            mapPieces = new List<GameItem>();
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
                    positionElements.Add(new positionElement(new Vector2(position.X + 860, position.Y + 54), new Vector2(position.X + 1248, position.Y + 54), MovementMode.HORIZONTAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 786, position.Y +  195), new Vector2(position.X + 434, position.Y + 195), MovementMode.HORIZONTAL));

                    positionElements.Add(new positionElement(new Vector2(position.X + 1347, position.Y + 180), new Vector2(position.X + 1347, position.Y + 384), MovementMode.VERTICAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 712, position.Y + 436), new Vector2(position.X + 712, position.Y + 344), MovementMode.VERTICAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 1163, position.Y + 382), new Vector2(position.X + 1163, position.Y + 254), MovementMode.VERTICAL));

                    positionElements.Add(new positionElement(new Vector2(position.X + 803, position.Y + 202), new Vector2(position.X + 803, position.Y + 202), MovementMode.PATROL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 638, position.Y + 56), new Vector2(position.X + 638, position.Y + 56), MovementMode.PATROL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 431, position.Y + 376), new Vector2(position.X + 431, position.Y + 376), MovementMode.PATROL));
                    break;

                case 3:
                    positionElements.Add(new positionElement(new Vector2(position.X + 61, position.Y + 1132), new Vector2(position.X + 1273, position.Y + 1143), MovementMode.HORIZONTAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 59, position.Y + 58), new Vector2(position.X + 195, position.Y + 58), MovementMode.HORIZONTAL));

                    positionElements.Add(new positionElement(new Vector2(position.X + 147, position.Y + 347), new Vector2(position.X + 147, position.Y + 184), MovementMode.VERTICAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 142, position.Y + 519), new Vector2(position.X + 142, position.Y + 647), MovementMode.VERTICAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 409, position.Y + 736), new Vector2(position.X + 409, position.Y + 916), MovementMode.VERTICAL));

                    positionElements.Add(new positionElement(new Vector2(position.X + 438, position.Y + 1155), new Vector2(position.X + 438, position.Y + 1155), MovementMode.PATROL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 55, position.Y + 976), new Vector2(position.X + 55, position.Y + 976), MovementMode.PATROL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 435, position.Y + 61), new Vector2(position.X + 435, position.Y + 61), MovementMode.PATROL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 301, position.Y + 671), new Vector2(position.X + 301, position.Y + 671), MovementMode.PATROL));
                    break;

                case 4:
                    positionElements.Add(new positionElement(new Vector2(position.X + 1210, position.Y + 435), new Vector2(position.X + 962, position.Y + 435), MovementMode.HORIZONTAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 653, position.Y + 283), new Vector2(position.X + 445, position.Y + 283), MovementMode.HORIZONTAL));

                    positionElements.Add(new positionElement(new Vector2(position.X + 855, position.Y + 168), new Vector2(position.X + 855, position.Y + 284), MovementMode.VERTICAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 85, position.Y + 427), new Vector2(position.X + 85, position.Y + 311), MovementMode.VERTICAL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 569, position.Y + 55), new Vector2(position.X + 569, position.Y + 143), MovementMode.VERTICAL));

                    positionElements.Add(new positionElement(new Vector2(position.X + 1100, position.Y + 66), new Vector2(position.X + 1100, position.Y + 66), MovementMode.PATROL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 746, position.Y + 356), new Vector2(position.X + 746, position.Y + 356), MovementMode.PATROL));
                    positionElements.Add(new positionElement(new Vector2(position.X + 65, position.Y + 112), new Vector2(position.X + 65, position.Y + 112), MovementMode.PATROL));
                    break;

                case 5:
                    break;

            }
            SpawnEnemies();
        }

        public void FullEnemiesList(int numberOfEnemies)
        {
            TankerShip tankerShip;
            SprinterShip sprinterShip;
            GunnerShip gunnerShip;

            int randomPosition;

            int[] orderOfEnemiesObj = OrderOfEnemies();

            // list [1, 1, 1, 1, 1]
            // list [2, 1, 2, 2, 2]
            // list [1, 3, 2, 3, 3]
            // list [3, 2, 1, 3, 2]

            for (int k = 0; k < numberOfEnemies; k++)
            {
                randomPosition = rnd.Next(positionElements.Count);

                if (orderOfEnemiesObj[k] == 1)
                {
                    tankerShip = new TankerShip(positionElements[randomPosition].start, positionElements[randomPosition].end, theHighlander.Position, 20.0, positionElements[randomPosition].MovementMode);

                    // give one enemy in every level an update shield item
                    if (k == numberOfEnemies-1)
                        tankerShip.gameItem = new UpdateShield(tankerShip.Position, tankerShip);

                    enemies.Add(tankerShip);
                }

                if (orderOfEnemiesObj[k] == 2)
                {
                    sprinterShip = new SprinterShip(positionElements[randomPosition].start, positionElements[randomPosition].end, theHighlander.Position, 20.0, positionElements[randomPosition].MovementMode);

                    // give one enemy in every level an update shield item
                    if (k == numberOfEnemies - 1)
                        sprinterShip.gameItem = new UpdateShield(sprinterShip.Position, sprinterShip);

                    enemies.Add(sprinterShip);
                }

                if (orderOfEnemiesObj[k] == 3)
                {
                    gunnerShip = new GunnerShip(positionElements[randomPosition].start, positionElements[randomPosition].end, theHighlander.Position, 20.0, positionElements[randomPosition].MovementMode);

                    // give one enemy in every level an update shield item
                    if (k == numberOfEnemies - 1)
                        gunnerShip.gameItem = new UpdateShield(gunnerShip.Position, gunnerShip);

                    enemies.Add(gunnerShip);
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
                    enemies.Add(endBoss = new Tanker(new Vector2(position.X + 500, position.Y + 900), new Vector2(position.X + 800, position.Y + 500), theHighlander.Position, 20.0, MovementMode.VERTICAL));
                    break;
                case 2:
                    enemies.Add(endBoss = new Sprinter(new Vector2(position.X + 500, position.Y + 900), new Vector2(position.X + 800, position.Y + 500), theHighlander.Position, 20.0, MovementMode.VERTICAL));
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

                // create rectangleArea
                levelArea = new Rectangle((int)position.X + 10, (int)position.Y + 10, 1480, 480);

            return;

            if (levelNumber == 1)
            {
                /// Map 1 Lvl 1
                /// left: P: x 0  y 0 , Size: w 10 x  h 1500
                /// right: P: x 490  y 0 , Size: w 10 x  h 1500
                rectangles[0] = rightB = new Rectangle((int)position.X + 490, (int)position.Y, 10, 1500);
                rectangles[1] = leftB = new Rectangle((int)position.X, (int)position.Y, 10, 500);

                // create rectangleArea
                levelArea = new Rectangle(0 + 10, 0 + 10, 480, 1480);
            }

            else if (levelNumber == 2)
            {
                /// Map 1 Lvl 2
                /// left: P: x 0  y 1500 , Size: w 10 x  h 500
                /// top: P: x 500  y 1500 , Size: w 1500 x  h 10
                rectangles[0] = leftB = new Rectangle((int)position.X, (int)position.Y, 10, 500);
                rectangles[1] = topB = new Rectangle((int)position.X, (int)position.Y, 1500, 10);

                // create rectangleArea
                levelArea = new Rectangle((int)position.X + 10, (int)position.Y + 10, 1480, 480);

            }

            else if (this.levelNumber == 3)
            {
                /// Map 1 Lvl 3
                /// left: P: x 1500  y 500 , Size: w 10 x  h 1500
                rectangles[0] = leftB = new Rectangle((int)position.X, (int)position.Y, 10, 1500);

                // create rectangleArea
                levelArea = new Rectangle((int)position.X + 10, (int)position.Y + 10, 480, 1480);
            }

            else if (levelNumber == 4)
            {
                /// Map 1 Lvl 4
                /// bottom: P: x 500  y + 490 , Size: w 1500 x  h 10 
                rectangles[0] = bottomB = new Rectangle((int)position.X, (int)position.Y + 490, 1500, 10);

                // create rectangleArea
                levelArea = new Rectangle((int)position.X + 10, (int)position.Y + 10, 1480, 480);
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

            foreach(GameItem gi in mapPieces)
            {
                gi.Draw(spriteBatch);
            }

            if (updateShield != null)
                updateShield.Draw(spriteBatch);

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

            if (updateShield != null)
                updateShield.Update(gameTime);

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

                    // Add game item to game item list if enemy is destroyed
                    if (e.gameItem != null && e == endBoss)
                        mapPieces.Add(e.gameItem);
                    else if (e.gameItem != null && e != endBoss)
                        updateShield = (UpdateShield)e.gameItem;

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
                        else
                            teleport.isVisible = true;
                    }
                    break;
                case 2:
                    foreach (GameObject go in gameObjects)
                    {
                        if (!go.keyPressed)
                            return false;
                        else
                            teleport.isVisible = true;
                    }
                    break;
                case 3:
                    if (enemies.Any())
                        return false;
                    else
                        teleport.isVisible = true;

                    break;
                case 4:
                    if (!generator.damaged)
                        return false;
                    else
                        teleport.isVisible = true;

                    break;
                case 5:
                    if (endBoss.actualShield > 0)
                    {
                        return false;
                    }
                    else if (theHighlander.updateLevel < 2 && !isEndBossDestroyed)
                    {
                        theHighlander.updateLevel++;
                        isEndBossDestroyed = true;
                    }

                    if (endBoss.actualShield <= 0)
                        teleport.isVisible = true;

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
