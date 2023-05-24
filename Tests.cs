using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Drawing;

namespace RegatulJocurilorTests
{
    public class Tests
    {
        private IWebDriver driver;
        private string productName = "";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Size = new Size(1920, 1080);
        }

        [Test]
        public void Run()
        {
            driver.Navigate().GoToUrl("https://regatuljocurilor.ro/ro/");

            // Close popups
            IWebElement button = driver.FindElement(By.XPath("//div[@class='tm-newsletter-popup-close']//i[@class = 'material-icons clear']"));
            button.Click();
            IWebElement button2 = driver.FindElement(By.XPath("//div[@id='cookieNotice']//button[@class='btn closeFontAwesome']"));
            button2.Click();

            // Get product name
            IWebElement text1 = driver.FindElement(By.XPath("//*[@id=\"newproduct-grid\"]/li[1]/div/div/div[2]/span/a"));
            productName = text1.GetAttribute("title");


            // Add product to cart
            IWebElement button3 = driver.FindElement(By.XPath("//*[@id=\"newproduct-grid\"]/li[1]/div/div/div[2]/div[3]/form/button"));
            button3.Click();
            System.Threading.Thread.Sleep(2000);

            //Finalize order
            IWebElement button4 = driver.FindElement(By.XPath("//*[@id=\"blockcart-modal\"]/div/div/div[2]/div/div[2]/div/div/a"));
            button4.Click();

            // Check that the product added in the cart is the right one
            IWebElement text2 = driver.FindElement(By.XPath("//*[@id=\"main\"]/div[3]/div[1]/div[1]/div[2]/ul/li/div/div[2]/div[1]/a"));
            Assert.That(productName, Is.EqualTo(text2.Text));

            // Check that the product was added in the correct quantity
            IWebElement input1 = driver.FindElement(By.XPath("//*[@id=\"main\"]/div[3]/div[1]/div[1]/div[2]/ul/li/div/div[3]/div/div[2]/div/div/div/input"));
            string value = input1.GetAttribute("value");
            Assert.That(value, Is.EqualTo("1"));
        }

        [TearDown]
        public void TearDown()
        {
            // Close the driver and the browser
            driver.Quit();
        }
    }
}