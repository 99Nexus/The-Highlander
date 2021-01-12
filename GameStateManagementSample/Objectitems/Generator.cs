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
        private Texture2D GeneratorDamagedTexture;
        public bool damaged;

        public Generator(Vector2 pos, TheHighlander theHighlander) : base(pos, theHighlander)
        {
            actualShield = (maxShield = 3);
            damageBuffer = 0;
            maxDamageBuffer = 20;
            shieldString = actualShield + " | " + maxShield;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"graphics\objects_items\Generator");
            GeneratorDamagedTexture = content.Load<Texture2D>(@"graphics\objects_items\Generator_damaged");
            rectangle = new Rectangle((int)position.X - (texture.Width / 2), (int)position.Y - (texture.Height / 2), texture.Width, texture.Height);
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void UpdateActualShieldValue(int damage)
        {
            if (actualShield - damage < 0 && !damaged)
            {
                actualShield = 0;
                player.PlayerScore.Value += 300;
                damaged = true;
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
            if (damaged)
            {
                spriteBatch.Draw(GeneratorDamagedTexture, position, null, Color.White, 0, Origin, 1, SpriteEffects.None, 0);
            }
            else
            {
                base.Draw(spriteBatch, sprite);
                spriteBatch.DrawString(sprite, new string(shieldString + "Destroy the generator"), new Vector2(position.X - 75, position.Y + 65), Color.Black);
            }
        }
    }
}