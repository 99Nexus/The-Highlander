#region File Description

//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

using GameStateManagementSample;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    internal class OptionsMenuScreen : MenuScreen
    {
        #region Fields


        private MenuEntry fullscreenMenuEntry;

        private MenuEntry musicMenuEntry;

        private MenuEntry effectsMenuEntry;

        private MenuEntry keyMapMenuEntry;


        /*scratch maybe we will need it later
         * idea switch keyMap between 2 options
        public struct KeyMapping
        {
            public static string[] KeyMapdefault = { "W", "S", "D", "A", "Space" };
            public static string[] KeyMapArrows = { "Up", "Down", "Right", "Left", "Space" };
        }{ "forward: W", "backward: S", "right: D", "link: A" };
        */
        private static string[] KeyMapping = { "Move Forward \"W\"", "Move Backward \"S\"",
                                                "Move Right \"D\"", "Move Left \"A\"" };


        
        private static bool currentfullscreen = false;

        private static bool currentmusic = true;

        private static bool currenteffecs = true;

        private static int currentKeyMap = 0;

 


        #endregion Fields

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("")//"Options"
        {

            // Create our menu entries.
            fullscreenMenuEntry = new MenuEntry(string.Empty);

            musicMenuEntry = new MenuEntry(string.Empty);

            effectsMenuEntry = new MenuEntry(string.Empty);

            keyMapMenuEntry = new MenuEntry(string.Empty);


            SetMenuEntryText();

            fullscreenMenuEntry.Selected += FullscreenMenuEntrySelected;

            musicMenuEntry.Selected += MusicMenuEntrySelected;

            effectsMenuEntry.Selected += EffectsMenuEntrySelected;

            keyMapMenuEntry.Selected += KeyMapMenuEntrySelected;



            MenuEntries.Add(fullscreenMenuEntry);

            MenuEntries.Add(musicMenuEntry);

            MenuEntries.Add(effectsMenuEntry);

            MenuEntries.Add(keyMapMenuEntry);

            //MenuEntries.Add(back);
        }

        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        private void SetMenuEntryText()
        {
            fullscreenMenuEntry.Text = "Full Screen: " + (currentfullscreen ? "on" : "off");

            musicMenuEntry.Text = "Music: " + (currentmusic ? "unmuted" : "muted");

            effectsMenuEntry.Text = "Effects: " + (currenteffecs ? "unmuted" : "muted");

            keyMapMenuEntry.Text = "KeyMap: " + string.Join(",",KeyMapping[currentKeyMap]);

        }

        #endregion Initialization

        #region Handle Input


        /// <summary>
        /// Event handler for when the Fullscreen menu entry is selected.
        /// </summary>

        private void FullscreenMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentfullscreen = !currentfullscreen;

            if (GameStateManagementGame.newgame.graphics.IsFullScreen)
            {
                GameStateManagementGame.newgame.graphics.IsFullScreen = false;
                GameStateManagementGame.newgame.graphics.ApplyChanges();
            }
            else
            {
                GameStateManagementGame.newgame.graphics.IsFullScreen = true;
                GameStateManagementGame.newgame.graphics.ApplyChanges();
            }
            

            SetMenuEntryText();

            
        }


        /// <summary>
        /// Event handler for when the Music menu entry is selected.
        /// </summary>
        private void MusicMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {

            currentmusic = !currentmusic;
            SetMenuEntryText();

        }


        /// <summary>
        /// Event handler for when the effects menu entry is selected.
        /// </summary>
        private void EffectsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {

            currenteffecs = !currenteffecs;
            SetMenuEntryText();

        }


        /// <summary>
        /// Event handler for when the effects menu entry is selected.
        /// </summary>
        private void KeyMapMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {

            currentKeyMap = (currentKeyMap + 1) % KeyMapping.Length;

            SetMenuEntryText();

        }

        #endregion Handle Input
    }
}