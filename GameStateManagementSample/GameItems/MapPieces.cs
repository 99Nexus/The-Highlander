using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStateManagement.GameItems
{
    class MapPieces : GameItem
    {
        private int spriteCounter;
        private Texture2D[] pieces;
        private Vector2 position;

        public MapPieces(Vector2 pos, int which)
        {
            this.position = pos;
            pieces = new Texture2D[4];
            spriteCounter = which;
        }

        public void LoadContent(ContentManager content)
        {
            pieces[0] = content.Load<Texture2D>(@"graphics\game_items\MapPiece1");
            pieces[1] = content.Load<Texture2D>(@"graphics\game_items\MapPiece2");
            pieces[2] = content.Load<Texture2D>(@"graphics\game_items\MapPiece3");
            pieces[3] = content.Load<Texture2D>(@"graphics\game_items\MapPiece4");


            rectangle = new Rectangle((int)position.X - (pieces[spriteCounter].Width / 2),
                                      (int)position.Y - (pieces[spriteCounter].Height / 2),
                                      pieces[spriteCounter].Width,
                                      pieces[spriteCounter].Height);
            Origin = new Vector2(pieces[spriteCounter].Width / 2, pieces[spriteCounter].Height / 2);
            Origin = new Vector2(pieces[spriteCounter].Width / 2, pieces[spriteCounter].Height / 2);

            base.LoadContent(content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(pieces[spriteCounter] != null)
                spriteBatch.Draw(pieces[spriteCounter], position, null, Color.White, 0, Origin, 1, SpriteEffects.None, 0);
        }
    }
}
