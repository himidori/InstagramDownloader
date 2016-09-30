using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramDownloader
{
    class InstagramImage
    {
        public string status { get; set; }
        public Item[] items { get; set; }
    }

    class Item
    {
        public string id { get; set; }
        public Image images { get; set; }
    }

    class Image
    {
        public Resolution low_resolution { get; set; }
        public Resolution thumbnail { get; set; }
        public Resolution standard_resolution { get; set; }
    }

    class Resolution
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}
