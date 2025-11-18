using log4net;
using log4net.Config;
using OpenQA.Selenium;

namespace web_ui_automation
{
    public class HomePage
    {
        private IWebDriver driver;

        private static readonly ILog log = LogManager.GetLogger(typeof(HomePage));

        private string baseUrl = "https://en.ehuniversity.lt/";
        private By aboutLinkText = By.LinkText("About");
        private By searchMagnifier = By.CssSelector(".header-search");
        private By searchBoxName = By.Name("s");
        private By langSwitch = By.XPath("//*[@class='language-switcher']/li/a");
        private By ltLang = By.XPath("//a[text()='lt']");
        private string ltUrl = "https://lt.ehuniversity.lt/";

        public string LtUrl => ltUrl;

        public HomePage(IWebDriver driver)
        {

            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            this.driver = driver;
        }

        public void NavigateToHomePage()
        {
            driver.Navigate().GoToUrl(baseUrl);
            log.Debug("Navigated to Home page.");
        }

        public void NavigateToAboutPage()
        {
            driver.FindElement(aboutLinkText).Click();
            log.Debug("Navigated to About page.");
        }

        public void ClickLanguageSwitch()
        {
            driver.FindElement(langSwitch).Click();
            log.Info("Clicked on language switcher.");
        }

        public void SelectLtLanguage()
        {
            driver.FindElement(ltLang).Click();
            log.Info("Selected Lithuanian language.");
        }

        public void Search(string key)
        {
            var searchMagnifierElement = driver.FindElement(searchMagnifier);
            searchMagnifierElement.Click();
            log.Info("Clicked on search magnifier.");
            var searchBox = driver.FindElement(searchBoxName);
            searchBox.SendKeys(key);
            log.Info($"Entered search key: {key}");
            searchBox.Submit();
            log.Info("Submitted search form.");
        }
    }
}
