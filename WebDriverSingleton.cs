using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace web_ui_automation
{
    public class WebDriverSingleton
    {
        private static IWebDriver? driver;
        private WebDriverSingleton() { }

        public static IWebDriver? Driver { get => driver; set => driver = value; }
        public static IWebDriver GetDriver()
        {
            if (Driver == null)
            {
                Driver = new ChromeDriver();
            }
            return Driver;
        }

        public static IWebDriver GetDriver(ChromeOptions options)
        {
            if (Driver == null)
            {
                Driver = new ChromeDriver(options);
            }
            return Driver;
        }
        public static void QuitDriver()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver = null;
            }
        }
    }
}
