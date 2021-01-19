#region Using Statements
using System.Collections.Generic;
using GameStateManagement.Starships;
using GameStateManagement.ObjectItem;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameStateManagement.GameItems;
#endregion Using Statements

namespace GameStateManagement.MapClasses
{
    public class Map : MapStructure
    {
        #region Fields

        // Map have an variable position from the abstract class
        public Level[] levels;
        public int mapNumber;

        public Vector2 playerStartPosition;
        public TheHighlander player;

        public Vector2 EnemyStartPosition;
        public Vector2 EnemyEndPosition;
        public List<Vector2> enemiesPositions;

        public MovementMode movementMode;

        public int enemiesNumber;

        public bool isGameCompleted;

        // Mission
        public Mission mission;

        #endregion Fields

        #region Initialization

        public Map(TheHighlander theHighlander, int mNumber)
        {
            mapNumber = mNumber;
            levels = new Level[5];
            player = theHighlander;
        }

        ///assign the background directly in the Constructor
        public override void LoadContent(ContentManager content) { }

        //create Levels
        public void CreateLevels(ContentManager content)
        {
            int l = 1;
            for (int i = 0; i < levels.Length; i++)
            {
                //create new lvl and assign levelNumber
                levels[i] = new Level(l++, player, mapNumber);
                levels[i].LoadContent(content); //3
                SetPositions(levels[i], content);
            }
        }

        public void SetPositions(Level lvl, ContentManager content)
        {
            switch (lvl.levelNumber)
            {
                case 1:
                    lvl.position = new Vector2(position.X, position.Y);
                    playerStartPosition = (lvl.spawnPosition = new Vector2(lvl.position.X + 250, lvl.position.Y + 100));
                    lvl.teleport = new Teleport(new Vector2(lvl.position.X + 240, lvl.position.Y + 1435));

                    lvl.gameObjects.Add(new ControlSystem(new Vector2(lvl.position.X + 45, lvl.position.Y + 400), player));

                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 380, lvl.position.Y + 1200), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 140, lvl.position.Y + 900), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 150, lvl.position.Y + 1300), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 370, lvl.position.Y + 370), player));

                    break;

                case 2:
                    lvl.position = new Vector2(position.X, position.Y + 1500);
                    lvl.spawnPosition = new Vector2(lvl.position.X + 250, lvl.position.Y + 250);
                    lvl.teleport = new Teleport(new Vector2(lvl.position.X + 1435, lvl.position.Y + 240));

                    lvl.gameObjects.Add(new Alarm(new Vector2(lvl.position.X + 750, lvl.position.Y + 60), player));

                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 350, lvl.position.Y + 50), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 1350, lvl.position.Y + 100), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 800, lvl.position.Y + 275), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 1000, lvl.position.Y + 400), player));

                    break;

                case 3:
                    lvl.position = new Vector2(position.X + 1500, position.Y + 500);
                    lvl.spawnPosition = new Vector2(lvl.position.X + 250, lvl.position.Y + 1400);
                    lvl.teleport = new Teleport(new Vector2(lvl.position.X + 225, lvl.position.Y + 40));

                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 400, lvl.position.Y + 660), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 300, lvl.position.Y + 780), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 100, lvl.position.Y + 1300), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 150, lvl.position.Y + 440), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 170, lvl.position.Y + 975), player));

                    break;

                case 4:
                    lvl.position = new Vector2(position.X + 500, position.Y);
                    lvl.spawnPosition = new Vector2(lvl.position.X + 1400, position.Y + 250);
                    lvl.teleport = new Teleport(new Vector2(lvl.position.X + 50, lvl.position.Y + 235));

                    lvl.generator = new Generator(new Vector2(lvl.position.X + 1000, lvl.position.Y + 70), player);
                    lvl.generator.LoadContent(content);

                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 1300, lvl.position.Y + 350), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 550, lvl.position.Y + 400), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 740, lvl.position.Y + 280), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 370, lvl.position.Y + 140), player));

                    break;

                case 5:
                    lvl.position = new Vector2(position.X + 500, position.Y + 500);
                    lvl.spawnPosition = new Vector2(lvl.position.X + 500, lvl.position.Y + 100);
                    lvl.teleport = new Teleport(new Vector2(lvl.position.X + 500, lvl.position.Y + 940));

                    lvl.SetEndBoss(mapNumber);

                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 150, lvl.position.Y + 150), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 250, lvl.position.Y + 500), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 240, lvl.position.Y + 900), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 400, lvl.position.Y + 880), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 700, lvl.position.Y + 866), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 690, lvl.position.Y + 170), player));

                    break;
            }

            lvl.teleport.LoadContent(content);
            //when finish with rest positions call it from here
            lvl.SetEnemiesPositions();
            foreach (GameObject go in lvl.gameObjects)
            {
                go.LoadContent(content);
            }
            foreach (Enemy e in lvl.enemies)
            {
                e.LoadContent(content);
            }
            lvl.MakeBorders();




        }

        #endregion Initialization


        #region Update and Draw

        public override void Draw(SpriteBatch spriteBatch, SpriteFont sprite, GameTime gameTime)
        {
            spriteBatch.Draw(texture2D, position, Color.White);

            spriteBatch.DrawString(sprite, new string("Map " + mapNumber.ToString()), new Vector2(position.X + 50, position.Y + 50), Color.Black);

            foreach (Level l in levels)
            {
                l.Draw(spriteBatch, sprite, gameTime);
            }
        }

        public override void Update(GameTime gameTime, TheHighlander theHighlander)
        {
            foreach (Level l in levels)
            {
                l.Update(gameTime, theHighlander);
            }
            if (mapNumber == 4)
            {
                if (levels[4].isEndBossDestroyed)
                {
                    isGameCompleted = true;
                }
            }
        }
        #endregion Update and Draw
    }
}
