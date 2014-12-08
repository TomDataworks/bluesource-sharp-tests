using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects; // *
using NUnit.Framework;
using System;
using System.Globalization;

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
			depts = addDept.AddDepartment ("The Paranormal Investigators");
			Assert.IsTrue (nav.HasAddedDepartmentText ());
			Assert.IsNotNull (depts.FindDepartmentByName ("The Paranormal Investigators"));
			depts = depts.TrashDepartment ("The Paranormal Investigators");
			// Assert.IsNull (depts.FindDepartmentByName ("The Paranormal Investigators"));
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
			titles = addTitle.AddTitle ("The Paranormal Investigators");
			Assert.IsTrue (nav.HasAddedTitleText ());
			Assert.IsNotNull (titles.FindTitleByName ("The Paranormal Investigators"));
			titles = titles.TrashTitle ("The Paranormal Investigators");
			// Assert.IsNull (titles.FindTitleByName ("The Paranormal Investigators"));
			page = nav.DoLogout ();
			Assert.IsTrue (page.HasLoginLink ());
		}*/

		[Test ()]
		public void TestTimeOff()
		{
			LoginPage page = new LoginPage (driver);
			EmployeesPage empl = page.DoLogin ("company.admin", "anything");
			NavigationBar nav = new NavigationBar (driver);
			empl.EnterInSearch ("Kazirick Revele");
			EmployeeDataPage data = empl.SelectFirstMatchingEmployee ();
			ManageTimeOffPage timeOff = data.GotoManageTimeOff ();
			timeOff = timeOff.SetVacationInfo (
				DateTime.ParseExact ("27122014", "ddMMyyyy", CultureInfo.InvariantCulture),
				DateTime.ParseExact ("28122014", "ddMMyyyy", CultureInfo.InvariantCulture),
				"Vacation"
			);
			foreach (IWebElement we in timeOff.GetAllTimeOff()) {
				Console.WriteLine (we.Text);
			}
		}
	}
}

