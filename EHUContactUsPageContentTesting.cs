using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using web_ui_automation;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class EHUContactUsPageContentTesting
{
    private IWebDriver driver;
    private ContactUsPage contactUsPage;

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        driver = WebDriverSingleton.GetDriver(options);
        contactUsPage = new ContactUsPage(driver);
        contactUsPage.NavigateToContactUsPage();
    }

    [TearDown]
    public void Teardown()
    {
        WebDriverSingleton.QuitDriver();
    }

    [Test, Category("Contact Form")]
    public void ContactUs()
    {
        IWebElement email = driver.FindElement(By.XPath("//strong[text()='E-mail']/following-sibling::a"));
        IWebElement ltPhone = driver.FindElement(By.XPath("//li[strong[text()='Phone']]"));
        IWebElement byPhone = driver.FindElement(By.XPath("//li[strong[text()='Phone (BY']]"));
        IWebElement socialFacebook = driver.FindElement(By.CssSelector("[href*='https://www.facebook.com/groups/434978221124539/']"));
        IWebElement socialTg = driver.FindElement(By.CssSelector("[href*='https://t.me/skaryna_cultural_route']"));
        IWebElement socialVK = driver.FindElement(By.CssSelector("[href*='https://vk.com/public203605228']"));

        Assert.That(email.Text, Is.EqualTo("franciskscarynacr@gmail.com"));
        Assert.That(ltPhone.Text, Does.Contain("+370 68 771365"));
        Assert.That(byPhone.Text, Does.Contain("+375 29 5781488"));
        Assert.That(socialFacebook.Text, Is.EqualTo("Facebook"));
        Assert.That(socialTg.Text, Is.EqualTo("Telegram"));
        Assert.That(socialVK.Text, Is.EqualTo("VK"));
    }
}