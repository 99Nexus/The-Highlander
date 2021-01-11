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
        public List<Enemy> enemies;
        public Vector2 playerStartPosition;
        public TheHighlander player;
        public Vector2 EnemyStartPosition;
        public Vector2 EnemyEndPosition;
        public List<Vector2> enemiesPositions;
        public MovementMode movementMode;
        public int enemiesNumber;
        private bool isCompleted;

        private List<Explosion> explosions;
        private Texture2D explosionTexture;

        // Mission-Test
        private Texture2D teleportTexture;



        public List<GameObject> gameObjects;
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
            enemies = new List<Enemy>();
            explosions = new List<Explosion>();
            gameObjects = new List<GameObject>();
        }

        ///assign the background directly in the Constructor
        public override void LoadContent(ContentManager content)
        {
            explosionTexture = content.Load<Texture2D>(@"explosion");
            teleportTexture = content.Load<Texture2D>(@"graphics\objects_items\Crate");
        }

        public void SetPositions(Level lvl)
        {
            switch (lvl.levelNumber)
            {
                case 1:
                    lvl.position = new Vector2(position.X, position.Y);
                    playerStartPosition = (lvl.spawnPosition = new Vector2(lvl.position.X + 250, lvl.position.Y + 100));
                    gameObjects.Add(new ControlSystem(new Vector2(lvl.position.X + 45, lvl.position.Y + 400), player));
                    lvl.teleport = new Teleport(teleportTexture, new Vector2(lvl.position.X + 250, lvl.position.Y + 1440));
                    break;

                case 2:
                    lvl.position = new Vector2(position.X, position.Y + 1500);
                    lvl.spawnPosition = new Vector2(lvl.position.X + 250, lvl.position.Y + 250);
                    gameObjects.Add(new Alarm(new Vector2(lvl.position.X + 750, lvl.position.Y + 60), player));
                    lvl.teleport = new Teleport(teleportTexture, new Vector2(lvl.position.X + 1450, lvl.position.Y + 240));
                    break;

                case 3:
                    lvl.position = new Vector2(position.X + 1500, position.Y + 500);
                    lvl.spawnPosition = new Vector2(lvl.position.X + 250, lvl.position.Y + 1400);
                    gameObjects.Add(new Crate(new Vector2(lvl.position.X + 40, lvl.position.Y + 40), player));
                    lvl.teleport = new Teleport(teleportTexture, new Vector2(lvl.position.X + 250, lvl.position.Y + 40));
                    break;

                case 4:
                    lvl.position = new Vector2(position.X + 500, position.Y);
                    lvl.spawnPosition = new Vector2(lvl.position.X + 1400, position.Y + 250);
                    gameObjects.Add(new Generator(new Vector2(lvl.position.X + 1000, lvl.position.Y + 70), player));
                    lvl.teleport = new Teleport(teleportTexture, new Vector2(lvl.position.X + 50, lvl.position.Y + 240));
                    break;

                case 5:
                    lvl.position = new Vector2(position.X + 500, position.Y + 500);
                    lvl.spawnPosition = new Vector2(lvl.position.X + 500, lvl.position.Y + 100);
                    gameObjects.Add(new Generator(new Vector2(lvl.position.X + 750, lvl.position.Y + 100), player));
                    gameObjects.Add(new Crate(new Vector2(lvl.position.X + 250, lvl.position.Y + 150), player));
                    gameObjects.Add(new Crate(new Vector2(lvl.position.X + 200, lvl.position.Y + 300), player));
                    lvl.teleport = new Teleport(teleportTexture, new Vector2(lvl.position.X + 500, lvl.position.Y + 940));
                    break;
            }
            lvl.MakeBorders();
        }

        //create Levels
        public void CreateLevels()
        {
            int l = 1;
            for (int i = 0; i < levels.Length; i++)
            {
                //create new lvl and assign levelNumber
                levels[i] = new Level(l++);
                SetPositions(levels[i]);
            }
        }

        #endregion Initialization

        #region Loading Enemy & Manage Explosions

        public void ObserveEnemies()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].actualShield <= 0)
                {
                    explosions.Add(new Explosion(explosionTexture, new Vector2(enemies[i].Position.X - 50, enemies[i].Position.Y - 25)));
                    enemies.Remove(enemies[i]);
                    player.PlayerScore.Value += 150;
                }
            }
        }

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
        public void LoadEnemies(MovementMode movementMode, int enemiesNumber, List<Vector2> enemiesPositions)
        {

        }
        #endregion Loading Enemy & Manage Explosions 

        #region Update and Draw

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, position, Color.White);
            foreach (Explosion ex in explosions)
            {
                ex.Draw(spriteBatch);
            }

        }
        //for test
        public void Draw(SpriteBatch spriteBatch, SpriteFont sprite)
        {
            spriteBatch.DrawString(sprite, new string("Map " + mapNumber.ToString()), new Vector2(position.X + 50, position.Y + 50), Color.Black);
            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch, sprite);
            }

            foreach(Level l in levels)
            {
                l.Draw(spriteBatch, sprite);
                l.teleport.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime, TheHighlander theHighlander)
        {
            foreach (Enemy e in enemies)
            {
                e.Update(gameTime, theHighlander.Position);
            }

            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);
            }

            foreach (Level l in levels)
            {
                //l.Update();
            }

            foreach (Explosion ex in explosions)
            {
                ex.Update(gameTime);
            }
            ObserveEnemies();
            ManageExplosions();
        }

        public void CompleteCheck()
        {
            foreach (Level le in levels)
            {
                if (le.hasEndBoos)
                {
                    if (le.endBossDefeated)
                    {
                        isCompleted = true;
                    }
                }
            }
        }
        #endregion Update and Draw
    }
}
