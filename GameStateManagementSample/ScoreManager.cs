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
        private static string _fileName = "score.xml";
        public List<Score> Highscore { get; private set; }
        public List<Score> Scores { get; private set; }


        public ScoreManager() : this(new List<Score>()) { }


        public ScoreManager(List<Score> Scores)
        {
            this.Scores = Scores;
            UpdateHighscore();
        }

        public void UpdateHighscore()
        {
            Highscore = Scores.Take(5).ToList();
        }

        public void Add(Score score)
        {
            Scores.Add(score);
            Scores = Scores.OrderByDescending(c => c.Value).ToList();
            UpdateHighscore();
        }

        public static ScoreManager Load()
        {
            if (!File.Exists(_fileName))
            {
                return new ScoreManager();
            }
            using (var reader = new StreamReader(new FileStream(_fileName, FileMode.Open)))
            {
                var serillizer = new XmlSerializer(typeof(List<Score>));
                var scores = (List<Score>)serillizer.Deserialize(reader);
                return new ScoreManager(scores);
            }
        }

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
