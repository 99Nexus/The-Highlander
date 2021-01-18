using GameStateManagement.GameItems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement.Starships
{
    public class GunnerShip : Enemy
    {
        public GunnerShip(Vector2 position,
        Vector2 end, Vector2 playerPosition, double keepDistanceToPlayer, MovementMode movementMode) :
        base(position, 2, 2, 3f, end, playerPosition, keepDistanceToPlayer, movementMode, 200)
        { }

        public override void LoadContent(ContentManager content)
        {
            UpdateShield updateShield;
            texture = new Texture2D[3];
            texture[0] = content.Load<Texture2D>(@"graphics\starships\gunnerShip1");
            texture[1] = content.Load<Texture2D>(@"graphics\starships\gunnerShip2");
            texture[2] = content.Load<Texture2D>(@"graphics\starships\gunnerShip3");

            if (gameItem != null)
            {
                updateShield = (UpdateShield)gameItem;
                updateShield.LoadContent(content);
            }

            base.LoadContent(content);
        }
    }
}
