using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using System;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using CsvHelper;
using System.Collections.Generic;
using System.Text;

namespace BluesourceSharpTests
{
	[TestFixture ()]
	public class BluesourceTest
	{
		private IWebDriver driver;

		[TestFixtureSetUp] public void Init()
		{
			driver = new FirefoxDriver();
			driver.Navigate ().GoToUrl ("http://bluesourcestaging.herokuapp.com/login");
			driver.Manage ().Timeouts ().ImplicitlyWait (TimeSpan.FromSeconds (10));
		}

		[TestFixtureTearDown] public void Cleanup()
		{
			driver.Quit ();
		}

		/*[Test ()]
		public void TestLoginLogout()
		{
			LoginPage page = new LoginPage (driver);
			page.DoLogin ("company.admin", "anything");
			NavigationBar nav = new NavigationBar (driver);
			Assert.IsTrue (nav.HasLogoutLink ());
			page = nav.DoLogout ();
			Assert.IsTrue (page.HasLoginLink ());
		}

		[Test ()]
		public void TestAddEmployee()
		{
			LoginPage page = new LoginPage (driver);
			EmployeesPage empl = page.DoLogin ("company.admin", "anything");
			NavigationBar nav = new NavigationBar (driver);
			Assert.IsTrue (nav.HasLogoutLink ());
			AddEmployeePage newempl = empl.GotoAddEmployee ();
			string user = Guid.NewGuid ().ToString ();
			string first = Guid.NewGuid().ToString ();
			string last = Guid.NewGuid ().ToString ();
			empl = newempl.AddEmployee (user, first, last);
			Assert.IsTrue (nav.HasAddedEmployeeText ());
			page = nav.DoLogout ();
			Assert.IsTrue (page.HasLoginLink ());
		}

		[Test ()]
		public void TestAddDepartment()
		{
			LoginPage page = new LoginPage (driver);
			page.DoLogin ("company.admin", "anything");
			NavigationBar nav = new NavigationBar (driver);
			DepartmentsPage depts = nav.GotoDepartments ();
			AddDepartmentPage addDept = depts.GotoAddDepartment ();
			depts = addDept.AddDepartment ("The Investigators");
			Assert.IsTrue (nav.HasAddedDepartmentText ());
			Assert.IsNotNull (depts.FindDepartmentByName ("The Investigators"));
			depts = depts.TrashDepartment ("The Investigators");
			// Assert.IsNull (depts.FindDepartmentByName ("The Investigators"));
			page = nav.DoLogout ();
			Assert.IsTrue (page.HasLoginLink ());
		}

		[Test ()]
		public void TestAddTitle()
		{
			LoginPage page = new LoginPage (driver);
			page.DoLogin ("company.admin", "anything");
			NavigationBar nav = new NavigationBar (driver);
			TitlesPage titles = nav.GotoTitles ();
			AddTitlePage addTitle = titles.GotoAddTitle();
			titles = addTitle.AddTitle ("Agent");
			Assert.IsTrue (nav.HasAddedTitleText ());
			Assert.IsNotNull (titles.FindTitleByName ("Agent"));
			titles = titles.TrashTitle ("Agent");
			// Assert.IsNull (titles.FindTitleByName ("Agent"));
			page = nav.DoLogout ();
			Assert.IsTrue (page.HasLoginLink ());
		}*/

		public IEnumerable<TestCaseData> TestTimeOffData() {
			StreamReader re = new StreamReader("data/bluesource-timeoff-test.csv");
			var csv = new CsvReader (re);

			while (csv.Read ()) {
				yield return new TestCaseData (
					csv.GetField<string>("Name"),
					csv.GetField<DateTime>("Start"),
					csv.GetField<DateTime>("End"),
					csv.GetField<string>("Type"),
					csv.GetField<string>("Reason"),
					csv.GetField<bool>("Half-Day"),
					csv.GetField<float>("Days"),
					csv.GetField<bool>("Succeeds")
				);
			}
		}

		[Test (), TestCaseSource("TestTimeOffData") ]
		public void TestTimeOff(string name, DateTime start, DateTime end, string type, string reason, bool halfday, float days, bool succeeds)
		{
			LoginPage page = new LoginPage (driver);
			EmployeesPage empl = page.DoLogin ("company.admin", "anything");
			NavigationBar nav = new NavigationBar (driver);
			empl.EnterInSearch ("Kazirick Revele");
			EmployeeDataPage data = empl.SelectFirstMatchingEmployee ();
			ManageTimeOffPage timeOff = data.GotoManageTimeOff ();
			timeOff = timeOff.SetVacationInfo (start, end, type, reason, halfday);
			if (succeeds) {
				IWebElement time = timeOff.GetVacationInfo (start);
				Assert.IsNotNull (time);
				Assert.IsTrue (time.FindElement (By.CssSelector (".business-days")).FindElement (By.XPath ("strong")).Text.Equals (days.ToString()));
				timeOff.TrashVacationInfo (start);
			}
			page = nav.DoLogout ();
			Assert.IsTrue (page.HasLoginLink ());
		}
	}
}

