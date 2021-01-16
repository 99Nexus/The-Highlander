#region Using Statements

using GameStateManagement.Starships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion Using Statements

namespace GameStateManagement.Screens
{
    /// <summary>
    //This Class will contain the Healthbar and Score
    /// </summary>
    public class GameMenuInfo
    {
        private Texture2D texture;
        private SpriteFont score;
        public Vector2 Position;
        public Rectangle Rectangle;

        private int playerScore;

        private string playerName;

        private TheHighlander player;


        public GameMenuInfo(Texture2D texture, SpriteFont score, string playerName , int playerScore, TheHighlander player)
        {
            this.texture = texture;
            this.score = score;
            this.playerScore = playerScore;
            this.playerName = playerName;
            this.player = player;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, string playerName, int playerScore)
        {
            Position = new Vector2(0,0);
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            spriteBatch.Draw(texture, Position, Color.White);

            Position = new Vector2(GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Width - 125, 15);
            spriteBatch.DrawString(score, playerName, Position, Color.White);

            //Position = new Vector2(GameStateManagementGame.Newgame.Graphics.GraphicsDevice.Viewport.Width - 125, 30);
            spriteBatch.DrawString(score,"Score: " + playerScore, Position, Color.White);
        }



    }

}
