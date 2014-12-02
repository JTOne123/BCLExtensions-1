BCLExtensions
=============

Base Class Library Extensions for C# base class library classes, across .Net, Silverlight, WinRT.

For now, this is build upon the most generic PCL possible, but may explore specific additional components if required.

[![Build status](https://ci.appveyor.com/api/projects/status/j06g6b18e976wx3b)](https://ci.appveyor.com/project/MarkClearwater/bclextensions)

PCL Profile(Profile328)
-----------------------
* .Net Framework 4
* Silverlight 5
* Windows 8/8.1(winrt)
* Windows Phone 8.1
* Window Phone Silverlight 8
* Xamarin.Android
* Xamarin.iOS

At some point this can include nuget packages.


Proposed Extensions
===================

String Extensions
-----------------
* string Left(this string value, int length)
* string Right(this string value, int length)
* string SafeLeft(this string value, int length)
* string SafeRight(this string value, int length)
* string SafeToString&lt;T&gt;(this T item)
* string SafeTrim(this string value)
* string SafeTrimStart(this string value)
* string SafeTrimEnd(this string value)
* string SafeTruncate(this string value)


Timespan Extensions
-------------------
* TimeSpan Years(this int value)
* TimeSpan Months(this int value)
* TimeSpan Weeks(this int value)
* TimeSpan Days(this int value)
* TimeSpan Hours(this int value)
* TimeSpan Minutes(this int value)
* TimeSpan Seconds(this int value)
* TimeSpan Milliseconds(this int value)


DateTime Extensions
-------------------
* DateTime Ago(this TimeSpan interval)
* DateTime Since(this TimeSpan interval, DateTime origin)


(Mutable)Collection Extensions
-------------------
* ICollection&lt;T&gt; RemoveEach&lt;T&gt;(this ICollection&lt;T&gt; collecton)
* ICollection&lt;T&gt; RemoveEach&lt;T&gt;(this ICollection&lt;T&gt; collecton, Func&lt;T, bool&gt; whereExpression)
* ICollection&lt;T&gt; RemoveEach&lt;T&gt;(this ICollection&lt;T&gt; collecton, Func&lt;T, int, bool&gt; whereExpression)
* ICollection&lt;T&gt; RemoveEachInReverse&lt;T&gt;(this ICollection&lt;T&gt; collection)
* ICollection&lt;T&gt; RemoveEachInReverse&lt;T&gt;(this ICollection&lt;T&gt; collection, Func&lt;T, bool&gt; whereExpression)
* ICollection&lt;T&gt; RemoveEachInReverse&lt;T&gt;(this ICollection&lt;T&gt; collection, Func&lt;T, int, bool&gt; whereExpression)
* ICollection&lt;T&gt; RemoveEachByIndex&lt;T&gt;(this ICollection&lt;T&gt; collection)
* ICollection&lt;T&gt; RemoveEachByIndex&lt;T&gt;(this ICollection&lt;T&gt; collection, Func&lt;T, bool&gt; whereExpression)
* ICollection&lt;T&gt; RemoveEachByIndex&lt;T&gt;(this ICollection&lt;T&gt; collection, Func&lt;T, int, bool&gt; whereExpression)
* ICollection&lt;T&gt; RemoveEachByIndexInReverse&lt;T&gt;(this ICollection&lt;T&gt; collection)
* ICollection&lt;T&gt; RemoveEachByIndexInReverse&lt;T&gt;(this ICollection&lt;T&gt; collection, Func&lt;T, bool&gt; whereExpression)
* ICollection&lt;T&gt; RemoveEachByIndexInReverse&lt;T&gt;(this ICollection&lt;T&gt; collection, Func&lt;T, int, bool&gt; whereExpression)
* ICollection&lt;T&gt; OrNullIfEmpty(this ICollection&lt;T&gt; items)
* ICollection&lt;T&gt; OrEmptyIfNull(this ICollection&lt;T&gt; items)


IList Extensions
----------------------
* IList&lt;T&gt; OrNullIfEmpty(this IList&lt;T&gt; items)
* IList&lt;T&gt; OrEmptyIfNull(this IList&lt;T&gt; items)


List Extensions
----------------------
* List&lt;T&gt; OrNullIfEmpty(this List&lt;T&gt; items)
* List&lt;T&gt; OrEmptyIfNull(this List&lt;T&gt; items)


Array Extensions
----------------------
* T[] OrNullIfEmpty(this T[] items)
* T[] OrEmptyIfNull(this T[] items)


IEnumerable Extensions
----------------------
* SafeToList&lt;T&gt;(this IEnumerable&lt;T&gt; items)
* SafeToArray&lt;T&gt;(this IEnumerable&lt;T&gt; items)
* SafeToDictionary&lt;TItem TKey&gt;(this IEnumerable&lt;T&gt; items, Func&lt;TItem TKey&gt; keySelector)
* SafeToHashSet&lt;TItem TKey&gt;(this IEnumerable&lt;T&gt; items)
* SafeToHashSet&lt;TItem TKey&gt;(this IEnumerable&lt;T&gt; items, IEqualityComparer<T> equalityComparer)


Dictionary Extensions
---------------------
* Dictionary&lt;TKey, TValue&gt; OrNullIfEmpty(this Dictionary&lt;TKey, TValue&gt; dictionary)
* Dictionary&lt;TKey, TValue&gt; OrEmptyIfNull(this Dictionary&lt;TKey, TValue&gt; dictionary)


Completed Extensions
====================

Object Extensions
-----------------
* void EnsureIsNotNull(this object instance)
* void EnsureIsNotNull(this object instance, string argumentName)
* bool IsNotNull(this object instance)
* bool IsNull(this object instance)


Generic Extensions
-----------------
* T GetValueOrDefault&lt;T&gt;(this T item, T defaultValue) where T : object


IEnumerable Extensions
----------------------
* bool IsNotEmpty&lt;TItem&gt;(this IEnumerable&lt;TItem&gt; items)
* bool IsNullOrEmpty&lt;TItem&gt;(this IEnumerable&lt;TItem&gt; items)
* IEnumerable&lt;TItem&gt; OrEmptyIfNull&lt;TItem&gt;(this IEnumerable&lt;TItem&gt; items)
* IEnumerable&lt;TItem&gt; OrNullIfEmpty&lt;TItem&gt;(this IEnumerable&lt;TItem&gt; items)


String Extensions
-----------------
* string FormatWith(this string format, params object[] parameters)
* bool IsNotNullOrWhitespace(this String s)
* bool IsNullOrWhitespace(this String s)
* string ValueOrEmptyIfNull(this string value)
* string ValueOrEmptyIfNullOrWhitespace(this string value)
* string ValueOrIfNull(this string value, String replacement)
* string ValueOrIfNullOrWhitespace(this string value, String replacement)
* string ValueOrNullIfWhitespace(this string value)