#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion Using Statements



namespace GameStateManagement.MapClasses
{
    public abstract class MapStructure
    {
        #region Fields
        public Vector2 position { get; set; }
        public Texture2D texture2D { get; set; }
        #endregion Fields

        #region Initialization
        public MapStructure()
        {

        }

        public abstract void LoadContent(ContentManager content);


        public abstract void Draw(SpriteBatch spriteBatch);
        #endregion Initialization
    }



}
