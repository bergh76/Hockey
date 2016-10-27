namespace Hockey.Models
{
    public class TeamImage
    {
        public int TeamImageId { get; set; }
        public string TeamImageName { get; set; }
        public string TeamImagePath { get; set; }
        public League _Leauge { get; set; }
        public int LeagueId { get; set; }
        public NhlTeam _NhlTeams {get;set;}
        public int NhlTeamId { get; set; }
    }
}