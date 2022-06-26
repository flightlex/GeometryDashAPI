﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace GeometryDashAPI
{
    public static class Extensions
    {
        public static T GetAttributeOfSelected<T>(this Enum value) where T : Attribute
        {
            var info = value.GetType().GetMember(value.ToString())[0];
            var attributes = info.GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? (T)attributes[0] : null;
        }

        public static Type GetMemberType(this MemberInfo member)
        {
            return member switch
            {
                PropertyInfo info => info.PropertyType,
                FieldInfo info => info.FieldType,
                _ => throw new ArgumentException("Not supported member type", nameof(member))
            };
        }

        public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> create)
        {
            if (!dictionary.TryGetValue(key, out var value))
                dictionary.Add(key, value = create(key));
            return value;
        }
    }
}
