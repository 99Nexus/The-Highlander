#region File Description

//-----------------------------------------------------------------------------
// LoadingScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Net.Mime;
using System.Threading;

#endregion Using Statements

namespace GameStateManagement
{
    /// <summary>
    /// The loading screen coordinates transitions between the menu system and the
    /// game itself. Normally one screen will transition off at the same time as
    /// the next screen is transitioning on, but for larger transitions that can
    /// take a longer time to load their data, we want the menu system to be entirely
    /// gone before we start loading the game. This is done as follows:
    ///
    /// - Tell all the existing screens to transition off.
    /// - Activate a loading screen, which will transition on at the same time.
    /// - The loading screen watches the state of the previous screens.
    /// - When it sees they have finished transitioning off, it activates the real
    ///   next screen, which may take a long time to load its data. The loading
    ///   screen will be the only thing displayed while this load is taking place.
    /// </summary>
    internal class LoadingScreen : GameScreen
    {
        #region Fields

        ContentManager content;

        private bool loadingIsSlow;
        private bool otherScreensAreGone;

        private int emergencyTimer = 0;
        private int dialog_count = 0;

        private Texture2D message;

        // Alarm_Icon
        private Texture2D alarm;

        private SpriteFont dialogSprite;

        // Booleans for next steps
        private bool isSPressed = false;
        private bool isMPressed = false;
        private bool isEmergencyTimeOver = false;

        // Messages
        private string loadingMessage1 = "Initiate    Starship    System    E.X.O.";
        private string loadingMessage2 = "Press    ' S '    to    start    the    system";
        private string emergency = "EMERGENCY";
        private SpriteFont emergencyMessage;


        // Dialog
        /*private string[] dialog_list = {"Commander Smith: CAPTAIN..? CAPTAIN.. are you okay?!\n" +
                "Press 'M'",

        "Commander Smith: I'm glad to hear that you are fine.\n" +
                "Press 'M'",

       "Commander Smith: You must complete the mission. You are our last hope...\n" +
                "Press 'ENTER'"};*/

        private string dialog1 = "Commander Smith: CAPTAIN..? CAPTAIN.. are you okay?!\n";
        private string dialog2 = "I'm glad to hear that you are fine.\n";
        private string dialog3 = "You must complete the mission. You are our last hope...";
        private string enter = "PRESS ENTER";

        private string dialog = " ";

        private GameScreen[] screensToLoad;

        #endregion Fields

        #region Initialization

        /// <summary>
        /// The constructor is private: loading screens should
        /// be activated via the static Load method instead.
        /// </summary>
        private LoadingScreen(ScreenManager screenManager, bool loadingIsSlow,
                              GameScreen[] screensToLoad)
        {
            this.loadingIsSlow = loadingIsSlow;
            this.screensToLoad = screensToLoad;

            TransitionOnTime = TimeSpan.FromSeconds(2.0);
        }

        /// <summary>
        /// Activates the loading screen.
        /// </summary>
        public static void Load(ScreenManager screenManager, bool loadingIsSlow,
                                PlayerIndex? controllingPlayer,
                                params GameScreen[] screensToLoad)
        {
            // Tell all the current screens to transition off.
            foreach (GameScreen screen in screenManager.GetScreens())
                screen.ExitScreen();

            // Create and activate the loading screen.
            LoadingScreen loadingScreen = new LoadingScreen(screenManager,
                                                            loadingIsSlow,
                                                            screensToLoad);

            
            screenManager.AddScreen(loadingScreen, controllingPlayer);
        }

        public override void LoadContent()
        {
            content = new ContentManager(ScreenManager.Game.Services, "Content");

            emergencyMessage = content.Load<SpriteFont>(@"spritefonts\screen_fonts\start_screen_font");
            dialogSprite = content.Load<SpriteFont>(@"spritefonts\screen_fonts\dialog_font");

            //alarm = content.Load<Texture2D>(@"graphics\screen_graphics\alarm_icon");
            

            //load the message-pic
            message = content.Load<Texture2D>("message");
        }  

        #endregion Initialization

        #region Update and Draw

        /// <summary>
        /// Updates the loading screen.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                loadingMessage1 = " ";
                loadingMessage2 = " ";
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                // If all the previous screens have finished transitioning
                // off, it is time to actually perform the load.
                if (otherScreensAreGone)
                {
                    ScreenManager.RemoveScreen(this);

                    foreach (GameScreen screen in screensToLoad)
                    {
                        if (screen != null)
                        {
                            ScreenManager.AddScreen(screen, ControllingPlayer);
                        }
                    }

                    // Once the load has finished, we use ResetElapsedTime to tell
                    // the  game timing mechanism that we have just finished a very
                    // long frame, and that it should not try to catch up.
                    ScreenManager.Game.ResetElapsedTime();
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                isMPressed = true;
                dialog = dialog1;
                dialog += dialog2;
                dialog += dialog3;
            }
            
        }
        /// <summary>
        /// Draws the loading screen.
        /// </summary>
        /// 

