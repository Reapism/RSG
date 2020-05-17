using System;
using System.ComponentModel;

namespace RSG.Core.Extensions
{
    /// <summary>
    /// Extensions for enumerated values.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the <see cref="DescriptionAttribute"/> text
        /// from an enumerated type.
        /// </summary>
        /// <typeparam name="TEnum">An Enum value.</typeparam>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>The description of an enum value, or calling
        /// ToString() on it.</returns>
        public static string GetDescription<TEnum>(this TEnum enumValue)
            where TEnum : struct, IConvertible
        {
            Type type = enumValue.GetType();
            if (!type.IsEnum)
                throw new ArgumentException("T must be an enumerated type.");

            System.Reflection.MemberInfo[] memberInfo = type.GetMember(enumValue.ToString());
            if (memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumValue.ToString();
        }
    }
}
