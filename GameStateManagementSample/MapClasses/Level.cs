using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using GameStateManagement.Starships;

namespace GameStateManagement.MapClasses
{
    public class Level : MapStructure
    {
        private Enemy endBoss;
        public bool hasEndBoos;
        private bool isCompleted;
        public bool endBossDefeated;
        public Vector2 spawnPosition;
        public int levelNumber;
        public int mapNumber;
        //wenn wir eine Startposition in Map haben dann wozu brauchen wir
        //die spawnPosition?

        public Rectangle gate;
        public Rectangle leftB;
        public Rectangle rightB;
        public Rectangle topB;
        public Rectangle bottomB;

        public Level(int levelNumber, int mapNumber)
        {
            this.levelNumber = levelNumber;
            this.mapNumber = mapNumber;
            MakeBorders();
            SetSpawnPosition(levelNumber);
        }


        public void MakeBorders()
        {
            if (this.levelNumber == 5) return;

            if (this.levelNumber == 1)
            {
                /// Map 1 Lvl 1
                /// left: P: x 0  y 0 , Size: w 10 x  h 150
                /// right: P: x 490  y 0 , Size: w 10 x  h 150
                /// top: P: x 0  y 0 , Size: w 10 x  h 150
                /// bottom: P: x 0  y 1490 , Size: w 10 x  h 150 gate
                this.leftB = new Rectangle((int)position.X,(int)position.Y,10,1500);
                this.rightB = new Rectangle((int)position.X, (int)position.Y, 10, 1500);
                this.topB = new Rectangle((int)position.X, (int)position.Y, 10, 500);
                this.gate = (this.bottomB = new Rectangle((int)position.X, (int)position.Y, 10, 500));
            }

            else if (this.levelNumber == 2)
            {
                /// Map 1 Lvl 2
                /// left: P: x 0  y 1500 , Size: w 10 x  h 500
                /// right: P: x 1490  y 1500 , Size: w 10 x  h 500 gate
                /// adjusted top: P: x 500  y 1500 , Size: w 1000 x  h 10
                /// bottom: P: x 0  y 1990 , Size: w 10 x  h 1500

                this.leftB = new Rectangle((int)position.X, (int)position.Y, 10, 500);
                this.gate = (this.rightB = new Rectangle((int)position.X, (int)position.Y, 10, 500));
                this.topB = new Rectangle((int)position.X, (int)position.Y, 1000, 10);
                this.bottomB = new Rectangle((int)position.X, (int)position.Y, 10, 1500);
            }

            else if (this.levelNumber == 3)
            {
                /// Map 1 Lvl 3
                /// adjusted left: P: x 1500  y 500 , Size: w 10 x  h 1000
                /// right: P: x 1990  y 500 , Size: w 10 x  h 1500
                /// top: P: x 1500  y 500 , Size: w 10 x  h 1500 gate
                /// bottom: P: x 1500  y 1990 , Size: w 10 x  h 1500

                this.leftB = new Rectangle((int)position.X, (int)position.Y, 10, 1000);
                this.rightB = new Rectangle((int)position.X, (int)position.Y, 10, 1500);
                this.gate = (this.topB = new Rectangle((int)position.X, (int)position.Y, 10, 500));
                this.bottomB = new Rectangle((int)position.X, (int)position.Y, 10, 500);
            }

            else if (this.levelNumber == 4)
            {
                /// Map 1 Lvl 4
                /// left: no need
                /// right: P: x 1990  y 0 , Size: w 10 x  h 500
                /// top: P: x 500  y 0 , Size: w 1500 x  h 10
                /// bottom: P: x 500  y 490 , Size: w 1000 x  h 10 gate
                
                this.rightB = new Rectangle((int)position.X, (int)position.Y, 10, 500);
                this.topB = new Rectangle((int)position.X, (int)position.Y, 1500, 10);
                this.gate = (this.bottomB = new Rectangle((int)position.X, (int)position.Y, 1000, 10));
            }
        }

        public void OpenGate()
        {
            this.gate = Rectangle.Empty;
        }

        public void SetSpawnPosition(int levelNumber)
        {
            switch (levelNumber)
            {
                case 1:
                    this.spawnPosition = new Vector2();
                    break;
            }
        }
        

        //no need for this Method
        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
        //no need for this Method
        public override void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }



        public void SetEndBoss(Enemy enemy, Vector2 EndBossPostion)
        {
            this.endBoss = enemy;
            this.endBoss.Position = new Vector2 (this.position.X + 850 , this.position.Y + 500 );
            this.hasEndBoos = true;
        }
    }
}
