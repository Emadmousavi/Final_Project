using Microsoft.VisualStudio.TestTools.UnitTesting;
using Final_Project_again;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_again.Tests
{
	[TestClass()]
	public class ListTests
	{
		[TestMethod()]
		public void RemoveWhitespaceTest()
		{
			Assert.AreEqual("HotDog", List.RemoveWhitespace("  Hot Dog "));
		}
	}
}