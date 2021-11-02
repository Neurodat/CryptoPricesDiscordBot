using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptoScraping
{
    public class HTMLScraper : ICryptoPriceScraper
    {
        private string baseURL = "https://www.coinbase.com/";
        public Dictionary<string, string> GetPrices()
        {
            Dictionary<string, string> prices = new Dictionary<string, string>();
            var web = new HtmlWeb();
            var doc = web.Load(baseURL);
            var tbody = doc.DocumentNode.Descendants("tbody").FirstOrDefault();
            var trs = tbody.Descendants("tr");
            foreach (var tr in trs)
            {
                var name = tr.Attributes.FirstOrDefault(item => item.Name == "data-slug").Value;
                var tds = tr.Descendants("td").ToArray();
                string price = tds[2].Descendants("span").FirstOrDefault().InnerText.Split(';')[0];
                prices.Add(name, price);
            }
            return prices;
        }
    }
}
