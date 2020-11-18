#region File Description

//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//amer
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using GameStateManagementSample;
using Microsoft.Xna.Framework.Graphics;

#endregion Using Statements

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
            : base("")//to make a header for the screen
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


            //MenuEntries.Add(back);
            MenuEntries.Add(fullscreenMenuEntry);

            MenuEntries.Add(musicMenuEntry);

            MenuEntries.Add(effectsMenuEntry);

            MenuEntries.Add(keyMapMenuEntry);

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

            if (GameStateManagementGame.Newgame.Graphics.IsFullScreen)
            {
                GameStateManagementGame.Newgame.Graphics.IsFullScreen = false;
                GameStateManagementGame.Newgame.Graphics.ApplyChanges();
            }
            else
            {
                GameStateManagementGame.Newgame.Graphics.IsFullScreen = true;
                GameStateManagementGame.Newgame.Graphics.ApplyChanges();
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