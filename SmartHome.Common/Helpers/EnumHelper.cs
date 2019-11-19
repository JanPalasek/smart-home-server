using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartHome.Common.Helpers
{
    public class EnumHelper
    {
        public static IList<TEnum> GetAllValues<TEnum>()
        {
            var enumValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();

            return enumValues;
        }
    }
}