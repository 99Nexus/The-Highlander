using GameStateManagement.Starships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement.ObjectItem
{
    public class Generator : GameObject
    {
        public int actualShield;
        private int maxShield;
        private string shieldString;
        public int damageBuffer;
        public int maxDamageBuffer;
        private Texture2D explosionTexture;
        private Texture2D texture2;

        public Generator(Vector2 pos, TheHighlander theHighlander) : base(pos, theHighlander)
        {
            actualShield = (maxShield = 3);
            damageBuffer = 0;
            maxDamageBuffer = 20;
            shieldString = actualShield + " | " + maxShield;
        }

        public override void LoadContent(ContentManager content)
        {
            explosionTexture = content.Load<Texture2D>(@"explosion");
            texture = content.Load<Texture2D>(@"graphics\objects_items\Generator");
            texture2 = content.Load<Texture2D>(@"graphics\objects_items\Generator_damaged");
            rectangle = new Rectangle((int)position.X - (texture.Width / 2), (int)position.Y - (texture.Height / 2), texture.Width, texture.Height);
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public new void UpdateActualShieldValue(int damage)
        {
            if (actualShield - damage < 0)
            {
                actualShield = 0;
                isVisible = false;
            }
            else
            {
                actualShield -= damage;
            }
                shieldString = actualShield + " | " + maxShield;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
             
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont sprite)
        {
            if (isVisible) { 
            base.Draw(spriteBatch, sprite);
                spriteBatch.DrawString(sprite, new string(shieldString + "Destroy the generator"), new Vector2(position.X - 75, position.Y + 65), Color.Black);
            }
            else
            {
                spriteBatch.Draw(texture2, position, null, Color.White, 0, Origin, 1, SpriteEffects.None, 0);
            }
        }
    }
}