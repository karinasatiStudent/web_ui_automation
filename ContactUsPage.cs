using log4net;
using log4net.Config;
using OpenQA.Selenium;

namespace web_ui_automation
{
    public class ContactUsPage
    {
        private IWebDriver driver;
        private static readonly ILog log = LogManager.GetLogger(typeof(ContactUsPage));

        private string contactUsUrl = "https://en.ehuniversity.lt/research/projects/contact-us/";
        private By email = By.XPath("//strong[text()='E-mail']/following-sibling::a");
        private By ltPhone = By.XPath("//li[strong[text()='Phone']]");
        private By byPhone = By.XPath("//li[strong[text()='Phone (BY']]");
        private By socialFacebook = By.CssSelector("[href*='https://www.facebook.com/groups/434978221124539/']");
        private By socialTg = By.CssSelector("[href*='https://t.me/skaryna_cultural_route']");
        private By socialVK = By.CssSelector("[href*='https://vk.com/public203605228']");

        public ContactUsPage(IWebDriver driver)
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            this.driver = driver;
        }

        public void NavigateToContactUsPage()
        {
            driver.Navigate().GoToUrl(contactUsUrl);
            log.Debug("Navigated to Contact Us page.");
        }

        private string GetTextOrEmpty(By by)
        {
            var el = driver.FindElements(by).FirstOrDefault();
            return el != null ? el.Text.Trim() : string.Empty;
            log.Info($"Retrieved text for element {by.ToString()}.");
        }

        public string GetEmailText() => GetTextOrEmpty(email);
        public string GetLtPhoneText() => GetTextOrEmpty(ltPhone);
        public string GetByPhoneText() => GetTextOrEmpty(byPhone);
        public string GetSocialFacebookText() => GetTextOrEmpty(socialFacebook);
        public string GetSocialTgText() => GetTextOrEmpty(socialTg);
        public string GetSocialVkText() => GetTextOrEmpty(socialVK);
    }
}
