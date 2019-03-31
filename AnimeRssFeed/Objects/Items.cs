using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRssFeed.Objects
{
    public class Item
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public FeedType MyProperty { get; set; }

        public Item()
        {
            Link = "";
            Title = "";
            Content = "";
            PublishDate = DateTime.Today;
            MyProperty = FeedType.RSS;
        }
    }
    public enum FeedType
    {
        RSS,
        RDF,
        Atom
    }
    public class ItemDetails
    {
        public string Name { get; set; }
        public int EpisodeCount { get; set; }
        public int EpisodeCountFound { get; set; }
        public string Quality { get; set; }
        public string ReferenceName { get; set; }
        public int PageCount { get; set; }

        public ItemDetails()
        {
            Name = "";
            EpisodeCount = 0;
            EpisodeCountFound = 0;
            Quality = "";
            ReferenceName = "";
            PageCount = 0;
        }
    }

}
