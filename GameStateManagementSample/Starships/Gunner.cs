using GameStateManagement.GameItems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement.Starships
{
    class Gunner : Enemy
    {
        public Gunner(Vector2 position, Vector2 end, Vector2 playerPosition, double keepDistanceToPlayer, MovementMode movementMode) : 
            base(position, 13, 2, 3f, end, playerPosition, keepDistanceToPlayer, movementMode, 3000)
        {
            gameItem = new MapPieces(new Vector2(1000, 3000), 2);
        }

        public override void LoadContent(ContentManager content)
        {
            MapPieces mapPiece;
            texture = new Texture2D[3];
            texture[0] = content.Load<Texture2D>(@"graphics\starships\gunner1");
            texture[1] = content.Load<Texture2D>(@"graphics\starships\gunner2");
            texture[2] = content.Load<Texture2D>(@"graphics\starships\gunner3");

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
