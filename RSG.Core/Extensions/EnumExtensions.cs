﻿using System;
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
            var type = enumValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type.");
            }

            var memberInfo = type.GetMember(enumValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return enumValue.ToString();
        }

        public static TEnum GetEnumValue<TEnum>(string value)
            where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            TEnum val = ((TEnum[])Enum.GetValues(typeof(TEnum)))[0];
            if (!string.IsNullOrEmpty(value))
            {
                foreach (TEnum enumValue in (TEnum[])Enum.GetValues(typeof(TEnum)))
                {
                    if (enumValue.ToString().ToUpper().Equals(value.ToUpper()))
                    {
                        val = enumValue;
                        break;
                    }
                }
            }

            return val;
        }
    }
}
