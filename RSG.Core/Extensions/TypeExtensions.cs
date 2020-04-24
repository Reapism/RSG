﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RSG.Core.Extensions
{
    /// <summary>
    /// Extensions for the <see cref="Type"/> class.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets all public members of type <typeparamref name="T"/>
        /// given a <paramref name="type"/>.
        /// </summary>
        /// <typeparam name="T">The type of public constants to search.</typeparam>
        /// <param name="type">The class containing the public constants of type
        /// <typeparamref name="T"/>.</param>
        /// <returns>A collection of public constants of type <typeparamref name="T"/>
        /// in the class <paramref name="type"/>.</returns>
        public static IEnumerable<T> GetPublicConstants<T>(this Type type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(field => field.IsLiteral && !field.IsInitOnly && field.FieldType == typeof(T))
                .Select(constType => (T)constType.GetRawConstantValue());
        }
    }
}