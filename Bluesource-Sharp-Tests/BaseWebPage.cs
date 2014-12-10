using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace BluesourceSharpTests
{
	public class BaseWebPage
	{
		public IWebDriver driver { get; private set; }

		public BaseWebPage(IWebDriver driver)
		{
			this.driver = driver;
			PageFactory.InitElements(this.driver, this);
		}

		public bool SyncElement(By by) {
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(10000));
			wait.IgnoreExceptionTypes (typeof(NoSuchElementException));
			IWebElement element = wait.Until (WaitingConditions.ElementIsClickable(by));
			return element.Enabled;
		}
	}
}

