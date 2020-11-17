#region Using Statements

using GameStateManagement.Starships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

#endregion Using Statements



namespace GameStateManagement.Screens
{
    /// <summary>
    //This Class will contain the Healthbar and Score
    /// </summary>
    /// 
    public class GameMenuInfo
    {

        private Texture2D texture;

        private SpriteFont score;

        private int playerScore;

        private string playerName;



        public GameMenuInfo(Texture2D texture, SpriteFont score, string playerName , int playerScore)
        {
            this.texture = texture;
            this.score = score;
            this.playerScore = playerScore;
            this.playerName = playerName;
        }



        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, string playerName, int playerScore)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(texture, new Vector2(15, 15), Color.White);
            spriteBatch.DrawString(score, playerName, new Vector2(GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Width - 125, 15), Color.White);
            spriteBatch.DrawString(score,"Score: " + playerScore, new Vector2(GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Width - 125, 30), Color.White);
            
            spriteBatch.End();
        }



    }

}
