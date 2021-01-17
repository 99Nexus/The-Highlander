using GameStateManagement.GameItems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement.Starships
{
    public class Tanker : Enemy
    {
        public Tanker(Vector2 position, Vector2 end, Vector2 playerPosition, double keepDistanceToPlayer, MovementMode movementMode) : 
            base(position, 15, 2, 2f, end, playerPosition, keepDistanceToPlayer, movementMode, 1000)
        {
            gameItem = new MapPieces(new Vector2(1000,1000), 0);
        }

        public override void LoadContent(ContentManager content)
        {
            MapPieces mapPiece;
            texture = new Texture2D[3];
            texture[0] = content.Load<Texture2D>(@"graphics\starships\tanker1");
            texture[1] = content.Load<Texture2D>(@"graphics\starships\tanker2");
            texture[2] = content.Load<Texture2D>(@"graphics\starships\tanker3");

            mapPiece = (MapPieces)gameItem;
            mapPiece.LoadContent(content);

            base.LoadContent(content);
        }
    }
}
