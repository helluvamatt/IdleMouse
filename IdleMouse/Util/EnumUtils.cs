using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace IdleMouse.Util
{
    internal static class EnumUtils
    {
        public static IEnumerable<KeyValuePair<object, string>> GetEnumValueList(Type enumType)
        {
            if (enumType == null) throw new ArgumentNullException("enumType");
            if (!enumType.IsEnum) throw new ArgumentException(enumType.Name + " is not an enum type");
            foreach (var enumVal in Enum.GetValues(enumType))
            {
                string dispStr = enumVal.ToString();
                var enumValMember = enumType.GetMember(dispStr).FirstOrDefault();
                if (enumValMember != null)
                {
                    var desc = enumValMember.GetCustomAttribute<DescriptionAttribute>()?.Description;
                    var displayName = enumValMember.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;
                    if (!string.IsNullOrEmpty(desc)) dispStr = desc;
                    else if (!string.IsNullOrEmpty(displayName)) dispStr = displayName;
                }
                yield return new KeyValuePair<object, string>(enumVal, dispStr);
            }
        }
    }
}
