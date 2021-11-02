using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoScraping
{
    public class Scraper : ICryptoPriceScraper
    {
        ChromeOptions options;
        public Scraper()
        {
             options = new ChromeOptions();
             options.AddArguments("disable-gpu");
        }

        public Dictionary<string, string> GetPrices()
        {
            Dictionary<string, string> prices = new Dictionary<string, string>();
            using (IWebDriver driver = new ChromeDriver(options))
            {
                //AssetTable__AssetTableBody-sc-1hzgxt1-0
                //AssetTable__AssetTableBody-sc-1hzgxt1-0 gcVqXC
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("https://www.coinbase.com/");
                var tableElement = driver.FindElement(By.TagName("tbody"));
                foreach (var item in tableElement.FindElements(By.TagName("tr")))
                {
                    var cryptoName = item.GetAttribute("data-slug");
                    var elements = item.FindElements(By.TagName("td"));
                    string textPrice = elements[2].FindElement(By.TagName("span")).Text.Split(';')[0];
                    //decimal price = Convert.ToDecimal(text);
                    prices.Add(cryptoName, textPrice);
            
                }
                return prices;
            }
        }
    }
}
