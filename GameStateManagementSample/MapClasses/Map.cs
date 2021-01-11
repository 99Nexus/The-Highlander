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

        private bool isCompleted;

        private Texture2D teleportTexture;

        #endregion Fields

        //vector startposition
        //list von positionen(start,end)
        //(Bewegungsmuster) type als struct
        //enum von enemy class holen

        // max and mini enemies
        //positions from enemies

        #region Initialization

        public Map(TheHighlander theHighlander, int mNumber)
        {
            mapNumber = mNumber;
            levels = new Level[5];
            player = theHighlander;
        }

        ///assign the background directly in the Constructor
        public override void LoadContent(ContentManager content)
        {
            /*
            foreach(Level l in levels)
            {
                l.LoadContent(content);
            }
            */
            teleportTexture = content.Load<Texture2D>(@"graphics\objects_items\Crate");
        }

        //create Levels
        public void CreateLevels(ContentManager content)
        {
            int l = 1;
            for (int i = 0; i < levels.Length; i++)
            {
                //create new lvl and assign levelNumber
                levels[i] = new Level(l++, player);
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
                    lvl.gameObjects.Add(new ControlSystem(new Vector2(lvl.position.X + 45, lvl.position.Y + 400), player));
                    lvl.teleport = new Teleport(teleportTexture, new Vector2(lvl.position.X + 250, lvl.position.Y + 1440));
                    break;

                case 2:
                    lvl.position = new Vector2(position.X, position.Y + 1500);
                    lvl.spawnPosition = new Vector2(lvl.position.X + 250, lvl.position.Y + 250);
                    lvl.gameObjects.Add(new Alarm(new Vector2(lvl.position.X + 750, lvl.position.Y + 60), player));
                    lvl.teleport = new Teleport(teleportTexture, new Vector2(lvl.position.X + 1450, lvl.position.Y + 240));
                    break;

                case 3:
                    lvl.position = new Vector2(position.X + 1500, position.Y + 500);
                    lvl.spawnPosition = new Vector2(lvl.position.X + 250, lvl.position.Y + 1400);
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 40, lvl.position.Y + 40), player));
                    lvl.teleport = new Teleport(teleportTexture, new Vector2(lvl.position.X + 250, lvl.position.Y + 40));
                    break;

                case 4:
                    lvl.position = new Vector2(position.X + 500, position.Y);
                    lvl.spawnPosition = new Vector2(lvl.position.X + 1400, position.Y + 250);
                    lvl.gameObjects.Add(new Generator(new Vector2(lvl.position.X + 1000, lvl.position.Y + 70), player));
                    lvl.teleport = new Teleport(teleportTexture, new Vector2(lvl.position.X + 50, lvl.position.Y + 240));
                    break;

                case 5:
                    lvl.position = new Vector2(position.X + 500, position.Y + 500);
                    lvl.spawnPosition = new Vector2(lvl.position.X + 500, lvl.position.Y + 100);

                    lvl.enemies.Add(new Tanker(new Vector2(lvl.position.X + 500, lvl.position.Y + 500), 2, 1, 2f, new Vector2(lvl.position.X + 800, lvl.position.Y + 800),
                player.Position, 20.0, MovementMode.VERTICAL));

                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 250, lvl.position.Y + 150), player));
                    lvl.gameObjects.Add(new Crate(new Vector2(lvl.position.X + 200, lvl.position.Y + 300), player));

                    lvl.teleport = new Teleport(teleportTexture, new Vector2(lvl.position.X + 500, lvl.position.Y + 940));
                    break;
            }
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
                l.Update(gameTime,theHighlander);
            }
        }
        #endregion Update and Draw
    }
}
