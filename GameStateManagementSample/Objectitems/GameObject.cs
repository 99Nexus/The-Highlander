using GameStateManagement.Starships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameStateManagement.ObjectItem
{
    public abstract class GameObject
    {
        public Rectangle rectangle;
        public Vector2 position;
        public Texture2D texture;

        public TheHighlander player;
        public bool keyPressed;
        public Vector2 Origin;
        public int count = 0;

        private bool scoreObserver;

        public GameObject(Vector2 pos, TheHighlander theHighlander)
        {
            position = pos;
            player = theHighlander;
        }

        public abstract void LoadContent(ContentManager content);

        public double CalculateDistanceToPlayer()
        {
            Vector2 distanceVector = new Vector2(player.Position.X - position.X, player.Position.Y - position.Y);
            return distanceVector.Length();
        }

        public virtual void Update(GameTime gameTime)
        {
            //hier muss eine Bedingung hin und dann wird das Objekt dort visible
            if (!scoreObserver && Keyboard.GetState().IsKeyDown(Keys.E) && CalculateDistanceToPlayer() <= 80)
            {
                keyPressed = true;
                player.PlayerScore.Value += 250;
                scoreObserver = true;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, SpriteFont sprite)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0, Origin, 1, SpriteEffects.None, 0);
        }
    }
}