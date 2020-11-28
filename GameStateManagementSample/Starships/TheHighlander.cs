using GameStateManagement.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagement.Starships
{
    public class TheHighlander
    {
        #region Fields

        // Graphical attributes
        public Texture2D[] texture;
        public Rectangle highlanderBox;
        public bool isVisible;
        public int spriteCounter = 0;
        public Score Player { get; set; }
        public string playerName = "Player1";

        // State attributes
        public float linearVelocity = 4f;
        public int updateLevel = 0;
        public int shield = 10;
        public int score;

        // Shoot
        public Vector2 speed;
        public Texture2D laserTexture;
        public float laserDelay;
        public List<Laser> laserList;

        // Movement attributes
        private float rotation;
        public float rotationVelocity = 3f;
        public Vector2 Origin;
        public Rectangle Rectangle;
        public Vector2 Position;

        //amer, liber das lassen für testen
        private SpriteFont sprite;
        //public int PlayerScore { get; set; }

    
        #endregion Fields

        #region Initialization

        public TheHighlander(SpriteFont sprite, string playerName, int playerScore)
        {
            laserList = new List<Laser>();
            this.sprite = sprite;
            laserDelay = 20;
            Player = new Score(playerName, playerScore);
            this.isVisible = true;
        }

        public void LoadContent(ContentManager content)
        {
            texture = new Texture2D[3];
            texture[0] = content.Load<Texture2D>(@"graphics\starships\the_highlander_1");
            texture[1] = content.Load<Texture2D>(@"graphics\starships\the_highlander_2");
            texture[2] = content.Load<Texture2D>(@"graphics\starships\the_highlander_3");

            Origin = new Vector2(texture[spriteCounter].Width / 2, texture[spriteCounter].Height / 2);

            laserTexture = content.Load<Texture2D>(@"graphics\game_objects\theHighlanderLaser");
        }

        #endregion Initialization

        #region Update and Draw

        public void Update(GameTime gameTime)
        {
           highlanderBox = new Rectangle((int)Position.X, (int)Position.Y, texture[spriteCounter].Width, texture[spriteCounter].Height);
           Rectangle = new Rectangle((int)Position.X, (int)Position.Y, texture[spriteCounter].Width, texture[spriteCounter].Height);
        }

        //This Methode will check the Position, whether is vaild or not  
        public bool IsValid()
        {

            if (Position.Y > GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Height - 35 || Position.Y < 25.0)
            {
                return false;
                
            }
            else if (Position.X > GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Width - 45  || Position.X < 25.0)
            {
                return false;
            }

            return true;
        }


        public void HandleInput()
        {            
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                rotation -= MathHelper.ToRadians(rotationVelocity);
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                rotation += MathHelper.ToRadians(rotationVelocity);

            Vector2 direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(90) - rotation), -(float)Math.Sin(MathHelper.ToRadians(90) - rotation));


            //First check whether the screen in Fullscreen or Windows size mode is
            //then if the Player click "w" the methode Mover() will be called
            if (!GameStateManagementGame.Newgame.Graphics.IsFullScreen) { 
                if (Keyboard.GetState().IsKeyDown(Keys.W)) {
                    Move(direction);
                }
            }
            //In FullScreen case
            else if (Keyboard.GetState().IsKeyDown(Keys.W)) {
                Move(direction);
            }

            //shoot
            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Shoot();

            }
            updateLaser();
        }

        //Here will be the Position checked, whether is vaild or not, by calling the
        //Methode IsValid()
        // If the Ship across the screen's borders then the ship will be replaced
        // in the middle of the screen (respawn)
        public void Move( Vector2 direction)
        {
            if (IsValid())
            {
                Position += direction * linearVelocity;
                Player.Value++;
            }
            else
            {
                Position = new Vector2(GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Width / 2,
                       GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Height -50);
                Player.Value = Player.Value - 200;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Count up to change sprite
            if (spriteCounter < 2)
                spriteCounter++;
            else
                spriteCounter = 0;

            spriteBatch.Draw(texture[spriteCounter], Position, null, Color.White, rotation, Origin, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(sprite, new string("Y " + Position.Y.ToString()), new Vector2(30, 100), Color.Black);
            spriteBatch.DrawString(sprite, new string("X " + Position.X.ToString()), new Vector2(30, 130), Color.Black);

            //shoot
            foreach(Laser l in laserList)
            {
                l.Draw(spriteBatch);
            }
        }

        #endregion Update and Draw

        #region Shoot
        public void Shoot()
        {
            if(laserDelay >= 0)
            {
                laserDelay--;
            }

            if(laserDelay <= 0)
            {
                Laser newLaser = new Laser(laserTexture);
                newLaser.position = new Vector2(Position.X + 30 - newLaser.texture.Width / 2, Position.Y + 30);

                newLaser.isVisible = true;

                if(laserList.Count() < 20)
                {
                    laserList.Add(newLaser);
                }
            }

            if(laserDelay == 0)
            {
                laserDelay = 20;
            }
        }

        public void updateLaser()
        {
            foreach(Laser l in laserList)
            {
                l.position.Y = l.position.Y - l.speed;

                if(l.position.Y <= 0)
                {
                    l.isVisible = false;
                }

                for(int i = 0; i < laserList.Count; i++)
                {
                    if(!laserList[i].isVisible)
                    {
                        laserList.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        #endregion Shoot
    }
}
