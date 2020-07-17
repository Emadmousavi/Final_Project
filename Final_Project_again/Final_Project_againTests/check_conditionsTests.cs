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
	public class check_conditionsTests
	{
		[TestMethod()]
		public void codeTest()
		{
			Assert.AreEqual(true, check_conditions.code("1234567891"));
			Assert.AreEqual(true, check_conditions.code("0000000000"));
			Assert.AreEqual(false, check_conditions.code("123456789"));
		}

		[TestMethod()]
		public void email_checkTest()
		{
			Assert.AreEqual(true, check_conditions.email_check("emad@gmail.com"));
			Assert.AreEqual(true, check_conditions.email_check("e@gmail.c"));
			Assert.AreEqual(false, check_conditions.email_check("emadgmail.com"));
			Assert.AreEqual(false, check_conditions.email_check("emad@gmailcom"));

		}
	}
}