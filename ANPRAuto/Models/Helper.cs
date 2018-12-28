using ANPRData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ANPRAuto.Models
{
    public static class Helper
    {
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { Id = e, Name = e.ToString() };
            return new SelectList(values, "Id", "Name", enumObj);
        }

        public static SelectList GetLaneList()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            var cam1 = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["cam1"]) ? ConfigurationManager.AppSettings["cam1"].Split('-') : null;
            var cam2 = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["cam2"]) ? ConfigurationManager.AppSettings["cam2"].Split('-') : null;
            var cam3 = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["cam3"]) ? ConfigurationManager.AppSettings["cam3"].Split('-') : null;
            var cam4 = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["cam4"]) ? ConfigurationManager.AppSettings["cam4"].Split('-') : null;
            var cam5 = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["cam5"]) ? ConfigurationManager.AppSettings["cam5"].Split('-') : null;
            var cam6 = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["cam6"]) ? ConfigurationManager.AppSettings["cam6"].Split('-') : null;
            var cam7 = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["cam7"]) ? ConfigurationManager.AppSettings["cam7"].Split('-') : null;
            var cam8 = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["cam8"]) ? ConfigurationManager.AppSettings["cam8"].Split('-') : null;

            myList.Add(new SelectListItem { Value = "-1", Text = "All" });

            if (cam1 != null )
                myList.Add(new SelectListItem { Value = cam1[0].Trim(), Text = cam1[0].Trim() });
            if (cam2 != null )
                myList.Add(new SelectListItem { Value = cam2[0].Trim(), Text = cam2[0].Trim() });
            if (cam3 != null )
                myList.Add(new SelectListItem { Value = cam3[0].Trim(), Text = cam3[0].Trim() });
            if (cam4 != null )
                myList.Add(new SelectListItem { Value = cam4[0].Trim(), Text = cam4[0].Trim() });
            if (cam5 != null )
                myList.Add(new SelectListItem { Value = cam5[0].Trim(), Text = cam5[0].Trim() });
            if (cam6 != null )
                myList.Add(new SelectListItem { Value = cam6[0].Trim(), Text = cam6[0].Trim() });
            if (cam7 != null )
                myList.Add(new SelectListItem { Value = cam7[0].Trim(), Text = cam7[0].Trim() });
            if (cam8 != null )
                myList.Add(new SelectListItem { Value = cam8[0].Trim(), Text = cam8[0].Trim() });

            return new SelectList(myList, "Value", "Text");
        }

        public static SelectList GetCountryList()
        {
            dbANPREntities _context = new dbANPREntities();
            return new SelectList(_context.tblCountries.Select(x=> new { Id = x.Id, Name = x.CountryName }),"Id", "Name");
        }

       
    }

}