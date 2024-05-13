using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UnitTest1.Pages
{
    public class WikipediaPage
    {
        private readonly IWebDriver _driver;

        public WikipediaPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void NavigateTo()
        {
            _driver.Navigate().GoToUrl("https://en.wikipedia.org/wiki/Test_automation");
        }

        /*public void CloseAdvertisement()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            var closeButton = wait.Until(driver => driver.FindElement(By.Id("cnotice-toggle-icon")));
            closeButton.Click();
        }*/

        public string GetTestDrivenDevelopmentParagraphText()
        {
            // Wait for the "Test-driven development" section to be present
            var waitSection = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            var testDrivenDevelopmentHeader = waitSection.Until(driver => driver.FindElement(By.XPath("//*[@id=\"Test-driven_development\"]")));

            // Navigate to the following sibling p element
            var testDrivenDevelopmentParagraph = testDrivenDevelopmentHeader.FindElement(By.XPath("//*[@id=\"mw-content-text\"]/div[1]/p[11]"));

            return testDrivenDevelopmentParagraph.Text;
        }

        public Dictionary<string, int> CountUniqueWords(string text)
        {
            var wordCounts = new Dictionary<string, int>();

            // Preprocess text
            text = text.ToLower();
            text = RemoveContentWithinBrackets(text);

            // Tokenize and count words
            var words = text.Split(new char[] { ' ', ',', '.', '-', ';', ':', '(', ')', '[', ']', '{', '}', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                var cleanedWord = CleanWord(word);
                if (!wordCounts.ContainsKey(cleanedWord))
                {
                    wordCounts[cleanedWord] = 0;
                }
                wordCounts[cleanedWord]++;
            }

            return wordCounts;
        }

        private string RemoveContentWithinBrackets(string text)
        {
            // Remove content within square brackets and their enclosing brackets
            int startIndex;
            while ((startIndex = text.IndexOf('[')) != -1)
            {
                int endIndex = text.IndexOf(']', startIndex);
                if (endIndex != -1)
                {
                    text = text.Remove(startIndex, endIndex - startIndex + 1);
                }
                else
                {
                    break;
                }
            }
            return text;
        }

        private string CleanWord(string word)
        {
            // Remove any non-alphabetic characters
            return new string(Array.FindAll(word.ToCharArray(), char.IsLetter));
        }
    }
}
