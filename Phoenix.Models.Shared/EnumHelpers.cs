using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Models.Shared
{
    public static class EnumHelpers
    {
        /// <summary>
        /// Get the name of an enum value
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumName(Type enumType, object value)
        {
            return Enum.GetName(enumType, value);
        }
    }
}
