using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatisticalTracker.Helpers
{
    public class SelectListItemHelper
    {
        public static IEnumerable<SelectListItem> GetMonths()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem{Text = "Jan", Value = "1"},
                new SelectListItem{Text = "Feb", Value = "2"},
                new SelectListItem{Text = "Mar", Value = "3"},
                new SelectListItem{Text = "Apr", Value = "4"},
                new SelectListItem{Text = "May", Value = "5"},
                new SelectListItem{Text = "Jun", Value = "6"},
                new SelectListItem{Text = "Jul", Value = "7"},
                new SelectListItem{Text = "Aug", Value = "8"},
                new SelectListItem{Text = "Sep", Value = "9"},
                new SelectListItem{Text = "Oct", Value = "10"},
                new SelectListItem{Text = "Nov", Value = "11"},
                new SelectListItem{Text = "Dec", Value = "12"}
            };
            return items;
        }

        public static IEnumerable<SelectListItem> GetDays()
        {
            IList<SelectListItem> items = new List<SelectListItem>();
            var item = new SelectListItem();
            for (int i = 1; i < 32; i++)
            {
                item = new SelectListItem { Text = "" + i + "", Value = "" + i + "" };
                items.Add(item);
            }
            return items;
        }

        public static IEnumerable<SelectListItem> GetYears()
        {
            IList<SelectListItem> items = new List<SelectListItem>();
            var item = new SelectListItem();
            for (int i = 1900; i < DateTime.Now.Year + 1; i++)
            {
                item = new SelectListItem { Text = "" + i + "", Value = "" + i + "" };
                items.Add(item);
            }
            return items;
        }
    }
}