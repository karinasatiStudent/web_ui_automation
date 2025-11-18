using log4net;
using log4net.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Shouldly;
using System.Text.RegularExpressions;
using web_ui_automation;

[TestFixture]
[Parallelizable(ParallelScope.Fixtures)]
public class EHUHomaPageTesting
{
    private IWebDriver driver;
    private HomePage homePage;
    private string aboutUrl = "https://en.ehuniversity.lt/about/";
    private static readonly ILog log = LogManager.GetLogger(typeof(EHUHomaPageTesting));

    [SetUp]
    public void Setup()
    {
        XmlConfigurator.Configure(new FileInfo("log4net.config"));
        log.Info("Starting EHU Home Page Tests");

        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");

        try
        {
            driver = WebDriverSingleton.GetDriver(options);
            homePage = new HomePage(driver);
            log.Info("ChromeDriver initialized successfully.");
        }
        catch (Exception ex)
        {
            log.Fatal("Failed to initialize ChromeDriver.", ex);
            throw;
        }

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

        driver.Url.ShouldBe(aboutUrl);
        driver.Title.ShouldBe("About");
        driver.FindElement(By.XPath("//*[@class='subheader__breadcrumbs']/li/a")).Text.ShouldBe("Home");
    }

    [Test, Category("Search")]
    [TestCase("study programs", "/?s=study+programs", "Search Results study programs")]
    public void Search(string key, string urlParams, string pageTitle)
    {
        homePage.Search(key);

        driver.Url.ShouldContain(urlParams);
        driver.Title.ShouldBe(pageTitle);

        string searchResultsText = driver.FindElement(By.XPath("//*[@class='content search-results']")).Text;
        Regex.IsMatch(searchResultsText, @"([Ss]tudy|[Pp]rograms*)").ShouldBeTrue();

        driver.Quit();
    }

    [Test, Category("Language")]
    public void LanguageChange()
    {
        homePage.ClickLanguageSwitch();
        homePage.SelectLtLanguage();

        driver.Url.ShouldBe(homePage.LtUrl);

        string? language = driver.FindElement(By.TagName("html")).GetAttribute("lang");
        language.ShouldBe("lt-LT");
    }
}

