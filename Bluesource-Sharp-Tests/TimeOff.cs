using System;

namespace BluesourceSharpTests
{
	public class TimeOff
	{
		public class TimeOffLimits : TimeOff {}
		public class TimeOffUsed : TimeOff {}

		public float sick { get; set; }
		public float vacation { get; set; }
		public float floating { get; set; }

		public TimeOff ()
		{
		}

		public override bool Equals(Object obj) 
		{
			// Check for null values and compare run-time types.
			if (obj == null || GetType() != obj.GetType()) 
				return false;

			TimeOff p = (TimeOff)obj;
			return (sick == p.sick) && (vacation == p.vacation) && (floating == p.floating);
		}

		public override int GetHashCode() 
		{
			return (int) (sick + vacation + floating);
		}

		public String ToString()
		{
			return "Sick Days: " + sick + ", Vacation Days: " + vacation + ", Floating Holidays: " + floating;
		}
	}
}