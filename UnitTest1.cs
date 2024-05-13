using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace UnitTest1.Tests
{
    [TestFixture]
    public class WikipediaPageTests
    {
        private IWebDriver _driver;
        private Pages.WikipediaPage _wikipediaPage;

        [SetUp]
        public void SetUp()
        {
            try
            {
                string edgeDriverPath = @"C:\Users\91893\Downloads\edgedriver_win64\msedgedriver.exe";
                EdgeOptions options = new EdgeOptions();
                _driver = new EdgeDriver(edgeDriverPath, options);
                _wikipediaPage = new Pages.WikipediaPage(_driver);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during setup: {ex}");
                throw;
            }
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                if (_driver != null)
                {
                    _driver.Quit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during teardown: {ex}");
            }
        }

        [Test]
        public void TestUniqueWordCount()
        {
            try
            {
                _wikipediaPage.NavigateTo();
                //_wikipediaPage.CloseAdvertisement();

                // Get the paragraph text
                string paragraphText = _wikipediaPage.GetTestDrivenDevelopmentParagraphText();
                Console.WriteLine(paragraphText); // Output paragraph text

                // Analyze text to count unique words
                var wordCounts = _wikipediaPage.CountUniqueWords(paragraphText);

                // Output word counts
                foreach (var wordCount in wordCounts)
                {
                    Console.WriteLine($"{wordCount.Key}: {wordCount.Value}");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail($"An error occurred during test execution: {ex.Message}");
            }
        }
    }
}