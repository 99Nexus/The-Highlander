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
using GameStateManagement.GameObjects;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using GameStateManagement.Starships;
using GameStateManagement.GameItems;
#endregion Using Statements

namespace GameStateManagement.MapClasses
{
    public class Level : MapStructure
    {
        #region Fields
        
        private Enemy endBoss;
        public bool hasEndBoos;
        private bool isCompleted;
        public bool endBossDefeated;
        public Vector2 spawnPosition;
        public int levelNumber;
        public int mapNumber;
        public Rectangle[] rectangles;
        public Rectangle leftB;
        public Rectangle rightB;
        public Rectangle topB;
        public Rectangle bottomB;

        //Items
        public Teleport teleport;

        #endregion Fields

        #region Initialization

        public Level(int levelNumber)
        {
            this.levelNumber = levelNumber;
            rectangles = new Rectangle[2];
        }

        //no need for this Method
        public override void LoadContent(ContentManager content) { }

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
        //no need for this Method

        public override void Draw(SpriteBatch spriteBatch) { }

        //no need for this Method
        public void Draw(SpriteBatch spriteBatch, SpriteFont sprite)
        {
            spriteBatch.DrawString(sprite, new string("Level" + levelNumber.ToString()), position, Color.Black);
           if (!(teleport.texture is null) )
            {
                teleport.Draw(spriteBatch);
            }
        }
        #endregion Update and Draw
    }
}
