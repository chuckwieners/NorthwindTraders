using System.Web.Mvc;

namespace NorthwindTraders.MVC.Helpers
{
    /// <summary>
    /// Simple example of extending the MVC HTML Helper object
    /// http://www.asp.net/mvc/tutorials/older-versions/views/creating-custom-html-helpers-cs
    /// </summary>
    public static class HtmlHelperExtensions
    {
        public static string BooleanToYesNo(this HtmlHelper helper, bool b)
        {
            return b ? "Yes" : "No";
        }
    }
}