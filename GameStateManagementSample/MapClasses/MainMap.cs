#region Using Statements
using GameStateManagement.Starships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion Using Statements


namespace GameStateManagement.MapClasses
{
    public class MainMap : MapStructure
    {
        #region Fields
        public Map[] maps;
        //Borders
        public Rectangle[] rectangles;
        public Rectangle topMainBorder;
        public Rectangle leftMainBorder;
        public Rectangle verticalMainBorder;
        public Rectangle horizontalMainBorder;
        public Rectangle bottomMainBorder;
        public Rectangle rightMainBorder;

        public TheHighlander player;
        #endregion Fields

        #region Initialization
        public MainMap(ContentManager content, TheHighlander player)
        {
            rectangles = new Rectangle[6];
            rectangles[0] = this.topMainBorder = new Rectangle(0, 0, 4000, 10);
            rectangles[1] = this.leftMainBorder = new Rectangle(0, 0, 10, 4000);
            rectangles[2] = this.verticalMainBorder = new Rectangle(2000, 0, 10, 4000);
            rectangles[3] = this.horizontalMainBorder = new Rectangle(0, 2000, 4000, 10);
            rectangles[4] = this.bottomMainBorder = new Rectangle(0, 4000, 4000, 10);
            rectangles[5] = this.leftMainBorder = new Rectangle(4000, 0, 10, 4000);
            maps = new Map[4];
            position = new Vector2(0, 0);
            CreateMaps(content);
            this.player = player;
        }

        public override void LoadContent(ContentManager content)
        {
            texture2D = content.Load<Texture2D>(@"mapGraphics\mainMap");

            LoadMapsTextures(content);
        }

        public void LoadMapsTextures(ContentManager content)
        {
            int m = 1;
            for (int i = 0; i < this.maps.Length; i++)
            {
                maps[i].texture2D = content.Load<Texture2D>(@"mapGraphics\map" + m++);
            }
        }

        //create Maps
        public void CreateMaps(ContentManager content)
        {
            int m = 1;
            for (int i = 0; i < this.maps.Length; i++)
            {
                //create new lvl and assign map Number
                this.maps[i] = new Map(player, m++);
                SetPositions(this.maps[i]);
            }
        }

        public void SetPositions(Map map)
        {
            switch (map.mapNumber)
            {
                case 1:
                    map.position = new Vector2(0, 0);
                    break;
                case 2:
                    map.position = new Vector2(2000, 0);
                    break;
                case 3:
                    map.position = new Vector2(0, 2000);
                    break;
                case 4:
                    map.position = new Vector2(2000, 2000);
                    break;
            }
            map.CreateLevels();
        }

        #endregion Initialization

        #region Update and Draw

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Map m in maps)
            {
                m.Update(gameTime, player);
            }
        }
        #endregion Update and Draw
    }
}
