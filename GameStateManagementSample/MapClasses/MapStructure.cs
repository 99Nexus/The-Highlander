using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;



namespace GameStateManagement.MapClasses
{
    public abstract class MapStructure
    {
        public Vector2 position { get; set; }
        public Texture2D texture2D { get; set; }
        public MapStructure()
        {

        }

        public abstract void LoadContent(ContentManager content);


        public abstract void Draw(SpriteBatch spriteBatch);

    }



}
