using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Globalization;
using System.Linq;
using Xunit;

namespace SeleniumTestsDotnetCore
{
    public class SeleniumTests
    {
        [Fact]
        public void ValidateProductSearch()
        {
            using (var driver = new ChromeDriver("C:\\Driver\\"))
            {
                string siteUrl = "https://www.amazon.com/";
                string searchString = "Laptop";
                By searchTextBox = By.Id("twotabsearchtextbox");
                By searchButton = By.Id("nav-search-submit-button");
                By productList = By.XPath("//span[@class='a-size-medium a-color-base a-text-normal']");
                By productPrice = By.Id("priceblock_ourprice");
                string price;

                //Navigate to website
                driver.Navigate().GoToUrl(siteUrl);

                //Clear & enter searchText
                driver.FindElement(searchTextBox).Clear();
                driver.FindElement(searchTextBox).SendKeys(searchString);

                //Click on search
                driver.FindElement(searchButton).Click();

                //Click on first product in result list.
                driver.FindElements(productList).First().Click();

                //Assert price of the item to be greater than 100
                price = driver.FindElement(productPrice).Text;
                decimal priceInDecimal = ConvertCurrencyToDecimal(price);

                Assert.True(priceInDecimal > 100, priceInDecimal+ "expected to be greater than "+ 100);
            }
        }

        public decimal ConvertCurrencyToDecimal(string currency)
        {
            NumberStyles style;
            CultureInfo culture = new CultureInfo("en-US");
            style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            decimal convertedValue = Decimal.Parse(currency, style, culture);

            Console.WriteLine("'{0}' converted to {1}", currency, convertedValue);

            return convertedValue;
        }

    }
}
