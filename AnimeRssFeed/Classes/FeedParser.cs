using AnimeRssFeed.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.System;
using Windows.UI.Input.Preview.Injection;

namespace AnimeRssFeed.Classes
{
    public class FeedParser
    {
        public static Windows.Web.Syndication.SyndicationFeed CurrentFeed { get; set; }
        // Get single episode method
        public async void ParseSingleRssAsync(string url, ItemDetails itemDetails)
        {
            try
            {
                XDocument doc = new XDocument();
                try
                {
                    doc = XDocument.Load(url);
                }
                catch
                {
                    //Console.WriteLine("Connection failed");
                }

                var entries = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                              select new Item
                              {
                                  Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
                                  Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                                  //PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
                                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value
                              };

                foreach (var item in entries)
                {
                    string exstraZero = "";
                    if (itemDetails.EpisodeCountFound < 10)
                        exstraZero = "0";
                    else if (itemDetails.EpisodeCountFound == 0)
                        exstraZero = "01";
                    else
                        exstraZero = "";

                    if (item.Title == $"[HorribleSubs] {itemDetails.Name} - {exstraZero}{itemDetails.EpisodeCountFound} {itemDetails.Quality}.mkv" || item.Title == $"[HorribleSubs] {itemDetails.Name} - {exstraZero}{itemDetails.EpisodeCountFound} {itemDetails.Quality}")
                    {
                        //Console.WriteLine(item.Title);
                        var uri = new Uri(item.Link.ToString());
                        var success = await Windows.System.Launcher.LaunchUriAsync(uri);

                        if (success)
                        {
                            // URI launched
                        }
                        else
                        {
                            // URI launch failed
                        }
                        //Console.WriteLine(item.PublishDate);
                        OpenTorrent();
                    }
                    //Console.WriteLine(item.Title);
                }
                //OpenTorrent();
            }
            catch (Exception ex)
            {
            }
        }

        public IList<Item> ParseAllRss(string url)
        {
            //var entries = from item in CurrentFeed.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
            //              select new Item
            //              {
            //                  Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
            //                  Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
            //                  //PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
            //                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value
            //              };
            XDocument doc = new XDocument();
            try
            {
                doc = XDocument.Load(CurrentFeed.Items);
            }
            catch
            {
                //Console.WriteLine("Connection failed");
            }
            foreach (var item in CurrentFeed.Items)
            {
                //Console.WriteLine(item.Title);
                Debug.Write(item);
                
                //var entries = from item in item.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                //              select new Item
                //              {
                //                  Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
                //                  Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                //                  //PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
                //                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value
                //              };
            }

            return null;
        }

        public async Task<bool> ParseRssTest(string url)
        {
            var client = new Windows.Web.Syndication.SyndicationClient();
            try
            {
                var uri = new Uri(url);
                CurrentFeed = await client.RetrieveFeedAsync(uri);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async void OpenTorrent()
        {
            try
            {
                await Task.Delay(10000);
                // For console = SendKeys.SendWait("{Enter}");
                InputInjector inputInjector = InputInjector.TryCreate();

                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = (ushort)((VirtualKey)Enum.Parse(typeof(VirtualKey), "{Enter}", true));
            }
            catch
            {
            }
        }
    }
}