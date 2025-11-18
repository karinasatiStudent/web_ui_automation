using log4net;
using log4net.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Shouldly;
using System.Security.Cryptography;
using web_ui_automation;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class EHUContactUsPageContentTesting
{
    private IWebDriver driver;
    private ContactUsPage contactUsPage;

    private static readonly ILog log = LogManager.GetLogger(typeof(EHUContactUsPageContentTesting));

    [SetUp]
    public void Setup()
    {

        XmlConfigurator.Configure(new FileInfo("log4net.config"));
        log.Info("Starting EHU Contact Us Page Content Testing");


        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");

        try
        {
            driver = WebDriverSingleton.GetDriver(options);
            contactUsPage = new ContactUsPage(driver);
            log.Info("ChromeDriver initialized successfully.");
        }
        catch (Exception ex)
        {
            log.Fatal("Failed to initialize ChromeDriver.", ex);
            throw;
        }

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
        contactUsPage.GetEmailText().ShouldBe("franciskscarynacr@gmail.com");
        contactUsPage.GetLtPhoneText().ShouldContain(" +370 68 771365");
        contactUsPage.GetByPhoneText().ShouldContain("+375 29 5781488");
        contactUsPage.GetSocialFacebookText().ShouldBe("Facebook");
        contactUsPage.GetSocialTgText().ShouldBe("Telegram");
        contactUsPage.GetSocialVkText().ShouldBe("VK");
    }
}