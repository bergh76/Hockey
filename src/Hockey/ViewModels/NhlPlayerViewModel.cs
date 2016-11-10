using Hockey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hockey.ViewModels
{
    public class NhlPlayerViewModel
    {
        public int PlayerId { get; set; }
        public string CardNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Season { get; set; }
        //public int SeasonId { get; set; }
        public string Position { get; set; }
        public int JersyNumber { get; set; }
        //public string League { get; set; }
        public  string Conference { get; set; }
        public string Division { get; set; }
        public string TeamImgPath { get; set; }
        public string Team { get; set; }
        public string ImagePath { get; set; }
        public bool ISSigned { get; set; }
        public string Nationality { get; set; }
        public string NationalityImgPath { get; set; }
        public string CardManufacture { get; set; }

    }
}