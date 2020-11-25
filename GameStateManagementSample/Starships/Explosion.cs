#region File Description

//-----------------------------------------------------------------------------
// Explosion.cs
//amer
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion Using Statements

namespace GameStateManagement.Starships
{
    public class Explosion
    {
        public Texture2D texture;
        public Vector2 position;
        public float timer;
        //speed up or speed down the animation
        // higher number higher speed
        public float interval;
        public Vector2 origion;
        public int currentFrame, spriteWidth , spriteHeight;
        public Rectangle sourceRect;
        public bool isVisible;

        public Explosion(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
            texture = newTexture;
            timer = 0f;
            interval = 30f;
            currentFrame = 0;
            spriteWidth = 128;
            spriteHeight = 128;
            isVisible = true;
        }

        public void LoadContent(ContentManager Content)
        {

        }

        public void Update(GameTime gameTime)
        {
            //increase the timer by the number of millseconds since was last called
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        
        
            //check the timer more than the chosen interval
            if(timer > interval)
            {
                //show next frame
                currentFrame++;
                //rest timer
                timer = 0;
            }

            //if were on the last frame, make exploision invisible
            //rest back to the one before the first frame
            //(because currentframe++ is called next so the next frame will be 1)
            if (currentFrame == 17)
            {
                isVisible = false;
                currentFrame = 0;
            }

            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);

            origion = new Vector2(sourceRect.Width/4, sourceRect.Height/4);
            //origion = new Vector2(0,0);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(texture, position, sourceRect, Color.White, 0f,origion,1.0f,SpriteEffects.None,0);
                spriteBatch.End();
            }
        }
    }
}
