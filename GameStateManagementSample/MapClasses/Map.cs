using System;
using System.Collections.Generic;
using System.Text;
using GameStateManagement.Starships;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameStateManagement.MapClasses
{
    public class Map : MapStructure
    {


        // Map have an variable from the abstract class
        // public Vector2 position;
        public Level[] levels;
        public int mapNumber;
        public List<Enemy> enemies;
        public Vector2 playerStartPosition;
        public Vector2 EnemyStartPosition;
        public Vector2 EnemyEndPosition;
        public List<Vector2> enemiesPositions;
        public MovementMode movementMode;
        public int enemiesNumber;
        private bool isCompleted;
        private bool openGate;
        //vector startposition
        //list von positionen(start,end)
        //(Bewegungsmuster) type als struct
        //enum von enemy class holen
        // max and mini enemies
        //positions from enemies
        public Map(Texture2D texture2D, Vector2 position, List<Enemy> enemies)
        {
            this.texture2D = texture2D;
            this.position = position;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, texture2D.Width, texture2D.Height);
            this.levels = new Level[5];
            this.enemies = enemies;
        }

        ///assign the background directly in the Constructor
        public override void LoadContent(ContentManager content)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, position, rectangle, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            ObserveEnemies();
        }

        public void ObserveEnemies()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].actualShield <= 0)
                    enemies.Remove(enemies[i]);
            }
        }

        public override bool Equals(object obj)
        {
            //make make an id for each created enemy
            return base.Equals(obj);
        }

        public void CompleteCheck()
        {
            foreach(Level le in levels)
            {
                if (le.hasEndBoos)
                {
                    if (le.endBossDefeated)
                    {
                        this.isCompleted = true;
                        this.openGate = true;
                    }
                }
            }
        }
        //create Levels
        public void CreateLevels()
        {
            //assign Map Number
            foreach(Level l in levels)
            {

            }
        }
        
        
        //load enemies
        public void LoadEnemies(MovementMode movementMode, int enemiesNumber, List<Vector2> enemiesPositions)
        {

        }
    }
}
