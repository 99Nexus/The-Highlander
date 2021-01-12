using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagement.Starships;

namespace GameStateManagement.ObjectItem
{
    public class Alarm : GameObject
    {
        private int spriteCounter = 0;
        private Texture2D[] alarmList; 

        public Alarm(Vector2 pos, TheHighlander theHighlander) : base(pos, theHighlander)
        {
            alarmList = new Texture2D[2];
        }

        public override void LoadContent(ContentManager content)
        {
            alarmList[0] = content.Load<Texture2D>(@"graphics\objects_items\AlarmSystem");
            alarmList[1] = content.Load<Texture2D>(@"graphics\objects_items\AlarmSystem2");
            rectangle = new Rectangle((int)position.X - (alarmList[spriteCounter].Width / 2),
                                      (int)position.Y - (alarmList[spriteCounter].Height / 2),
                                      alarmList[spriteCounter].Width,
                                      alarmList[spriteCounter].Height);
            Origin = new Vector2(alarmList[spriteCounter].Width / 2, alarmList[spriteCounter].Height / 2);
            Origin = new Vector2(alarmList[spriteCounter].Width / 2, alarmList[spriteCounter].Height / 2);
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont sprite)
        {
            spriteBatch.Draw(alarmList[spriteCounter], position, null, Color.White, 0, Origin, 1, SpriteEffects.None, 0);

            if (CalculateDistanceToPlayer() <= 120 && !keyPressed)
            {
                spriteBatch.DrawString(sprite, new string("Press 'E' to set \n off the alarm"), new Vector2(position.X - 50, position.Y + 60), Color.Black);
            }

            if(keyPressed && CalculateDistanceToPlayer() <= 80)
            {
                spriteCounter = 1;
            }
        }
    }
}