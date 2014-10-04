using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaPegada.Web.Extensions
{
    public static class EnumExtension
    {
        public static SelectList ToSelectList<TEnum>(this TEnum value)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var list = (from TEnum e in Enum.GetValues(typeof(TEnum))
                        select new { Value = e, Text = e.ToString() });

            return new SelectList(list, "Value", "Text", "Selecione");
        }
    }
}