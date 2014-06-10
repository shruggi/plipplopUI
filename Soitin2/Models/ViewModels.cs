using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soitin2.Models
{
    public class PlaylistViewModel
    {
        public string artist { get; set; }
        public string title { get; set; }
    }

    public class SupplementalPlaylistViewModel
    {
        public string artist { get; set; }
        public string title { get; set; }
        public string Owner { get; set; }

    }
}