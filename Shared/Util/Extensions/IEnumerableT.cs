﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionTreeTransform.Util {
    public static class IEnumerableTExtensions {
        public static bool None<T>(this IEnumerable<T> src, Func<T, bool> predicate = null) {
            if (predicate != null) { return !src.Any(predicate); }
            return !src.Any();
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> src, Action<T> action) {
            foreach (var item in src) {
                action(item);
            }
            return src;
        }
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> src, Action<T, int> action) {
            var current = 0;
            foreach (var item in src) {
                action(item, current);
                current += 1;
            }
            return src;
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> src) => src.ToDictionary(t => t.Item1, t => t.Item2);

        public static string Joined<T>(this IEnumerable<T> source, string delimiter = ",", Func<T, string> selector = null) {
            if (source == null) { return ""; }
            if (selector == null) { return string.Join(delimiter, source); }
            return string.Join(delimiter, source.Select(selector));
        }
        public static string Joined<T>(this IEnumerable<T> source, string delimiter, Func<T, int, string> selector) {
            if (source == null) { return ""; }
            if (selector == null) { return string.Join(delimiter, source); }
            return string.Join(delimiter, source.Select(selector));
        }

        public static IEnumerable<(TFirst, TSecond)> Zip<TFirst, TSecond>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second) => first.Zip(second, (x, y) => (x, y));

        public static IEnumerable<T> Ordered<T>(this IEnumerable<T> src) => src.OrderBy(x => x);

        public static void AddRangeTo<T>(this IEnumerable<T> src, ICollection<T> dest) => dest.AddRange(src);

        /// <summary>Returns an element If the sequence has exactly one element. Returns the default of T if there is more or less than one element.</summary>
        public static T SingleOrDefaultExt<T>(this IEnumerable<T> src) {
            if (src == null) { return default; }
            T ret = default;
            var counter = 0;
            foreach (var item in src.Take(2)) {
                if (counter == 1) { return default; }
                ret = item;
                counter += 1;
            }
            return ret;
        }
    }
}