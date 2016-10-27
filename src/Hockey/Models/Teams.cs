using Hockey.Models;

namespace Hockey.Models
{
    public class NhlTeam
    {
        public int NhlTeamId { get; set; }
        public string NhlTeamName { get; set; }
        public int ConferenceId { get; set; }
        public int DivisionId { get; set; }
    }
}