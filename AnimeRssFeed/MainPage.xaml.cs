using AnimeRssFeed.Classes;
using AnimeRssFeed.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.System;
using Windows.UI.Input.Preview.Injection;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AnimeRssFeed
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static bool single = false;

        public MainPage()
        {
            this.InitializeComponent();

            FeedConnectionTest();
            
        }

        public async void FeedConnectionTest()
        {
            FeedParser parser = new FeedParser();
            
            ConsoleOutPut.Text += Environment.NewLine + "Attmepting RSS Feed Connection Test";

            bool result = await parser.ParseRssTest("https://nyaa.si/?page=rss");
            if (result)
            {
                ConsoleOutPut.Text += Environment.NewLine + "Attmepting RSS Feed Connection Success";
                StartFeed();
            }
            else
            {
                ConsoleOutPut.Text += Environment.NewLine + "Attmepting RSS Feed Connection Failed";
            }
        }

        private void StartFeed()
        {
            FeedParser parser = new FeedParser();

            if (single)
            {
                List<ItemDetails> details = new List<ItemDetails>()
                {
                    new ItemDetails
                    {
                        EpisodeCount = 7,
                        EpisodeCountFound = 3,
                        Name = "JoJo's Bizarre Adventure - Golden Wind",
                        Quality = "[720p]",
                        ReferenceName = "JoJo's+Bizarre+Adventure+Golden+Wind",
                        PageCount = 5
                    },
                     new ItemDetails
                     {
                         EpisodeCount = 12,
                        EpisodeCountFound = 5,
                        Name = "Kaguya-sama wa Kokurasetai",
                        Quality = "[720p]",
                        ReferenceName = "Kaguya+sama+wa+Kokurasetai",
                        PageCount = 5 // Guess the average pages the rss will have
                     }
                };
                foreach (var item in details)
                {
                    for (int i = 0; i < item.PageCount; i++)
                    {
                        try { parser.ParseSingleRssAsync($"https://nyaa.si/?page=rss&q=+{item.ReferenceName}+&c=0_0&f={i.ToString()}", item); } catch { throw; }
                    }
                }
                //var items = parser.Parse("https://nyaa.si/?page=rss&q=Kaguya-sama+wa+Kokurasetai&c=0_0&f=0", FeedType.RSS);
                //OpenTorrent();
            }
            else
            {
                parser.ParseAllRss("https://nyaa.si/?page=rss");
            }

            //Console.ReadLine();
        }
    }


    
}