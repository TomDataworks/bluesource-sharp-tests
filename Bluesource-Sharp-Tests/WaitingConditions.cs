using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace BluesourceSharpTests
{
	public class WaitingConditions
	{
		public static Func<IWebDriver, IWebElement> ElementIsClickable(By locator)
		{
			return driver =>
			{
				var element = driver.FindElement(locator);
				return (element != null && element.Displayed && element.Enabled) ? element : null;
			};
		}
	}
}

