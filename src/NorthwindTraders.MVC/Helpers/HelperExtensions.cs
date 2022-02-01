using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NorthwindTraders.MVC.Helpers
{
    public static class HelperExtensions
    {
        /// <summary>
        /// this is a very simple example of creating an extension method. although there are more robust open source projects for ToSelectList helpers
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="text">Display text for the DDL</param>
        /// <param name="value">Value for the DDL</param>
        /// <returns></returns>
        public static SelectList ToSelectList<T>(this IEnumerable<T> collection, Func<T, string> text, Func<T, object> value)
        {
            return new SelectList(collection.Select(i => new { Value = value(i), Text = text(i) }), "Value", "Text");
        }
    }
}