        public override void Draw(GameTime gameTime)
        {
            // If we are the only active screen, that means all the previous screens
            // must have finished transitioning off. We check for this in the Draw
            // method, rather than in Update, because it isn't enough just for the
            // screens to be gone: in order for the transition to look good we must
            // have actually drawn a frame without them before we perform the load.
            if ((ScreenState == ScreenState.Active) &&
                (ScreenManager.GetScreens().Length == 1))
            {
                otherScreensAreGone = true;
            }

            // The gameplay screen takes a while to load, so we display a loading
            // message while that is going on, but the menus load very quickly, and
            // it would look silly if we flashed this up for just a fraction of a
            // second while returning from the game to the menus. This parameter
            // tells us how long the loading is going to take, so we know whether
            // to bother drawing the message.
            if (loadingIsSlow)
            {
                SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
                SpriteFont font = ScreenManager.Font;

                // Center the text in the viewport.
                Viewport viewport = ScreenManager.GraphicsDevice.Viewport;

                // Loading message 1
                Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
                Vector2 textSizeMessage1 = font.MeasureString(loadingMessage1);
                Vector2 textPositionMessage1 = (viewportSize - textSizeMessage1) / 2;

                // Loading message 2
                viewportSize = new Vector2(viewport.Width, viewport.Height+70);
                Vector2 textSizeMessage2 = font.MeasureString(loadingMessage2);
                Vector2 textPositionMessage2 = (viewportSize - textSizeMessage2) / 2;

                // Emergency message 2
                viewportSize = new Vector2(viewport.Width, viewport.Height - 200);
                Vector2 textSizeEmergencyMessage = emergencyMessage.MeasureString(emergency);
                Vector2 textPositionEmergencyMessage = (viewportSize - textSizeEmergencyMessage) / 2;

                // Message 
                viewportSize = new Vector2(viewport.Width, viewport.Height + 50);
                Vector2 sizeMessage = new Vector2(message.Width, message.Height);
                Vector2 positionMessage = (viewportSize - sizeMessage) / 2;

                // Dialog 
                viewportSize = new Vector2(viewport.Width - 20, viewport.Height + 50);
                Vector2 textSizeDialog = dialogSprite.MeasureString(dialog);
                Vector2 textPositionDialog = (viewportSize - textSizeDialog) / 2;

                // Alarm-Position
                viewportSize = new Vector2(viewport.Width, viewport.Height - 10);
                Vector2 pictureSizeAlarm = emergencyMessage.MeasureString(emergency);
                Vector2 picturePositionAlarm = (viewportSize - textSizeEmergencyMessage) / 2;

                // Enter
                viewportSize = new Vector2(viewport.Width + 80, viewport.Height + 200);
                Vector2 textSizePressEnter = dialogSprite.MeasureString(enter);
                Vector2 textPositionPressEnter = (viewportSize - textSizeEmergencyMessage) / 2;


                Color color = Color.Green * TransitionAlpha;


                // Draw the text.
                spriteBatch.Begin();

                // 1.Section - Begin - Loading text
                spriteBatch.DrawString(font, loadingMessage1, textPositionMessage1, color);
                spriteBatch.DrawString(font, loadingMessage2, textPositionMessage2, color);

                // Loading ends if "S" is pressed
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    isSPressed = true;

                // 1.Section - End - Loading text


                //2.Section - Begin - Emergency screen

                if(isSPressed == true)
                {
                    ScreenManager.GraphicsDevice.Clear(Color.Red);
                    //spriteBatch.Draw(alarm, picturePositionAlarm, Color.White);
                    spriteBatch.DrawString(emergencyMessage, emergency, textPositionEmergencyMessage, Color.White);
                    emergencyTimer++;
                }

                //2.Section - End - Emergency screen

               

                //3.Section - Begin - Commnication request

                // draw message symbol after 5 seconds
                if (emergencyTimer == 300)
                    isEmergencyTimeOver = true;

                

                if (isEmergencyTimeOver == true)
                {
                    spriteBatch.Draw(message, positionMessage, Color.White);
                }

                //3.Section - End - Commnication request


                //4.Section - Begin - Commander text
                if (isMPressed)
                {

                    isEmergencyTimeOver = false;
                    spriteBatch.DrawString(dialogSprite, dialog, textPositionDialog, Color.White);
                    spriteBatch.DrawString(dialogSprite, enter, textPositionPressEnter, Color.White);
                }


                //4.Section - End - Commander text

                spriteBatch.End();
            }
        }

        #endregion Update and Draw
    }
}