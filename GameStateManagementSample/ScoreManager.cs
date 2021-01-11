#region File Description

//-----------------------------------------------------------------------------
// ScoreManager.cs
//amer
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using GameStateManagement.Starships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

#endregion Using Statements

namespace GameStateManagement
{
    public class ScoreManager
    {
        //the filename to save or load the score list to our system
        private static string _fileName = "d:\\score.xml";

        //this list to represent the first 5 high scores
        public List<Score> Highscore { get; private set; }
        
        //this list to save all scores
        public List<Score> Scores { get; private set; }

        //a Constructor to create a new Score List if there is no list
        public ScoreManager() : this(new List<Score>()) { }

        //if there is a score list then this Constructor will be called
        // and then will update the highscore list
        public ScoreManager(List<Score> Scores)
        {
            this.Scores = Scores;
            UpdateHighscore();
        }

        //update the highscore list
        public void UpdateHighscore()
        {
            Highscore = Scores.Take(5).ToList();
        }

        //add score to the score list and order it
        // then call the methode UpdateHighscore()
        public void Add(Score score)
        {
            Scores.Add(score);
            Scores = Scores.OrderByDescending(c => c.Value).ToList();
            UpdateHighscore();
        }

        // to load the xml file
        public static ScoreManager Load()
        {
            //if there is no file then call the Constructor to create the lists
            if (!File.Exists(_fileName))
            {
                return new ScoreManager();
            }

            //else then read the file and assing it to a ScoreManager with a call from the Constructor
            using (var reader = new StreamReader(new FileStream(_fileName, FileMode.Open)))
            {
                var serillizer = new XmlSerializer(typeof(List<Score>));
                var scores = (List<Score>)serillizer.Deserialize(reader);
                return new ScoreManager(scores);
            }
        }

        //this methode will overwirte or save the scores list
        public static void Save(ScoreManager gameMenuInfo)
        {
            using (var writer = new StreamWriter(new FileStream(_fileName, FileMode.Create)))
            {
                var serillizer = new XmlSerializer(typeof(List<Score>));
                serillizer.Serialize(writer, gameMenuInfo.Scores);
            }
        }
    }
}
