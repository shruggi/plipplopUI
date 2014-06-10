using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soitin2.Models
{
    public class TrackSetModel
    {
        public int Id { get; set; }
        public string album { get; set; }
        public string title { get; set; }
        public string track { get; set; }
        public string artist { get; set; }
        public string genre { get; set; }
        public string filename { get; set; }
        public int runningtime { get; set; }
        public DateTime date { get; set; }
        public int weight { get; set; }

    }
}