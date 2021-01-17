using GameStateManagement.GameItems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement.Starships
{
    public class Doomer : Enemy
    {
        public Doomer(Vector2 position,
            Vector2 end, Vector2 playerPosition, double keepDistanceToPlayer, MovementMode movementMode) :
            base(position, 17, 2, 4f, end, playerPosition, keepDistanceToPlayer, movementMode, 5000)
        {
            gameItem = new MapPieces(new Vector2(3000, 3000), 3);
        }

        public override void LoadContent(ContentManager content)
        {
            MapPieces mapPiece;
            texture = new Texture2D[3];
            texture[0] = content.Load<Texture2D>(@"graphics\starships\doomer1");
            texture[1] = content.Load<Texture2D>(@"graphics\starships\doomer2");
            texture[2] = content.Load<Texture2D>(@"graphics\starships\doomer3");

            mapPiece = (MapPieces)gameItem;
            mapPiece.LoadContent(content);

            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime, Vector2 playerPosition)
        {
            base.Update(gameTime, playerPosition);
        }
    }
}
