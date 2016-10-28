using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hockey.Models
{
    public class ShlPlayer
    {
        public int ShlPlayerId { get; set; }
        public string PlayerCardId { get; set; }
        public Position _Position { get; set; }
        public int PositionId { get; set; }
        public string PlayerFirstName { get; set; }
        public string PlayerLastName { get; set; }
        public int PlayerJersyNumber { get; set; }
        public string PlayerImage { get; set; }
        public bool ISActive { get; set; }
        public bool ISSigned { get; set; }
        public decimal Value { get; set; }
        public League _Leauge { get; set; }
        public int LeagueId { get; set; }
        public Season _Season { get; set; }
        public int SeasonId { get; set; }
        public Image _Image { get; set; }
        public int ImageId { get; set; }
        public CardManufacture _CardManufacture { get; set; }
        public int CardManufactureId { get; set; }
        public DateTime PlayerAddDate { get; set; }
        public Team _Team { get; set; }
        public int TeamId { get; set; }
        public TeamImage _TeamImage { get; set; }
        public int TeamImageId { get; set; }
        public Nationality _Nationality { get; set; }
        public int NationalityId { get; set; }

    }
}
