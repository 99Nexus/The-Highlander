using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace GameStateManagement.MapClasses
{
    public class Mission
    {
        private String misString;
        private Vector2 position;
        private int missionNr;
        private bool isVisible;
        private bool keyPressed;

        // missions
        private SpriteFont missionMessage;

        public Mission(int missionNr)
        {
            this.missionNr = missionNr;
            misString = chooseMis(missionNr);
            position = new Vector2(100, 100);
            isVisible = true;
            keyPressed = false;
        }

        public void LoadContent(ContentManager Content)
        {
            /*missionen[0] = "Task 1: Destroy the control system of the section";
            missionen[1] = "Task 2: Turn off the alarm signal";
            missionen[2] = "Task 3: Destroy all enemy spaceships";
            missionen[3] = "Task 4: To go further, you need to turn of the generator";
            missionen[4] = "Task 5: Take out the final boss to get part of the starmap";*/
        }

        public String chooseMis(int missionNr)
        {
            switch(missionNr)
            {
                case 1:
                    return "Task 1: Destroy the control system of the section";

                case 2:
                    return "Task 2: Turn off the alarm signal";

                case 3:
                    return "Task 3: Destroy all enemy spaceships";

                case 4:
                    return "Task 4: To go further, you need to turn of the generator";

                case 5:
                    return "Task 5: Take out the final boss to get part of the starmap";
            }
            return "";
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            spriteBatch.DrawString(spriteFont, misString, position, Color.AliceBlue);
        }
    }
}
