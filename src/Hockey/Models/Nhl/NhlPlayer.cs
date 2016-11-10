using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hockey.Models
{
    public class NhlPlayer
    {
        public int NhlPlayerId { get; set; }
        public string NhlPlayerCardId { get; set; }
        public Position _Position { get; set; }
        public int PositionId { get; set; }
        public string PlayerFirstName { get; set; }
        public string PlayerLastName { get; set; }
        public int PlayerJersyNumber { get; set; }
        public bool ISActive { get; set; }
        public bool ISSigned { get; set; }
        public decimal Value { get; set; }
        public Conference _Conference { get; set; }
        public int ConferenceId { get; set; }
        public Division _Division { get; set; }
        public int DivisionId { get; set; }
        public Season _Season { get; set; }
        public int SeasonId { get; set; }
        public int ImageId { get; set; }
        public int TeamId { get; set; }
        public int TeamImageId { get; set; }
        public CardManufacture _CardManufacture { get; set; }
        public int CardManufactureId { get; set; }
        public DateTime PlayerAddDate { get; set; }
        public Nationality _Nationality { get; set; }
        public int NationalityId { get; set; }
        public int LeagueId { get; set; }

        ICollection<Image> Image { get; set; }
    }
}
