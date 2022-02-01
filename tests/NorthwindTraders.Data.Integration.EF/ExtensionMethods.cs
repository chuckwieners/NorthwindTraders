using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NorthwindTraders.Data.Integration.EF
{
    [TestClass]
    public class ExtensionMethods
    {
        [TestMethod]
        public void TestMethod1()
        {
            var name = "Neill";
            var result = name.PullDownTheZip();
            Assert.IsTrue(result == "Neill pull down the zip");
        }
    }

    //EXTENSION METHODS ----
    //to create an extension method these are the rules:
    //  1) has to be declared in a static class
    //  2) the method has to be static
    //  3) the 'this' keyword is used to specify what 
    //      data type is being extended

    public static class StringExtensions
    {
        public static string PullDownTheZip(this string str)
        {
            return string.Format("{0} pull down the zip", str);
        }
    }
}
