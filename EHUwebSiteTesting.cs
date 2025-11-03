using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class EHUwebSiteTesting
{
    private IWebDriver driver;
    private string baseUrl = "https://en.ehuniversity.lt/";

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        driver = new ChromeDriver(options);
    }

    [TearDown]
    public void Teardown()
    {
        driver.Quit();
    }

    [Test, Category("Navigation")]
    public void Navigation()
    {
        driver.Navigate().GoToUrl(baseUrl);
        IWebElement aboutLink = driver.FindElement(By.LinkText("About"));
        aboutLink.Click();

        Assert.That(driver.Url, Is.EqualTo("https://en.ehuniversity.lt/about/"));
        Assert.That(driver.Title, Is.EqualTo("About"));
        Assert.That(driver.FindElement(By.XPath("//*[@class='subheader__breadcrumbs']/li/a")).Text, Is.EqualTo("Home"));

        driver.Quit();
    }

    [Test, Category("Search")]
    [TestCase("study programs", "/?s=study+programs", "Search Results study programs")]
    public void Search(string key, string urlParams, string pageTitle)
    {
        driver.Navigate().GoToUrl(baseUrl);
        IWebElement searchMagnifier = driver.FindElement(By.CssSelector(".header-search"));
        searchMagnifier.Click();

        IWebElement searchBox = driver.FindElement(By.Name("s"));
        searchBox.SendKeys(key);
        searchBox.Submit();

        Assert.That(driver.Url, Does.Contain(urlParams));
        Assert.That(driver.Title, Is.EqualTo(pageTitle));

        string searchResultsText = driver.FindElement(By.XPath("//*[@class='content search-results']")).Text;
        Assert.That(Regex.IsMatch(searchResultsText, @"([Ss]tudy|[Pp]rograms*)"));

        driver.Quit();
    }

    [Test, Category("Language")]
    public void LanguageChange()
    {
        driver.Navigate().GoToUrl(baseUrl);
        IWebElement langSwitch = driver.FindElement(By.XPath("//*[@class='language-switcher']/li/a"));
        langSwitch.Click();

        IWebElement ltLang = driver.FindElement(By.XPath("//a[text()='lt']"));
        ltLang.Click();

        Assert.That(driver.Url, Is.EqualTo("https://lt.ehuniversity.lt/"));

        string? language = driver.FindElement(By.TagName("html")).GetAttribute("lang");
        Assert.That(language, Is.EqualTo("lt-LT"));
    }

    [Test, Category("Contact Form")]
    public void ContactUs()
    {
        driver.Navigate().GoToUrl("https://en.ehuniversity.lt/research/projects/contact-us/");
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

