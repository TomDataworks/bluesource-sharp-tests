using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace BluesourceSharpTests
{
	public class ViewTimeOffPage : BaseWebPage
	{
		public ViewTimeOffPage ( IWebDriver driver ) : base (driver) {}
	}
}