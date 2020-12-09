using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace GameStateManagement.MapClasses
{
    public class MainMap : MapStructure
    {
        public Map[] maps;

        public MainMap()
        {
            maps = new Map[4];
            position = new Vector2(0, 0);
        }

        public override void LoadContent(ContentManager content)
        {
            texture2D = content.Load<Texture2D>(@"mapGraphics\mainMap");
            //we can not declare Rectangle in Constructor
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture2D.Width, texture2D.Height);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, rectangle, Color.White);
        }
        /*
        public void LoadMapsTextures(ContentManager content)
        {
            maps[0].texture2D = content.Load<Texture2D>(@"mapGraphics\maps\map1");
            maps[1].texture2D = content.Load<Texture2D>(@"mapGraphics\maps\map2");
            maps[2].texture2D = content.Load<Texture2D>(@"mapGraphics\maps\map3");
            maps[3].texture2D = content.Load<Texture2D>(@"mapGraphics\maps\map4");
        }
        */

        //create Maps
        public void CreateMaps()
        {
            foreach (Map m in maps)
            {

            }
        }
    }
}
