using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace HeroesLoungeMMRGatherer.Shared.Models
{
    [Serializable]
    public class PlayerProfile
    {
        public string BattleTag { get; set; }

        public int PlayerID { get; set; }

        public string Name { get; set; }

        public List<LeaderboardRankings> LeaderboardRankings { get; set; }  

        public PlayerProfile()
        {
        

        }
    }

    [Serializable]
    public class LeaderboardRankings
    {
        public GameMode GameMode { get; set; }
        public int? LeagueID { get; set; }
        
        public int? LeagueRank { get; set; }

        public int? CurrentMMR { get; set; }
    }

    [Serializable]
    public enum GameMode
    {
        QuickMatch,
        HeroLeague,
        TeamLeague
    }
}
