using System;
using System.IO;
using CsvHelper;
using NUnit.Framework;
using System.Collections.Generic;

namespace BluesourceSharpTests
{
	public class BluesourceTestData
	{
		public BluesourceTestData ()
		{
		}

		public IEnumerable<TestCaseData> TestTimeOffData() {
			StreamReader re = new StreamReader("data/bluesource-timeoff-test.csv");
			var csv = new CsvReader (re);

			while (csv.Read ()) {
				yield return new TestCaseData (
					csv.GetField<string> ("Name"),
					csv.GetField<DateTime> ("Start"),
					csv.GetField<DateTime> ("End"),
					csv.GetField<string> ("Type"),
					csv.GetField<string> ("Reason"),
					csv.GetField<bool> ("Half-Day"),
					csv.GetField<float> ("Days"),
					csv.GetField<bool> ("Succeeds")
				).SetName(csv.GetField<string>("Test")).SetDescription (csv.GetField<string> ("Description"));
			}
		}

		public IEnumerable<TestCaseData> TestTotalTimeOffData() {
			StreamReader re = new StreamReader("data/bluesource-total-timeoff-test.csv");
			var csv = new CsvReader (re);

			while (csv.Read ()) {
				yield return new TestCaseData (
					csv.GetField<string> ("Name")
				).SetName(csv.GetField<string>("Test")).SetDescription (csv.GetField<string> ("Description"));
			}
		}

		public IEnumerable<TestCaseData> TestUsedTimeOffData() {
			StreamReader re = new StreamReader("data/bluesource-used-timeoff-test.csv");
			var csv = new CsvReader (re);

			while (csv.Read ()) {
				yield return new TestCaseData (
					csv.GetField<string> ("Name")
				).SetName(csv.GetField<string>("Test")).SetDescription (csv.GetField<string> ("Description"));
			}
		}
	}
}

