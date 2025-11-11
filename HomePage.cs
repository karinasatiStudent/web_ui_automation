using OpenQA.Selenium;

namespace web_ui_automation
{
    public class HomePage
    {
        private IWebDriver driver;

        private string baseUrl = "https://en.ehuniversity.lt/";
        private By aboutLinkText = By.LinkText("About");
        private By searchMagnifier = By.CssSelector(".header-search");
        private By searchBoxName = By.Name("s");
        private By langSwitch = By.XPath("//*[@class='language-switcher']/li/a");
        private By ltLang = By.XPath("//a[text()='lt']");

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void NavigateToHomePage()
        {
            driver.Navigate().GoToUrl(baseUrl);
        }

        public void NavigateToAboutPage()
        {
            driver.FindElement(aboutLinkText).Click();
        }

        public void ClickLanguageSwitch()
        {
            driver.FindElement(langSwitch).Click();
        }

        public void SelectLtLanguage()
        {
            driver.FindElement(ltLang).Click();
        }

        public void Search(string key)
        {
            var searchMagnifierElement = driver.FindElement(searchMagnifier);
            searchMagnifierElement.Click();
            var searchBox = driver.FindElement(searchBoxName);
            searchBox.SendKeys(key);
            searchBox.Submit();
        }
    }
}
