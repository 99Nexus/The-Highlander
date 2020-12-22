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
        //wenn wir eine Startposition in Map haben dann wozu brauchen wir
        //die spawnPosition?
        // methode for spawning
        // Player,enemies and objeckts Positions
        public Rectangle[] rectangles;
        public Rectangle leftB;
        public Rectangle rightB;
        public Rectangle topB;
        public Rectangle bottomB;

        #endregion Fields

        #region Initialization

        public Level(int levelNumber)
        {
            this.levelNumber = levelNumber;
            rectangles = new Rectangle[2];
        }

        //no need for this Method
        public override void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public void SetEndBoss(Enemy enemy, Vector2 EndBossPostion)
        {
            this.endBoss = enemy;
            this.endBoss.Position = new Vector2(this.position.X + 850, this.position.Y + 500);
            this.hasEndBoos = true;
        }

        #endregion Initialization

        #region Borders Initialization
        public void MakeBorders()
        {
            if (this.levelNumber == 5)
                return;

            if (this.levelNumber == 1)
            {
                /// Map 1 Lvl 1
                /// left: P: x 0  y 0 , Size: w 10 x  h 1500
                /// right: P: x 490  y 0 , Size: w 10 x  h 1500
                rectangles[0] = this.rightB = new Rectangle((int)position.X + 490, (int)position.Y, 10, 1500);
                rectangles[1] = this.leftB = new Rectangle((int)position.X, (int)position.Y, 10, 500);
            }

            else if (this.levelNumber == 2)
            {
                /// Map 1 Lvl 2
                /// left: P: x 0  y 1500 , Size: w 10 x  h 500
                /// top: P: x 500  y 1500 , Size: w 1500 x  h 10
                rectangles[0] = this.leftB = new Rectangle((int)position.X, (int)position.Y, 10, 500);
                rectangles[1] = this.topB = new Rectangle((int)position.X, (int)position.Y, 1500, 10);
            }

            else if (this.levelNumber == 3)
            {
                /// Map 1 Lvl 3
                /// left: P: x 1500  y 500 , Size: w 10 x  h 1500
                rectangles[0] = this.leftB = new Rectangle((int)position.X, (int)position.Y, 10, 1500);
            }

            else if (this.levelNumber == 4)
            {
                /// Map 1 Lvl 4
                /// bottom: P: x 500  y + 490 , Size: w 1500 x  h 10 
                rectangles[0] = this.bottomB = new Rectangle((int)position.X, (int)position.Y + 490, 1500, 10);
            }
        }
        #endregion Borders Initialization

        #region Update and Draw
        //no need for this Method
        public override void Draw(SpriteBatch spriteBatch){}

        //no need for this Method
        public void Draw(SpriteBatch spriteBatch, SpriteFont sprite)
        {
            spriteBatch.DrawString(sprite, new string("Level" + levelNumber.ToString()), position, Color.Black);
        }

        #endregion Update and Draw
    }
}
