using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoScraping
{
    public interface ICryptoPriceScraper
    {
        Dictionary<string, string> GetPrices();
    }
}
