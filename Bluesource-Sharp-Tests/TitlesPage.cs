using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace BluesourceSharpTests
{
	public class TitlesPage : BaseWebPage
	{
		public TitlesPage ( IWebDriver driver ) : base (driver) {}

		[FindsBy(How = How.LinkText, Using = "New Title")]
		private IWebElement addTitle;

		public AddTitlePage GotoAddTitle() {
			addTitle.Click ();
			return new AddTitlePage (driver);
		}

		public IWebElement FindTitleByName( string name ) {
			return driver.FindElement (By.XPath ("//tr[contains(., '" + name + "')]"));
		}

		public TitlesPage TrashTitle( string name ) {
			FindTitleByName(name).FindElement(By.CssSelector("span.glyphicon.glyphicon-trash")).Click();
			driver.SwitchTo ().Alert ().Accept ();
			return new TitlesPage (driver);
		}
	}
}