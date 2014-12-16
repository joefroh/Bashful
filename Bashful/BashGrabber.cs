using System.Windows.Controls;
using System.Windows.Documents;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bashful
{
    public class BashGrabber
    {
        private const string url = "http://www.bash.org/?random";
        private const string quoteSelector = "//p[@class='qt']";
        private const string quoteNumberSelector = "//p[@class='quote']";
        private const string upgradeSelector = "//a[contains(., '+')]";
        private const string downgradeSelector = "//a[contains(., '-')]";
        private const string reportSelector = "//a[@class='qa']";
        List<Quote> _quotes;
        bool needQuotes;
        int currentQuote;

        public BashGrabber()
        {
            _quotes = new List<Quote>();
            needQuotes = true;
            currentQuote = -1;
        }

        public async Task<KeyValuePair<string, Quote>> GetRandomQuotes()
        {
            KeyValuePair<string, Quote> output = new KeyValuePair<string, Quote>();
            if (needQuotes)
            {
                currentQuote = 0;
                WebRequest request;
                request = HttpWebRequest.Create(url);

                //cache work around
                if (request.Headers == null)
                {
                    request.Headers = new WebHeaderCollection();

                }
                request.Headers[HttpRequestHeader.IfModifiedSince] = DateTime.UtcNow.ToString();

                var result = await request.GetResponseAsync();
                var stream = result.GetResponseStream();
                _quotes = GetQuoteListFromHTML(stream);

                if (_quotes.Any())
                {
                    output = new KeyValuePair<string, Quote>(HttpUtility.HtmlDecode(_quotes[currentQuote].Number), _quotes[currentQuote]);
                }

                if (currentQuote + 1 < _quotes.Count)
                {
                    needQuotes = false;
                }
                else
                {
                    needQuotes = true;
                }
            }
            else
            {
                currentQuote++;
                output = new KeyValuePair<string, Quote>(HttpUtility.HtmlDecode(_quotes[currentQuote].Number), _quotes[currentQuote]);

                if (currentQuote + 1 < _quotes.Count)
                {
                    needQuotes = false;
                }
                else
                {
                    needQuotes = true;
                }
            }

            return output;
        }

        private List<Quote> GetQuoteListFromHTML(Stream htmlDoc)
        {
            var result = new List<Quote>();
            HtmlDocument doc = new HtmlDocument();
            doc.Load(htmlDoc);

            var quotes = doc.DocumentNode.SelectNodes(quoteSelector).ToArray();
            var numbers = doc.DocumentNode.SelectNodes(quoteNumberSelector).ToArray();
            var upgraders = doc.DocumentNode.SelectNodes(upgradeSelector).ToArray();
            var downgraders = doc.DocumentNode.SelectNodes(downgradeSelector).ToArray();
            var reporters = doc.DocumentNode.SelectNodes(reportSelector).ToArray();

            for (int i = 0; i < quotes.Length; i++)
            {
                var url = String.Format("http://bash.org/?{0}", HttpUtility.HtmlDecode(numbers[i].InnerText).Split(' ').First().Substring(1));
                var upgrader = HttpUtility.HtmlDecode(String.Format("http://bash.org{0}", upgraders[i].Attributes["href"].Value.Substring(1)));
                var downgrader = upgrader.Replace("&rox", "&sox");
                var reporter = upgrader.Replace("&rox", "&sux");
                result.Add(new Quote(quotes[i].InnerText, numbers[i].InnerText.Split(' ').First(), url, upgrader, downgrader, reporter));
            }

            return result;
        }

    }
}
