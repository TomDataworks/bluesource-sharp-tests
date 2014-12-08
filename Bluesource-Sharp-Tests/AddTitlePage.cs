using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace BluesourceSharpTests
{
	public class AddTitlePage : BaseWebPage
	{
		[FindsBy]
		private IWebElement title_name;

		public AddTitlePage ( IWebDriver driver ) : base (driver) {}

		public TitlesPage AddTitle(string name) {
			if (SyncElement (By.Id("title_name"))) {
				title_name.SendKeys (name);
				title_name.Submit ();
			}
			return new TitlesPage (driver);
		}
	}
}

