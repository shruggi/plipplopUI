using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soitin2.Models
{
    public class BrowserTableModel
    {
        public int id { get; set; }
        public string artist { get; set; }
        public string title { get; set; }
        public string album { get; set; }
        public string genre { get; set; }

        public string owner { get; set; }
    }
    public class QueueTableModel
    {
        public int id { get; set; }
        public DateTime addtime { get; set; }
        public string artist { get; set; }
        public string title { get; set; }
        public string source { get; set; }
    }
}