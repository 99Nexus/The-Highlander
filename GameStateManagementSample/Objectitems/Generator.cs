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

        public Generator(Vector2 pos, TheHighlander theHighlander) : base(pos, theHighlander)
        {
            maxShield = 5;
            actualShield = maxShield;
            damageBuffer = 0;
            maxDamageBuffer = 20;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"graphics\objects_items\Generator");
            rectangle = new Rectangle((int)position.X - (texture.Width / 2),
                                      (int)position.Y - (texture.Height / 2),
                                      texture.Width,
                                      texture.Height);
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void UpdateActualShieldValue(int damage)
        {
            if (actualShield - damage < 1)
            {
                //public void ManageExplosions()
                actualShield = 0;
            }
            else
            {
                actualShield -= damage;

                // Update the shield string
                shieldString = actualShield + " | " + maxShield;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            /*
             * UpdateActualShieldValue
             */
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont sprite)
        {
            base.Draw(spriteBatch, sprite);
            spriteBatch.DrawString(sprite, new string("Destroy the \n generator"), new Vector2(position.X + 20, position.Y + 126), Color.Black);
        }
    }
}