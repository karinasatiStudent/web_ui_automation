using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;
using web_ui_automation;

[TestFixture]
[Parallelizable(ParallelScope.Fixtures)]
public class EHUHomaPageTesting
{
    private IWebDriver driver;
    private HomePage homePage;
    private string aboutUrl = "https://en.ehuniversity.lt/about/";
    private string ltUrl = "https://lt.ehuniversity.lt/";

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        driver = WebDriverSingleton.GetDriver(options);
        homePage = new HomePage(driver);
        homePage.NavigateToHomePage();
    }

    [TearDown]
    public void Teardown()
    {
        WebDriverSingleton.QuitDriver();
    }

    [Test, Category("Navigation")]
    public void Navigation()
    { 
        homePage.NavigateToAboutPage();

        Assert.That(driver.Url, Is.EqualTo(aboutUrl));
        Assert.That(driver.Title, Is.EqualTo("About"));
        Assert.That(driver.FindElement(By.XPath("//*[@class='subheader__breadcrumbs']/li/a")).Text, Is.EqualTo("Home"));
    }

    [Test, Category("Search")]
    [TestCase("study programs", "/?s=study+programs", "Search Results study programs")]
    public void Search(string key, string urlParams, string pageTitle)
    {
        homePage.Search(key);

        Assert.That(driver.Url, Does.Contain(urlParams));
        Assert.That(driver.Title, Is.EqualTo(pageTitle));

        string searchResultsText = driver.FindElement(By.XPath("//*[@class='content search-results']")).Text;
        Assert.That(Regex.IsMatch(searchResultsText, @"([Ss]tudy|[Pp]rograms*)"));

        driver.Quit();
    }

    [Test, Category("Language")]
    public void LanguageChange()
    {
        homePage.ClickLanguageSwitch();
        homePage.SelectLtLanguage();

        Assert.That(driver.Url, Is.EqualTo(ltUrl));

        string? language = driver.FindElement(By.TagName("html")).GetAttribute("lang");
        Assert.That(language, Is.EqualTo("lt-LT"));
    }
}

