using OpenQA.Selenium;

namespace web_ui_automation
{
    public class ContactUsPage
    {
        private IWebDriver driver;

        private string contactUsUrl = "https://en.ehuniversity.lt/research/projects/contact-us/";

        public ContactUsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void NavigateToContactUsPage()
        {
            driver.Navigate().GoToUrl(contactUsUrl);
        }
    }
}
