using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Component = UnityEngine.Component;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;


namespace MathExt
{

    // Action with reference argument (lambda: [(ref T arg) => body])
    public delegate void RefAction<T>(ref T arg);

    // Func with reference argument
    public delegate TResult RefFunc<T, out TResult>(ref T arg);


    // --------------------------------



    #region [Math-Common]


    public static class Mathcx
    {
        public static readonly CultureInfo cultInfo = CultureInfo.InvariantCulture;

        private const long TicksPerMillisecond = 10000L;
        private const long TicksPerSecond = TicksPerMillisecond * 1000L;
        private const long TicksPerMinute = TicksPerSecond * 60L;
        private const long TicksPerHour = TicksPerMinute * 60L;
        private const long TicksPerDay = TicksPerHour * 24L;

        private const double MillisecondsPerTick = 1.0 / TicksPerMillisecond;   // 0.0001D;
        private const double SecondsPerTick = 1.0 / TicksPerSecond;             // 0.0000001D;
        private const double MinutesPerTick = 1.0 / TicksPerMinute;
        private const double HoursPerTick = 1.0 / TicksPerHour;
        private const double DaysPerTick = 1.0 / TicksPerDay;

        private const int TicksPerMillis = 10000;
        private const float MillisPerTick = 1.0f / TicksPerMillis; // 0.0001F;

        private const int MillisPerSecond = 1000;
        private const int MillisPerMinute = MillisPerSecond * 60;
        private const int MillisPerHour = MillisPerMinute * 60;
        private const int MillisPerDay = MillisPerHour * 24;

        private const float SecondsPerMillis = 1.0f / MillisPerSecond;
        private const float MinutesPerMillis = 1.0f / MillisPerMinute;
        private const float HoursPerMillis = 1.0f / MillisPerHour;
        private const float DaysPerMillis = 1.0f / MillisPerDay;

        private const long MinTicks = 0L;                   // DateTime.MinValue.Ticks
        private const long MaxTicks = 3155378975999999999L; // DateTime.MaxValue.Ticks

        private const long MaxMilliSeconds = Int64.MaxValue / TicksPerMillisecond; //  922337203685477L;
        private const long MinMilliSeconds = Int64.MinValue / TicksPerMillisecond; // -922337203685477L;

        private const long MaxSeconds = Int64.MaxValue / TicksPerSecond; //  922337203685L;
        private const long MinSeconds = Int64.MinValue / TicksPerSecond; // -922337203685L;


        public static float TicksToMillis(long t) { return t * MillisPerTick; }

        public static double TicksToMilliseconds(long t) { return t * MillisecondsPerTick; }

        //public static double TicksToSeconds(long t) { return t * SecondsPerTick; }

        //public static float MillisToSeconds(float t) { return t * SecondsPerMillis; }



        public static float Timestamp { get { return DateTime.UtcNow.Ticks * MillisPerTick; } }
        public static float TimeDelta(float t) { return Timestamp - t; }


        public static long GetProcessorTime { get { return Process.GetCurrentProcess().TotalProcessorTime.Ticks; } }


        /// Environment.TickCount (Int32)
        /// <summary>Milliseconds since the system was started. (precision about 16 ms)</summary>
        /// <remarks>[Int32.MinValue to Int32.MaxValue] (starts at 0, wraps around back to 0 each ~49.7 days)</remarks>
        public static int SysTimeMs { get { return Environment.TickCount & Int32.MaxValue; } }


        /// DateTime.Ticks (Int64)
        /// <summary>Number of ticks (100-nanosecond) since 12:00am (0:00:00 UTC) 01/01/0001. (precision depends on the system; 1 to 16 ms)</summary>
        /// <remarks>[DateTime.MinValue.Ticks to DateTime.MaxValue.Ticks]</remarks>
        public static long GetTimeTicks { get { return DateTime.UtcNow.Ticks; } } // & Int64.MaxValue // & DateTime.MaxValue.Ticks

        public static double GetTimeDeltaMs(long t) { return TicksToMilliseconds(GetTimeTicks - t); }

        public static double GetTimeMs { get { return DateTime.UtcNow.Ticks * MillisecondsPerTick; } }
        public static double GetTimeDeltaMs(double t) { return (GetTimeMs - t); }


        /// Stopwatch (Int64) (UNRELIABLE)
        /// <summary>Timestamp to accurately measure elapsed time.
        /// (high-resolution performance counter, or DateTime.Ticks if not supported)</summary>
        /// <returns>Number of elapsed intervals (ticks) of 1 second divided by the Frequency. (tick = 1.0 / Frequency)</returns>
        /// <remarks>Note: the return value is not an unit of seconds, See <see cref="HiResTimeMs"/> on conversion.</remarks>
        public static long HiResTimestamp { get { return Stopwatch.GetTimestamp(); } } // & Int64.MaxValue
        //public static long HiResTimeTicks(long t) { return (long)(t * (10000000.0 / Stopwatch.Frequency)); }

        public static double HiResTimeMs(long t) { return (t * (1000.0 / Stopwatch.Frequency)); }

        public static double HiResTimeDeltaMs(long t) { return HiResTimeMs(HiResTimestamp - t); }





        // iterate from tr up to max 2 parents.
        public static T GetComponentUp<T>(this Transform tr, int depth = 0) where T : MonoBehaviour
        {
            while (true)
            {
                if (depth++ > 2)
                    return null;
                if (tr == null)
                    return null;
                var plyr = tr.GetComponent<T>();
                if (plyr != null)
                    return plyr;
                tr = tr.parent;
            }
        }




        public static bool IsNull(this object source)
        {
            return source == null;
        }

        public static bool IsNaN(this float source)
        {
            return float.IsNaN(source);
        }
        public static bool IsNaN(this double source)
        {
            return double.IsNaN(source);
        }

        public static bool IsNullOrWhiteSpace(this string source) { return source == null || source.Trim().Length == 0; }


        /// <summary>
        /// Indicates whether the current string matches the supplied wildcard pattern.  Behaves the same
        /// as VB's "Like" Operator.
        /// </summary>
        /// <param name="s">The string instance where the extension method is called</param>
        /// <param name="wildcardPattern">The wildcard pattern to match.  Syntax matches VB's Like operator.</param>
        /// <returns>true if the string matches the supplied pattern, false otherwise.</returns>
        /// <remarks>See http://msdn.microsoft.com/en-us/library/swf8kaxw(v=VS.100).aspx </remarks>
        public static bool IsLike(this string s, string wildcardPattern)
        {
            if (s == null || String.IsNullOrEmpty(wildcardPattern)) return false;
            // turn into regex pattern, and match the whole string with ^$
            var regexPattern = "^" + Regex.Escape(wildcardPattern) + "$";

            // add support for ?, #, *, [], and [!]
            regexPattern = regexPattern.Replace(@"\[!", "[^")
                           .Replace(@"\[", "[")
                           .Replace(@"\]", "]")
                           .Replace(@"\?", ".")
                           .Replace(@"\*", ".*")
                           .Replace(@"\#", @"\d");

            var result = false;
            try
            {
                result = Regex.IsMatch(s, regexPattern);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(String.Format("Invalid pattern: {0}", wildcardPattern), ex);
            }
            return result;
        }



        public static T As<T>(this string strValue, T defaultValue)
        {
            T output = defaultValue;
            if (output == null)
                output = default(T);

            output = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(strValue);

            return output;
        }

        public static T As<T>(this string strValue)
        {
            return strValue.As<T>(default(T));
        }



        public static string RemovePrefix(this string s, string pf)
        {
            return s.StartsWith(pf) ? s.Remove(0, pf.Length) : s;
        }

        public static string RemovePrefix(this string s, params string[] pfs)
        {
            return RemovePrefix(s, pfs.FirstOrDefault(s.StartsWith));
        }


        #region [Hex--string-byte-uint]

        /// <summary>Converts value(0-1) to byte(0-255)</summary>
        public static byte ValueToByte(this float f)
        {
            return (byte)(f <= float.Epsilon ? 0 :
                          f >= 1.0f - float.Epsilon ? 255 :
                          Mathc.FloorToInt(f * 256.0f));
        }

        public static byte HexToByte(this string s)
        {
            byte result;
            if (byte.TryParse(s, NumberStyles.HexNumber, cultInfo, out result))
                return result;
            return 0;
        }

        public static string ToHexString(this byte value, string format = "x2")
        {
            return value.ToString(format, cultInfo);
        }


        /// <summary>Convert an hex string to uint. (witout 0x prefix!)</summary>
        public static uint ParseHex(this string hex)
        {
            return uint.Parse(hex, NumberStyles.HexNumber, cultInfo);
        }
        /// <summary>Try to convert an hex string to uint. (handles possible 0x prefix)</summary>
        public static bool TryParseHex(this string hex, out uint result)
        {
            if (hex.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase))
                hex = hex.Substring(2);
            return uint.TryParse(hex, NumberStyles.HexNumber, cultInfo, out result);
        }
        /// <summary>Try to convert an hex string to uint. (witout 0x prefix!)</summary>
        public static bool TryParseHex2(this string hex, out uint result)
        {
            return uint.TryParse(hex, NumberStyles.HexNumber, cultInfo, out result);
        }


        public static Color ToColorRGBA(this uint rgba)
        {
            return new Color32(
                       (byte)((rgba & 0xff0000) >> 0x10),
                       (byte)((rgba & 0xff00) >> 0x08),
                       (byte)(rgba & 0xff),
                       (byte)((rgba & 0xff000000) >> 0x18));
        }

        public static Color ToColorRGB(this uint rgb)
        {
            return new Color32(
                       (byte)((rgb & 0xff0000) >> 0x10),
                       (byte)((rgb & 0xff00) >> 0x08),
                       (byte)(rgb & 0xff), 0xFF);
        }

        public static Color ToColorARGB(this uint argb)
        {
            return new Color32(
                       (byte)((argb & 0xff000000) >> 0x18),
                       (byte)((argb & 0xff0000) >> 0x10),
                       (byte)((argb & 0xff00) >> 0x08),
                       (byte)(argb & 0xff));
        }


        /// <summary>Convert hex string (RGBA) to color. (witout 0x prefix!)</summary>
        public static Color HexToColorRGBA(this string hex)
        {
            return new Color32(
                       HexToByte(hex.Substring(0, 2)),
                       HexToByte(hex.Substring(2, 2)),
                       HexToByte(hex.Substring(4, 2)),
                       HexToByte(hex.Substring(6, 2)));
        }

        /// <summary>Convert hex string (RGB) to color. (witout 0x prefix!)</summary>
        public static Color HexToColorRGB(this string hex, float alpha)
        {
            return HexToColorRGB(hex, alpha.ValueToByte());
        }
        /// <summary>Convert hex string (RGB) to color. (witout 0x prefix!)</summary>
        public static Color HexToColorRGB(this string hex, byte alpha = (byte)255)
        {
            return new Color32(
                       HexToByte(hex.Substring(0, 2)),
                       HexToByte(hex.Substring(2, 2)),
                       HexToByte(hex.Substring(4, 2)), alpha);
        }

        /// <summary>Convert hex string (RGB or RGBA) to color. (witout 0x prefix!)</summary>
        public static Color HexToColor(this string hex)
        {
            return hex.Length == 6 ? HexToColorRGB(hex) : hex.Length == 8 ? HexToColorRGBA(hex) : Color.clear;
        }


        /// <summary>Convert to hex string (RGBA).</summary>
        public static string ToHexStringRGBA(this Color color, string format = "x2") { return HexStringRGBA(color, format); }
        /// <summary>Convert to hex string (RGBA).</summary>
        public static string HexStringRGBA(this Color32 color, string format = "x2")
        {
            return color.r.ToHexString(format) + color.g.ToHexString(format) + color.b.ToHexString(format) + color.a.ToHexString(format);
        }

        /// <summary>Convert to hex string (RGB).</summary>
        public static string ToHexStringRGB(this Color color, string format = "x2") { return HexStringRGB(color, format); }
        /// <summary>Convert to hex string (RGB).</summary>
        public static string HexStringRGB(this Color32 color, string format = "x2")
        {
            return color.r.ToHexString(format) + color.g.ToHexString(format) + color.b.ToHexString(format);
        }

        #endregion [Hex--string-byte-uint]


        #region [EnumerationExtensions]


        public static bool TryParse<T>(this Enum theEnum, string valueToParse, out T returnValue)
        {
            returnValue = default(T);
            if (Enum.IsDefined(typeof(T), valueToParse))
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                returnValue = (T)converter.ConvertFromString(valueToParse);
                return true;
            }
            return false;
        }


        //public void AddFlag(Enum flag) { flagfield |= flag; }
        //public void RemoveFlag(Enum flag) { flagfield &= ~flag; }
        //public void SetFlag(Enum flag, bool set) { flagfield = set ? flagfield | flag : flagfield & ~flag; }
        //public bool HasFlag(Enum flag) { return ((flagfield & flag) == flag); }
        //public bool HasFlags(Enum flag1, Enum flag2) { return ((flagfield & (flag1 | flag2)) == (flag1 | flag2)); }


        //checks if the value contains the provided type
        public static bool HasBoth<T>(this Enum type, T value1, T value2)
        {
            try
            {
                return (((int)(object)type &
                         ((int)(object)value1 | (int)(object)value2)) ==
                        ((int)(object)value1 | (int)(object)value2));
            }
            catch
            {
                return false;
            }
        }

        //checks if the value contains the provided type
        public static bool Has<T>(this Enum type, T value)
        {
            try
            {
                return (((int)(object)type & (int)(object)value) == (int)(object)value);
            }
            catch
            {
                return false;
            }
        }

        //checks if the value is only the provided type
        public static bool Is<T>(this Enum type, T value)
        {
            try
            {
                return (int)(object)type == (int)(object)value;
            }
            catch
            {
                return false;
            }
        }

        //appends a value
        public static T Add<T>(this Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type | (int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "Could not append value from enumerated type '{0}'.",
                        typeof(T).Name
                    ), ex);
            }
        }

        //completely removes the value
        public static T Remove<T>(this Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type & ~(int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "Could not remove value from enumerated type '{0}'.",
                        typeof(T).Name
                    ), ex);
            }
        }


        public static T Set<T>(this Enum type, T value, bool set)
        {
            return set ? Add(type, value) : Remove(type, value);
        }


        #endregion [EnumerationExtensions]


        #region [Array/IEnumerable Extensions]


        public static T GetRandomEnum<T>()
        {
            return Enum.GetValues(typeof(T)).GetRandom<T>();
        }

        public static T SetRandomEnum<T>(this Enum enumObj)
        {
            if (enumObj == null)
                throw new ArgumentNullException("enumObj");

            enumObj = GetRandom<Enum>(Enum.GetValues(typeof(T)));

            return Enum.GetValues(enumObj.GetType()).GetRandom<T>();
        }


        public static object GetRandom(this Array arr)
        {
            return arr.GetValue(Random.Range(0, arr.Length));
        }

        public static T GetRandom<T>(this Array arr)
        {
            return (T)arr.GetValue(Random.Range(0, arr.Length));
        }

        public static T GetRandom_Array<T>(this T[] arr)
        {
            return arr[Random.Range(0, arr.Length)];
        }

        public static T GetRandomSafe_Array<T>(this T[] arr)
        {
            if (arr != null && arr.Length > 0)
                return arr[Random.Range(0, arr.Length)];
            return default(T);
        }


        /// <summary>Returns a random element from a sequence.</summary>
        /// <returns>A random element in the source sequence.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IList`1"/> to return an element from.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        public static TSource GetRandomElement_List<TSource>(this IList<TSource> source)
        {
            //return list.IsNullOrEmpty() ? default(TSource) : list[Random.Range(0, list.Count)];
            return source[Random.Range(0, source.Count)];
        }

        /// <summary>Returns a random element from a sequence.</summary>
        /// <returns>A random element in the source sequence.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1"/> to return an element from.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        public static TSource GetRandomElement<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                int count = list.Count;
                if (count <= 0)
                    throw new InvalidOperationException("Sequence contains no elements");

                return list[Random.Range(0, count)];
            }
            else
            {
                int count = 0;
                ICollection<TSource> collection = source as ICollection<TSource>;
                if (collection != null)
                    count = collection.Count;
                else
                    using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                        while (enumerator.MoveNext())
                            checked { ++count; }
                            if (count <= 0)
                                throw new InvalidOperationException("Sequence contains no elements");

                int index = Random.Range(0, count);
                using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        if (index == 0)
                            return enumerator.Current;
                        --index;
                    }
                }
            }

            throw new Exception();
        }



        // RandomSampleExtension
        public static IEnumerable<T> RandomSample<T>(
            this IEnumerable<T> source, int count, bool allowDuplicates)
        {
            if (source == null) throw new ArgumentNullException("source");
            return RandomSampleIterator(source, count, allowDuplicates);
        }
        private static IEnumerable<T> RandomSampleIterator<T>(
            IEnumerable<T> source, int count, bool allowDuplicates)
        {
            // take a copy of the current list
            List<T> buffer = new List<T>(source);

            count = Math.Min(count, buffer.Count);
            if (count <= 0)
                yield break;

            // iterate count times and "randomly" return one of the elements
            for (int i = 1; i <= count; i++)
            {
                // maximum index actually buffer.Count -1.
                int randomIndex = Random.Range(0, buffer.Count);
                yield return buffer[randomIndex];
                if (!allowDuplicates)
                    // remove the element so it can't be selected a second time
                    buffer.RemoveAt(randomIndex);
            }
        }


        /// <summary>
        /// Converts an array of any type to <see cref="T:System.Collections.Generic.List`1"/>.
        /// (null will not be added to the collection; If the array is null,
        /// then a new instance of <see cref="T:System.Collections.Generic.List`1"/> is returned)
        /// </summary>
        /// <param name="items">The array to convert.</param>
        /// <param name="mapFunction">func used to convert each item</param>
        public static List<T> ToList<T>(this Array items, Func<object, T> mapFunction)
        {
            if (items == null || mapFunction == null)
                return new List<T>();

            List<T> coll = new List<T>();
            for (int i = 0; i < items.Length; i++)
            {
                T val = mapFunction(items.GetValue(i));
                if (val != null)
                    coll.Add(val);
            }
            return coll;
        }

        /// <summary>
        /// Sets all values.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array that will be modified.</typeparam>
        /// <param name="array">The one-dimensional, zero-based array</param>
        /// <param name="value">The value.</param>
        /// <returns>A reference to the changed array.</returns>
        public static T[] SetAllValues<T>(this T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
            return array;
        }


        public static IList<T> Swap<T>(this IList<T> list, int index1, int index2)
        {
            T tmp = list[index2];
            list[index2] = list[index1];
            list[index1] = tmp;
            return list;
        }

        /// <summary>Shuffle an array in O(n) time (fastest possible way in theory and practice!)</summary>
        public static T[] Shuffle<T>(this T[] list) { return Shuffle(list, i => Random.Range(0, i)); }
        /// <summary>Shuffle an array in O(n) time (fastest possible way in theory and practice!)</summary>
        public static T[] Shuffle<T>(this T[] list, Func<int, int> randomGen)
        {
            for (int i = list.Length - 1; i > 0; i--)
            {
                int j = randomGen(i);
                var e = list[i];
                list[i] = list[j];
                list[j] = e;
            }
            return list;
        }


        /// <summary>Randomly shuffles items within a list.</summary>
        /// <param name="list">The list to shuffle.</param>
        public static void Shuffle<T>(IList<T> list) { Shuffle(list, i => Random.Range(0, i)); }
        /// <summary>Randomly shuffles items within a list.</summary>
        /// <param name="list">The list to shuffle.</param>
        /// <param name="rng">Random number generator.</param>
        public static void Shuffle<T>(IList<T> list, Func<int, int> rng)
        {
            // This approach was suggested by Jon Skeet in a dotNet newsgroup post and
            // is also the technique used by the OpenJDK. The use of rnd.Next(i+1) introduces
            // the possibility of swapping an item with itself, I suspect the reasoning behind this
            // has to do with ensuring the probability of each possible permutation is approximately equal.
            for (int i = list.Count - 1; i > 0; i--)
            {
                int swapIndex = rng(i);
                T tmp = list[swapIndex];
                list[swapIndex] = list[i];
                list[i] = tmp;
            }
        }


        /// <summary>Combines parts of 2 byte arrays.</summary>
        public static byte[] Combine(this byte[] src1, int offset1, int count1, byte[] src2, int offset2, int count2)
        {
            byte[] arr = new byte[count1 + count2];
            for (int i = 0; i < count1; i++)
                arr[i] = src1[offset1 + i];
            for (int i = 0; i < count2; i++)
                arr[count1 + i] = src2[offset2 + i];
            return arr;
        }

        /// <summary>
        /// Extracts all fields from a string that match a certain regex.
        /// Will convert to desired type through a standard TypeConverter.
        /// (for example see ExtractInts())
        /// </summary>
        public static T[] REExtract<T>(this string s, string regex)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(T));
            if (!tc.CanConvertFrom(typeof(string)))
            {
                throw new ArgumentException("Type does not have a TypeConverter from string", "T");
            }
            if (!string.IsNullOrEmpty(s))
            {
                return
                    Regex.Matches(s, regex)
                    .Cast<Match>()
                    .Select(f => f.ToString())
                    .Select(f => (T)tc.ConvertFrom(f))
                    .ToArray();
            }
            else
                return new T[0];
        }
        public static int[] ExtractInts(this string s)
        {
            return s.REExtract<int>(@"\d+");
        }


        /// <summary>
        /// Copies the values from an array to another.
        /// </summary>
        /// <param name="source">The source array.</param>
        /// <param name="dest">The destination array.</param>
        public static void CopyArrayTo<T>(this T[] source, T[] dest)
        {
            Array.Copy(source, 0, dest, 0, source.Length);
        }
        /// <summary>
        /// Copies the values from an array to another.
        /// </summary>
        /// <param name="source">The source array.</param>
        /// <param name="dest">The destination array.</param>
        public static void CopyTo(this double[] source, double[] dest)
        {
            Buffer.BlockCopy(source, 0, dest, 0, source.Length * 8);
        }
        /// <summary>
        /// Copies the values from an array to another.
        /// </summary>
        /// <param name="source">The source array.</param>
        /// <param name="dest">The destination array.</param>
        public static void CopyTo(this float[] source, float[] dest)
        {
            Buffer.BlockCopy(source, 0, dest, 0, source.Length * 4);
        }


        /// <summary>Checks whether this list is either <see langword="null"/>, or contains 0 items.</summary>
        /// <param name="list">The list to check.</param>
        /// <returns>True if this list is null or empty, else false.</returns>
        public static bool IsNullOrEmpty<TSource>(this IList<TSource> list)
        {
            return list == null || list.Count == 0;
        }

        /// <summary>Checks if any element in this list is null.</summary>
        /// <param name="list">The list to check for null values.</param>
        /// <returns>The index of the first null value in the list if found, -1 if no value is null.</returns>
        public static int FindNullIndex<TSource>(this IList<TSource> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == null)
                    return i;
            }
            return -1;
        }

        public static TSource GetAtIndex<TSource>(this IList<TSource> items, int i)
        {
            if (items != null && i < items.Count)
                return items[i];
            return default(TSource);
        }

        /// <summary><para>Checks if the specified list equals the current list, content-wise.</para>
        /// <para>In other words, it returns <see langword="true"/> if, and only if:</para>
        /// <para> - Both lists have the same number of items;</para>
        /// <para> - For each item in this list, <code>this[index].Equals(other[index])</code>.</para>
        /// </summary>
        /// <typeparam name="TSource">The type of objects in both lists.</typeparam>
        /// <param name="list">The current list.</param>
        /// <param name="other">The list to check for content equality with.</param>
        /// <returns>Whether the contents of the two lists are equal.</returns>
        public static bool ContentEquals<TSource>(this IList<TSource> list, IList<TSource> other)
        {
            if (list.Count != other.Count)
                return false;

            // ReSharper disable LoopCanBeConvertedToQuery
            for (int i = 0; i < list.Count; i++)
                // ReSharper restore LoopCanBeConvertedToQuery
            {
                if (!list[i].Equals(other[i]))
                    return false;
            }
            return true;
        }


        //private static IEnumerable<TSource> WhereIterator<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        //{
        //    int index = -1;
        //    foreach (TSource source1 in source)
        //    {
        //        checked { ++index; }
        //        if (predicate(source1, index))
        //            yield return source1;
        //    }
        //}
        //private static IEnumerable<TResult> SelectIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        //{
        //    int index = -1;
        //    foreach (TSource source1 in source)
        //    {
        //        checked { ++index; }
        //        yield return selector(source1, index);
        //    }
        //}

        private static IEnumerable<TResult> WhereSelectIterator<TSource, TResult>(TSource[] source, Func<TSource, int, TResult> selector)
        {
            for (var index = 0; index < source.Length; index++)
            {
                TResult result = selector(source[index], index);
                if (result != null)
                    yield return result;
            }
        }
        private static IEnumerable<TResult> WhereSelectIterator<TSource, TResult>(IList<TSource> source, Func<TSource, int, TResult> selector)
        {
            for (var index = 0; index < source.Count; index++)
            {
                TResult result = selector(source[index], index);
                if (result != null)
                    yield return result;
            }
        }
        private static IEnumerable<TResult> WhereSelectIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        {
            int index = -1;
            foreach (TSource source1 in source)
            {
                checked { ++index; }
                TResult result = selector(source1, index);
                if (result != null)
                    yield return result;
            }
        }

        /// <summary>Projects each element of a sequence into a new form, and excludes it when null.</summary>
        ///<param name="selector">A transform function to apply to each element.</param>
        public static IEnumerable<TResult> SelectFilter_Array<TSource, TResult>(this TSource[] source, Func<TSource, TResult> selector)
        {
            //return source.Select(selector).Where(result => result != null);
            //foreach (TResult result in source.Select(selector)) if (result != null) yield return result;
            return WhereSelectIterator(source, (src, i) => selector(src));
        }
        /// <summary>Projects each element of a sequence into a new form, and excludes it when null.</summary>
        ///<param name="selector">A transform function to apply to each element.</param>
        public static IEnumerable<TResult> SelectFilter_List<TSource, TResult>(this IList<TSource> source, Func<TSource, TResult> selector)
        {
            //return source.Select(selector).Where(result => result != null);
            //foreach (TResult result in source.Select(selector)) if (result != null) yield return result;
            return WhereSelectIterator(source, (src, i) => selector(src));
        }
        /// <summary>Projects each element of a sequence into a new form, and excludes it when null.</summary>
        ///<param name="selector">A transform function to apply to each element.</param>
        public static IEnumerable<TResult> SelectFilter_IEnumerable<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            //return source.Select(selector).Where(result => result != null);
            //foreach (TResult result in source.Select(selector)) if (result != null) yield return result;
            return WhereSelectIterator(source, (src, i) => selector(src));
        }


        /// <summary>Filters a sequence of values based on a predicate.</summary>
        ///<param name="predicate">A function to test each element for a condition.</param>
        public static TSource[] Filter<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Where(predicate).ToArray();
        }


        ///<summary>Performs the specified action on each element in the array.</summary>
        ///<param name="action">The System.Action<TSource> delegate to perform on each element of the array.</param>
        public static void ForEach_Array<TSource>(this TSource[] source, Action<TSource> action)
        {
            int length = source.Length;
            for (int i = 0; i < length; i++)
            {
                action(source[i]);
            }
        }

        ///<summary>Performs the specified action on each element in the array by incorporating the element's index.</summary>
        ///<param name="action">The System.Action<TSource> delegate to perform on each element of the array.</param>
        public static void ForEach_Array<TSource>(this TSource[] source, Action<TSource, int> action)
        {
            int length = source.Length;
            for (int i = 0; i < length; i++)
            {
                action(source[i], i);
            }
        }

        ///<summary>Finds the index of the first item matching an expression in the array.</summary>
        ///<param name="source">The array to search.</param>
        ///<param name="predicate">The expression to test the items against.</param>
        ///<returns>The index of the first matching item, or -1 if no items match.</returns>
        public static int FindIndex_Array<TSource>(this TSource[] source, Func<TSource, bool> predicate)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (predicate(source[i])) return i;
            }
            return -1;
        }

        ///<summary>Finds the index of the first occurence of an item in the array.</summary>
        ///<param name="source">The array to search.</param>
        ///<param name="item">The item to find.</param>
        ///<returns>The index of the first matching item, or -1 if the item was not found.</returns>
        public static int IndexOf_Array<TSource>(this TSource[] source, TSource item)
        {
            return source.FindIndex_Array(i => EqualityComparer<TSource>.Default.Equals(item, i));
        }


        ///<summary>Performs the specified action on each element of the sequence.</summary>
        ///<param name="action">The System.Action delegate to perform on each element.</param>
        public static void ForEach_IEnumerable<TSource>(this IEnumerable<TSource> items, Action<TSource> action)
        {
            foreach (var item in items)
                action(item);
        }

        ///<summary>Performs the specified action on each element of the sequence that satisfies a condition.</summary>
        ///<param name="action">The System.Action delegate to perform on each element.</param>
        ///<param name="predicate">A function to test each element for a condition.</param>
        public static void ForEachWhere_IEnumerable<TSource>(this IEnumerable<TSource> items, Action<TSource> action, Func<TSource, bool> predicate)
        {
            foreach (TSource element in items)
            {
                if (predicate(element))
                    action(element);
            }
        }

        ///<summary>Finds the index of the first item matching an expression in the enumerable.</summary>
        ///<param name="items">The enumerable to search.</param>
        ///<param name="predicate">The expression to test the items against.</param>
        ///<returns>The index of the first matching item, or -1 if no items match.</returns>
        public static int FindIndex_IEnumerable<TSource>(this IEnumerable<TSource> items, Func<TSource, bool> predicate)
        {
            int retVal = 0;
            foreach (var item in items)
            {
                if (predicate(item)) return retVal;
                retVal++;
            }
            return -1;
        }

        ///<summary>Finds the index of the first occurence of an item in the enumerable.</summary>
        ///<param name="items">The enumerable to search.</param>
        ///<param name="item">The item to find.</param>
        ///<returns>The index of the first matching item, or -1 if the item was not found.</returns>
        public static int IndexOf_IEnumerable<TSource>(this IEnumerable<TSource> items, TSource item)
        {
            return items.FindIndex_IEnumerable(i => EqualityComparer<TSource>.Default.Equals(item, i));
        }

        #endregion


        /// <summary>Interpolates between colors by t.</summary>
        public static Color Lerp(this Color c1, Color c2, float value)
        {
            if (value > 1.0f) return c2;
            else if (value < 0.0f) return c1;
            return new Color(c1.r + (c2.r - c1.r) * value, c1.g + (c2.g - c1.g) * value,
                             c1.b + (c2.b - c1.b) * value, c1.a + (c2.a - c1.a) * value);
        }
        /// <summary>Interpolates between colors by t.</summary>
        public static Color Lerp2(this Color c1, Color c2, float value)
        {
            return new Color(c1.r + (c2.r - c1.r) * value, c1.g + (c2.g - c1.g) * value,
                             c1.b + (c2.b - c1.b) * value, c1.a + (c2.a - c1.a) * value);
        }
    }

    // -----------------------------------------------------------------------------------------------------


    public struct Mathc // Common / Generic
    {
        #region [Mathf]

        //public const float PI =    3.14159274f;
        //public const float PI2 =   6.283185318f;  // (pi * 2)
        //public const float PIO2 =  1.5707963268f; // (pi / 2)
        //public const float PI_F1 = 0.318309886f;  // (1/pi)
        //public const float PI_F2 = 0.63661977237; // (2/pi)

        public const float PI = 3.14159265358979323846264338327950288f;
        public const float PI2 = 6.283185307179586476925286766559f;  // (pi*2)

        public const float PIO2 = 1.57079632679489661923132169163975144f; // (pi/2)
        public const float PIO4 = 0.785398163397448309615660845819875721f; // (pi/4)

        public const float PI_F1 = 0.318309886183790671537767526745028724f; // (1/pi)
        public const float PI_F2 = 0.636619772367581343075535053490057448f; // (2/pi)

        public const float SQRTPI_F2 = 1.41421356237309504880168872420969808f; // 2/sqrt(pi)
        public const float SQRT2 = 1.41421356237309504880168872420969808f; // sqrt(2)
        public const float SQRT1_2 = 0.707106781186547524400844362104849039f; // 1/sqrt(2)


        public const float Infinity = float.PositiveInfinity;
        public const float NegativeInfinity = float.NegativeInfinity;
        public const float Deg2Rad = 0.0174532924f; // (pi/180)
        public const float Rad2Deg = 57.29578f; // (180/pi)
        public const float Epsilon = 1.401298E-45f;


        public static float Abs(float a) { return a < 0 ? -a : a; }
        public static int Abs(int a) { return a < 0 ? -a : a; }

        public static float Min(float a, float b) { return a < b ? a : b; }
        public static int Min(int a, int b) { return a < b ? a : b; }
        public static uint Min(uint a, uint b) { return a < b ? a : b; }

        public static float Max(float a, float b) { return a > b ? a : b; }
        public static int Max(int a, int b) { return a > b ? a : b; }
        public static uint Max(uint a, uint b) { return a > b ? a : b; }

        public static float Min(float a, float b, float c) { return a < b ? a < c ? a : c < b ? c : b : b; }
        public static int Min(int a, int b, int c) { return a < b ? a < c ? a : c < b ? c : b : b; }

        public static float Max(float a, float b, float c) { return a > b ? a > c ? a : c > b ? c : b : b; }
        public static int Max(int a, int b, int c) { return a > b ? a > c ? a : c > b ? c : b : b; }

        public static int Sign(float a) { return a < 0 ? -1 : 1; }
        public static int Sign(int a) { return a < 0 ? -1 : 1; }

        public static int Sign0(float a, float min = float.Epsilon) { return a < -min ? -1 : a > min ? 1 : 0; }
        public static int Sign0(int a, float min = 0) { return a < -min ? -1 : a > min ? 1 : 0; }

        public static float Clamp(float a, float min, float max) { return a > max ? max : a < min ? min : a; }
        public static int Clamp(int a, int min, int max) { return a > max ? max : a < min ? min : a; }

        public static float Clamp01(float a) { return a > 1 ? 1.0f : a < 0 ? 0.0f : a; }
        public static int Clamp01(int a) { return a > 1 ? 1 : a < 0 ? 0 : a; }


        // casts (same as Mathf)
        public static float Sin(float f) { return (float)Math.Sin((double)f); }
        public static float Cos(float f) { return (float)Math.Cos((double)f); }
        public static float Tan(float f) { return (float)Math.Tan((double)f); }
        public static float Asin(float f) { return (float)Math.Asin((double)f); }
        public static float Acos(float f) { return (float)Math.Acos((double)f); }
        public static float Atan(float f) { return (float)Math.Atan((double)f); }
        public static float Atan2(float y, float x) { return (float)Math.Atan2((double)y, (double)x); }
        public static float Sqrt(float f) { return (float)Math.Sqrt((double)f); }
        public static float Pow(float f, float p) { return (float)Math.Pow((double)f, (double)p); }
        public static float Exp(float power) { return (float)Math.Exp((double)power); }
        public static float Log(float f, float p) { return (float)Math.Log((double)f, (double)p); }
        public static float Log(float f) { return (float)Math.Log((double)f); }
        public static float Log10(float f) { return (float)Math.Log10((double)f); }
        public static float Ceil(float f) { return (float)Math.Ceiling((double)f); }
        public static float Floor(float f) { return (float)Math.Floor((double)f); }
        public static float Round(float f) { return (float)Math.Round((double)f); }
        public static int CeilToInt(float f) { return (int)Math.Ceiling((double)f); }
        public static int FloorToInt(float f) { return (int)Math.Floor((double)f); }
        //public static int RoundToInt(float f) { return (int)Math.Round((double)f); }

        public static int RoundToInt(float v)
        {
            return (int)(v + 0.5F);
        }
        public static int RoundToInt(double v)
        {
            return (int)(v + 0.5D);
        }


        /// <summary>
        /// Loops the value t, so that it is never larger than length and never smaller than 0.
        /// </summary>
        /// ( 3.0, 2.5) = 0.5   ( 3.0 - Foor( 1.2) * 2.5)
        /// (-4.0, 2.5) = 1.0   (-4.0 - Foor(-1.6) * 2.5)
        public static float Repeat(float t, float length)
        {
            return t - Mathf.Floor(t / length) * length;
        }

        public static float DeltaAngle(float current, float target)
        {
            float num = Mathf.Repeat(target - current, 360f);
            if ((double)num > 180.0)
                num -= 360f;
            return num;
        }

        // output: (-0.5 to 0.5)
        public static float DeltaWrap(float num)
        {
            return num < -0.5f ? num + 1.0f : (num > 0.5f ? num - 1.0f : num);
        }


        #endregion


        #region [Rounding]

        /// <summary>Round to the nearest integer towards zero.</summary>
        public static float Truncate(float f) { return (float)Math.Truncate(f); }

        /// <summary>Round to the nearest integer towards zero.</summary>
        public static float Round_ToZero(float f) { return (float)Math.Truncate(f); }
        /// <summary>Round to the nearest integer away from zero.</summary>
        public static float Round_FromZero(float f) { return (float)Math.Round(f, MidpointRounding.AwayFromZero); }
        /// <summary>Round to the nearest integer towards zero.</summary>
        public static int RoundToInt_ToZero(float f) { return (int)Math.Truncate(f); }
        /// <summary>Round to the nearest integer away from zero.</summary>
        public static int RoundToInt_FromZero(float f) { return (int)Math.Round(f, MidpointRounding.AwayFromZero); }

        public static float RoundTo(float f, float r = 90.0f) { return (float)Math.Round(f / r) * r; }
        public static float RoundTo(float f, double r = 90.0d) { return (float)(Math.Round(f / r) * r); }

        #endregion [Rounding]


        #region [Misc]


        //public static T NullCoalescing<T>(T a, T b)
        //{
        //    //return a ?? b;"
        //    return ((a == null) ||
        //        (a.Equals(default(T)))) ?
        //        b : a;
        //}


        public static float Pow1h(float x) { return (x + x * x) * 0.5f; }
        public static float Pow2(float x) { return x * x; }
        public static float Pow2h(float x) { return (x * x + x * x * x) * 0.5f; }
        public static float Pow3(float x) { return x * x * x; }
        public static float Pow4(float x) { return x * x * x * x; }
        public static float Pow5(float x) { return x * x * x * x * x; }
        public static float Pow6(float x) { return x * x * x * x * x * x; }
        public static float Pow7(float x) { return x * x * x * x * x * x * x; }
        public static float Pow8(float x) { return x * x * x * x * x * x * x * x; }

        public static float Pow2Inv(float x) { return 1.0f - Pow2(1.0f - x); }
        public static float Pow2Inv(float x, float min, float max)
        {
            float x1 = (x - min) / (max - min); // 0 to 1
            return min + (Pow2Inv(x1) * (max - min));
        }

        public static float PowPow(float x) { return Pow2(1.0f - Pow2(1.0f - x)); }


        // returns the largest absolute value.
        public static float AbsMax(float a, float b) { return Max(Abs(a), Abs(b)); }
        public static float AbsMax(float a, float b, float c) { return AbsMax(AbsMax(a, b), c); }
        public static float AbsMax(float a, float b, float c, float d) { return AbsMax(AbsMax(a, b), AbsMax(c, d)); }

        // returns the value that has the largest absolute value.
        public static float MaxAbs(float a, float b) { return Abs(a) > Abs(b) ? a : b; }
        public static float MaxAbs(float a, float b, float c) { return MaxAbs(MaxAbs(a, b), c); }
        public static float MaxAbs(float a, float b, float c, float d) { return MaxAbs(MaxAbs(a, b), MaxAbs(c, d)); }

        // x = min, y = max
        public static Vector2 MinMax(float a, float b) { return a > b ? new Vector2(b, a) : new Vector2(a, b); }


        public static bool Between(float val, float min, float max) { return val >= min && val <= max; }


        public static bool Approx(float val, float about, float range) { return ((Abs(val - about) <= range)); }
        public static bool Approx(Vector2 val, Vector2 about, float range) { return ((val - about).sqrMagnitude <= range * range); }
        public static bool Approx(Vector3 val, Vector3 about, float range) { return ((val - about).sqrMagnitude <= range * range); }
        public static bool Approx(Color val, Color about, float range, bool alphatest = false)
        {
            return (val.Diff(about) <= range) && (!alphatest || Approx(val.a, about.a, range));
        }


        /// <summary>
        /// Returns bit number <see cref="b"/> from int <see cref="a"/>. The bit number is zero based.
        /// Relevant <see cref="b"/> values are from 0 to 31. (Equals to (<see cref="a"/> >> <see cref="b"/>) & 1)
        /// </summary>
        /// <param name="a">Integer.</param>
        /// <param name="b">A bit number between 0 and 31.</param>
        public static int Bit(int a, int b)
        {
            return (a >> b) & 1;
            //return (a & (1 << b)) >> b; //Original code, one extra shift operation required
        }


        /// <summary>
        /// Returns a nice color from int <see cref="i"/> with alpha <see cref="a"/>. (64 possible colors)
        /// </summary>
        /// <param name="i">Integer.</param>
        /// <param name="a">Alpha.</param>
        public static Color IntToColor(int i, float a)
        {
            int r = Bit(i, 1) + Bit(i, 3) * 2 + 1;
            int g = Bit(i, 2) + Bit(i, 4) * 2 + 1;
            int b = Bit(i, 0) + Bit(i, 5) * 2 + 1;
            return new Color(r * 0.25F, g * 0.25F, b * 0.25F, a);
        }


        #endregion



        #region [Vectors]


        public static float DeadZone(float a, float dz) { return Abs(a) >= Abs(dz) ? a : 0; }
        public static Vector2 DeadZone(Vector2 a, float dz) { return a.sqrMagnitude < dz * dz ? Vector2.zero : a; }
        public static Vector3 DeadZone(Vector3 a, float dz) { return a.sqrMagnitude < dz * dz ? Vector3.zero : a; }


        public static float sqrDiff(float a, float b)
        {
            return Pow2(a - b);
        }
        public static float sqrLength(float vx, float vy, float vz)
        {
            return vx * vx + vy * vy + vz * vz;
        }
        public static float sqrLength(float vx, float vy)
        {
            return vx * vx + vy * vy;
        }


        private const float _diag = 1.4f; // Mathc.SQRT2;

        // absolute intput values!
        public static float Octile(float dx, float dy)
        {
            return dx > dy
                   ? (_diag * dy + (dx - dy))
                   : (_diag * dx + (dy - dx));
        }

        // absolute intput values!
        public static int ApproxDistInt(int dx, int dy)
        {
            if (dx > dy)
            {
                // swap; need dx <= dy
                dx ^= dy; dy ^= dx; dx ^= dy;
            }
            return (((dy << 8) + (dy << 3) - (dy << 4) - (dy << 1) +
                     (dx << 7) - (dx << 5) + (dx << 3) - (dx << 1)) >> 8);
        }


        #endregion



        #region [Array/IEnumerable]

        /// <summary>Assigns each element in an array to a specified <see cref="value"/>.</summary>
        /// <typeparam name="T">The type of the elements of the source array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array"/>.</param>
        /// <param name="value">An value that will be assigned to each element in the array</param>
        public static void InitArray<T>(T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }
        public static void InitArray<T>(T[] array, Func<int, T> factory)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = factory(i);
        }


        /// <summary>Returns a new array <see cref="T"/> of specified <see cref="length"/>,
        /// and all elements set to the specified <see cref="value"/>.</summary>
        /// <typeparam name="T">Type of the one-dimensional, zero-based <see cref="T:System.Array"/>.</typeparam>
        /// <param name="length">the number of elements you want the array to have</param>
        /// <param name="value">a value that will be assigned to each element in the array</param>
        public static T[] NewArray<T>(int length, T value)
        {
            var arr = new T[length];
            for (int i = 0; i < length; i++)
                arr[i] = value;
            return arr;
        }
        public static T[] NewArray<T>(int length, Func<int, T> factory)
        {
            var arr = new T[length];
            for (int i = 0; i < length; i++)
                arr[i] = factory(i);
            return arr;
        }



        /// <summary>Copies elements from an array into a new one, and returns the new array.</summary>
        public static T[] CopyArray<T>(T[] source) { return CopyArray(source, source.Length); }
        public static T[] CopyArray<T>(T[] source, int length)
        {
            T[] dest = new T[length];
            Array.Copy(source, 0, dest, 0, length);
            return dest;
        }


        /// <summary>Creates an array, and tries to cast elements from another array into it.</summary>
        public static TResult[] CopyArray<TSource, TResult>(TSource[] source)
        {
            int len = source.Length;
            TResult[] dest = new TResult[len];
            Array.Copy(source, 0, dest, 0, len);
            return dest;
        }


        /// <summary>Concatenates two sequences. (returns <see langword="null"/> if either one of the sequences equal null)</summary>
        /// <param name="a">The first sequence.</param><param name="b">The sequence to concatenate to the first sequence.</param>
        public static T[] ConcatArray<T>(T[] a, T[] b)
        {
            return (a != null && b != null) ? a.Concat<T>(b).ToArray() : null;
        }
        /// <summary>Returns two sequences concatenated if both valid (not <see langword="null"/>), or only the valid one if any.
        /// (<see langword="null"/> is returned if both invalid)</summary>)
        /// <param name="a">The first sequence.</param><param name="b">The sequence to concatenate to the first sequence.</param>
        public static T[] ConcatTry<T>(T[] a, T[] b)
        {
            if (a == null || b == null) return a ?? b;
            return a.Concat<T>(b).ToArray();
        }


        /// <summary>Converts an array of one type to an array of another type.
        /// (Ex: ConvertArray(source, delegate(TSource elem) => (TResult)elem); )</summary>
        //public static TResult[] ConvertArray<TSource, TResult>(TSource[] source, Converter<TSource, TResult> converter)
        //{
        //    //return Array.ConvertAll<TSource, TResult>(new TSource[length], input => value);
        //    return Array.ConvertAll(source, converter);
        //}



        #endregion [Array/IEnumerable]


    }


    #endregion [Math-Common]



    // --------------------------------



    #region [Lerping]


    public static class Interpolate
    {
        // Mathfx / MathS
        // http://wiki.unity3d.com/index.php?title=Mathfx
        // http://wiki.unity3d.com/index.php?title=SpeedLerp


        /// <summary>Interpolates between a and b by t. t is clamped between 0 and 1.</summary>
        public static float Linear(float from, float to, float value)
        {
            if (value < 0.0f) return from;
            else if (value > 1.0f) return to;
            return (to - from) * value + from;
        }
        /// <summary>Interpolates between a and b by t. (unclamped)</summary>
        public static float Linearf(float from, float to, float value)
        {
            return (1.0f - value) * from + value * to;
        }


        // Mathf.Lerp (from, to, Mathf.InverseLerp (from2, to2, value))
        public static float Linear(float from, float to, float from2, float to2, float value)
        {
            if (from2 < to2)
            {
                if (value < from2)
                    value = from2;
                else if (value > to2)
                    value = to2;
            }
            else
            {
                if (value < to2)
                    value = to2;
                else if (value > from2)
                    value = from2;
            }
            return (to - from) * ((value - from2) / (to2 - from2)) + from;
        }
        // Mathf.Lerp (from, to, Mathf.InverseLerp (from2, to2, value))
        public static float Linearf(float from, float to, float from2, float to2, float value)
        {
            return (to - from) * ((value - from2) / (to2 - from2)) + from;
        }


        /// <summary>Interpolate while easing in and out at the limits.</summary>
        public static float Hermite(float start, float end, float value)
        {
            return Linear(start, end, value * value * (3.0f - 2.0f * value));
        }
        /// <summary>Interpolate while easing out around the end, when value is near one.</summary>
        public static float Sinerp(float start, float end, float value)
        {
            return Linear(start, end, Mathc.Sin(value * Mathc.PI * 0.5f));
        }
        /// <summary>Interpolate while easing in around the start, when value is near zero.</summary>
        public static float Coserp(float start, float end, float value)
        {
            return Linear(start, end, 1.0f - Mathc.Cos(value * Mathc.PI * 0.5f));
        }


        /// <summary>Interpolate while easing in and out at the limits.</summary>
        public static float Hermite01(float value)
        {
            return value * value * (3.0f - 2.0f * value);
        }
        /// <summary>Interpolate while easing out around the end, when value is near one.</summary>
        public static float Sinerp01(float value)
        {
            return Mathc.Sin(value * Mathc.PI * 0.5f);
        }
        /// <summary>Interpolate while easing in around the start, when value is near zero.</summary>
        public static float Coserp01(float value)
        {
            return 1.0f - Mathc.Cos(value * Mathc.PI * 0.5f);
        }


        /// <summary>Short for 'boing-like interpolation', this method will first overshoot, then waver back and forth around the end value before coming to a rest.</summary>
        public static float Berp(float start, float end, float value)
        {
            value = Mathc.Clamp01(value);
            value = (Mathc.Sin(value * Mathc.PI * (0.2f + 2.5f * value * value * value)) * Mathc.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
            return start + (end - start) * value;
        }
        /// <summary>Makes x bounce 6 times between 0 and 1, were each bounce lower as x is increased.</summary>
        public static float Bounce(float x)
        {
            return Mathc.Abs(Mathc.Sin(6.28f * (x + 1f) * (x + 1f)) * (1f - x));
        }


        /// <summary>Same as Lerp but makes sure the values interpolate correctly when they wrap around 360 degrees.</summary>
        public static float Angle(float start, float end, float value)
        {
            float num = (end - start) - Mathc.Floor((end - start) / 360f) * 360f;
            if (num > 180f) { num -= 360f; }
            return start + num * value;
        }
        /// <summary>Linearly interpolates between two eulerangle vectors.</summary>
        public static Vector3 Euler(Vector3 from, Vector3 to, float t)
        {
            return new Vector3(Angle(from.x, to.x, t), Angle(from.y, to.y, t), Angle(from.z, to.z, t));
        }
        /// <summary>Linearly interpolates between two eulerangle vectors.</summary>
        public static Vector3 Euler(Vector3 from, Vector3 to, Vector3 t)
        {
            return new Vector3(Angle(from.x, to.x, t.x), Angle(from.y, to.y, t.y), Angle(from.z, to.z, t.z));
        }



        /// <summary>Interpolates between colors by t.</summary>
        public static Color Color(Color c1, Color c2, float value)
        {
            if (value > 1.0f) return c2;
            else if (value < 0.0f) return c1;
            return new Color(c1.r + (c2.r - c1.r) * value, c1.g + (c2.g - c1.g) * value,
                             c1.b + (c2.b - c1.b) * value, c1.a + (c2.a - c1.a) * value);
        }
        /// <summary>Interpolates between colors by t.</summary>
        public static Color Colorf(Color c1, Color c2, float value)
        {
            return new Color(c1.r + (c2.r - c1.r) * value, c1.g + (c2.g - c1.g) * value,
                             c1.b + (c2.b - c1.b) * value, c1.a + (c2.a - c1.a) * value);
        }
        /// <summary>Interpolates between colors by t.</summary>
        public static Color Color(Color c1, Color c2, Color c3, float value)
        {
            if (value > 1.0f) return c3; else if (value < 0.0f) return c1;
            return value < 0.5f ? Colorf(c1, c2, value * 2) : Colorf(c2, c3, (value - 0.5f) * 2);
        }


        /// <summary>Linearly interpolates between two vectors.</summary>
        public static Vector2 Vector2(Vector2 v1, Vector2 v2, float value)
        {
            if (value > 1.0f) return v2;
            else if (value < 0.0f) return v1;
            return new Vector2(v1.x + (v2.x - v1.x) * value, v1.y + (v2.y - v1.y) * value);
        }
        /// <summary>Linearly interpolates between two vectors.(unclamped)</summary>
        public static Vector2 Vector2f(Vector2 v1, Vector2 v2, float value)
        {
            return new Vector2(v1.x + (v2.x - v1.x) * value, v1.y + (v2.y - v1.y) * value);
        }


        /// <summary>Linearly interpolates between two vectors.</summary>
        public static Vector3 Vector3(Vector3 v1, Vector3 v2, float value)
        {
            if (value > 1.0f) return v2;
            else if (value < 0.0f) return v1;
            return new Vector3(v1.x + (v2.x - v1.x) * value, v1.y + (v2.y - v1.y) * value, v1.z + (v2.z - v1.z) * value);
        }
        /// <summary>Linearly interpolates between two vectors. (unclamped)</summary>
        public static Vector3 Vector3f(Vector3 v1, Vector3 v2, float value)
        {
            return new Vector3(v1.x + (v2.x - v1.x) * value, v1.y + (v2.y - v1.y) * value, v1.z + (v2.z - v1.z) * value);
        }


        /// <summary>Linearly interpolates between two vectors.</summary>
        public static Vector4 Vector4(Vector4 v1, Vector4 v2, float value)
        {
            if (value > 1.0f) return v2;
            else if (value < 0.0f) return v1;
            return new Vector4(v1.x + (v2.x - v1.x) * value, v1.y + (v2.y - v1.y) * value,
                               v1.z + (v2.z - v1.z) * value, v1.w + (v2.w - v1.w) * value);
        }



        public static Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            t = Mathc.Clamp01(t);
            float t2 = 1 - t;
            return Mathc.Pow3(t2) * p0 + 3 * Mathc.Pow2(t2) * t * p1 + 3 * t2 * Mathc.Pow2(t) * p2 + Mathc.Pow3(t) * p3;
        }



        public static float Smooth01(float x)
        {
            return Linear(x * x, 1f - Mathc.Pow2(1f - x), x);
        }

        // http://www.flong.com/texts/code/shapers_exp/
        public static float LogisticSigmoid(float x, float a = 0.787f)
        {
            // n.b.: this Logistic Sigmoid has been normalized.

            float epsilon = 0.0001f;
            float min_param_a = 0.0f + epsilon;
            float max_param_a = 1.0f - epsilon;
            a = Mathc.Max(min_param_a, Mathc.Min(max_param_a, a));
            a = (1.0f / (1.0f - a) - 1.0f);

            float A = 1.0f / (1.0f + Mathc.Exp(0.0f - ((x - 0.5f) * a * 2.0f)));
            float B = 1.0f / (1.0f + Mathc.Exp(a));
            float C = 1.0f / (1.0f + Mathc.Exp(0.0f - a));
            float y = (A - B) / (C - B);
            return y;
        }



        /// <summary> [.-''']  input and output in 0-1 range.</summary>
        public static float Shape_08FFF(float f)
        {
            return f < 0.5f ? (f - f * f) * 4 : 1;
        }

        /// <summary> [.-'-.]  input and output in 0-1 range.</summary>
        public static float Shape_08F80(float f)
        {
            return (f - f * f) * 4;
        }

        /// <summary> [.'''.]  input and output in 0-1 range.</summary>
        public static float Shape_0EFE0(float f)
        {
            return 1 - Mathc.Pow4(f * 2 - 1);
        }

        /// <summary> [.---']  input and output in 0-1 range.</summary>
        public static float Shape_0789F(float f)
        {
            return Mathc.Pow3(f * 1.5874f - 0.7937f) + .5f;
        }


        /// <summary> [.''-.]  input and output in 0-1 range.</summary>
        public static float Shape_0EF80(float f)
        {
            return 1 - Mathc.Pow4((1 - f) * (f * 3 - 1) + f * f);
        }



    }

    #endregion [Lerping]



    // --------------------------------



    #region [Rect Extensions]


    public enum RectPoint
    {
        TopLeft = 0, TopRight = 1, BottomRight = 2, BottomLeft = 3
    }

    // Rect Extensions
    public static class RectEx
    {
        public static Rect Offset(this Rect rect, Vector2 offs)
        {
            return new Rect(rect.x + offs.x, rect.y + offs.y, rect.width, rect.height);
        }
        public static Rect Offset(this Rect rect, float offx, float offy)
        {
            return new Rect(rect.x + offx, rect.y + offy, rect.width, rect.height);
        }
        public static Rect OffsetX(this Rect rect, float x)
        {
            return new Rect(rect.x + x, rect.y, rect.width, rect.height);
        }
        public static Rect OffsetY(this Rect rect, float y)
        {
            return new Rect(rect.x, rect.y + y, rect.width, rect.height);
        }

        public static Rect Add(this Rect rect, Vector2 offs)
        {
            return new Rect(rect.x + offs.x, rect.y + offs.y, rect.width, rect.height);
        }
        public static Rect Add(this Rect rect, float offx, float offy)
        {
            return new Rect(rect.x + offx, rect.y + offy, rect.width, rect.height);
        }
        public static Rect AddX(this Rect rect, float x)
        {
            return new Rect(rect.x + x, rect.y, rect.width, rect.height);
        }
        public static Rect AddY(this Rect rect, float y)
        {
            return new Rect(rect.x, rect.y + y, rect.width, rect.height);
        }

        public static Rect MoveX(this Rect rect, float x)
        {
            return new Rect(rect.x + x, rect.y, rect.width - x, rect.height);
        }
        public static Rect MoveY(this Rect rect, float y)
        {
            return new Rect(rect.x, rect.y + y, rect.width, rect.height - y);
        }

        public static Rect Scale(this Rect rect, float s)
        {
            return new Rect(rect.x, rect.y, rect.width * s, rect.height * s);
        }
        public static Rect Scale(this Rect rect, Vector2 s)
        {
            return new Rect(rect.x, rect.y, rect.width * s.x, rect.height * s.y);
        }
        public static Rect Scale(this Rect rect, float sx, float sy)
        {
            return new Rect(rect.x, rect.y, rect.width * sx, rect.height * sy);
        }
        public static Rect ScaleW(this Rect rect, float ws)
        {
            return new Rect(rect.x, rect.y, rect.width * ws, rect.height);
        }
        public static Rect ScaleH(this Rect rect, float hs)
        {
            return new Rect(rect.x, rect.y, rect.width, rect.height * hs);
        }

        public static Vector2 GetRectPnt(this Vector2[] points, RectPoint point)
        {
            return points[(int)point];
        }

        public static void SetRectPnt(this Vector2[] points, RectPoint point, Vector2 pos)
        {
            points[(int)point] = pos;
        }

        public static Vector2[] GetPoints(this Rect rect)
        {
            Vector2[] points = new Vector2[4];
            points[(int)RectPoint.TopLeft] = new Vector2(rect.xMin, rect.yMin);
            points[(int)RectPoint.TopRight] = new Vector2(rect.xMax, rect.yMin);
            points[(int)RectPoint.BottomRight] = new Vector2(rect.xMax, rect.yMax);
            points[(int)RectPoint.BottomLeft] = new Vector2(rect.xMin, rect.yMax);
            return points;
        }

        public static Vector2 GetPoint(this Rect rect, RectPoint point)
        {
            switch (point)
            {
                case RectPoint.TopLeft: return new Vector2(rect.xMin, rect.yMin);
                case RectPoint.TopRight: return new Vector2(rect.xMax, rect.yMin);
                case RectPoint.BottomRight: return new Vector2(rect.xMax, rect.yMax);
                case RectPoint.BottomLeft: return new Vector2(rect.xMin, rect.yMax);
            }
            return new Vector2(0, 0);
        }

        public static void SetPoint(this Rect rect, RectPoint point, Vector2 pos)
        {
            switch (point)
            {
                case RectPoint.TopLeft: rect.xMin = pos.x; rect.yMin = pos.y; break;
                case RectPoint.TopRight: rect.xMax = pos.x; rect.yMin = pos.y; break;
                case RectPoint.BottomRight: rect.xMax = pos.x; rect.yMax = pos.y; break;
                case RectPoint.BottomLeft: rect.xMin = pos.x; rect.yMax = pos.y; break;
            }
        }

        public static Rect Expand(this Rect rect, float dist)
        {
            return Rect.MinMaxRect(rect.xMin - dist, rect.yMin - dist, rect.xMax + dist, rect.yMax + dist);
        }
        public static Rect Shrink(this Rect rect, float dist)
        {
            return Rect.MinMaxRect(rect.xMin + dist, rect.yMin + dist, rect.xMax - dist, rect.yMax - dist);
        }

        public static Rect Expand(this Rect rect, Vector2 dist)
        {
            return Rect.MinMaxRect(rect.xMin - dist.x, rect.yMin - dist.y, rect.xMax + dist.x, rect.yMax + dist.y);
        }
        public static Rect Shrink(this Rect rect, Vector2 dist)
        {
            return Rect.MinMaxRect(rect.xMin + dist.x, rect.yMin + dist.y, rect.xMax - dist.x, rect.yMax - dist.y);
        }

        public static void FixMinsMaxs(this Rect rect)
        {
            if (rect.xMax < rect.xMin)
            {
                float max = rect.xMin; rect.xMin = rect.xMax; rect.xMax = max;
            }
            if (rect.yMax < rect.yMin)
            {
                float max = rect.yMin; rect.yMin = rect.yMax; rect.yMax = max;
            }
        }

        public static Vector2 TopLeft(this Rect rect)
        {
            return new Vector2(rect.xMin, rect.yMin);
        }
        public static Vector2 TopRight(this Rect rect)
        {
            return new Vector2(rect.xMax, rect.yMin);
        }
        public static Vector2 BottomRight(this Rect rect)
        {
            return new Vector2(rect.xMax, rect.yMax);
        }
        public static Vector2 BottomLeft(this Rect rect)
        {
            return new Vector2(rect.xMin, rect.yMax);
        }


        public static Rect GetRect(this Texture2D tex)
        {
            return new Rect(0, 0, tex.width, tex.height);
        }
        public static Rect GetRect(this Texture2D tex, Vector2 pos)
        {
            return new Rect(pos.x, pos.y, tex.width, tex.height);
        }
        public static Rect GetRect(this Texture2D tex, float x, float y)
        {
            return new Rect(x, y, tex.width, tex.height);
        }

        public static Vector2 GetPoint(this Rect rect, SpriteAlignment point)
        {
            switch (point)
            {
                case SpriteAlignment.TopLeft: return new Vector2(rect.xMin, rect.yMin);
                case SpriteAlignment.TopRight: return new Vector2(rect.xMax, rect.yMin);
                case SpriteAlignment.BottomRight: return new Vector2(rect.xMax, rect.yMax);
                case SpriteAlignment.BottomLeft: return new Vector2(rect.xMin, rect.yMax);
                case SpriteAlignment.TopCenter: return new Vector2((rect.xMin + rect.xMax) * 0.5f, rect.yMin);
                case SpriteAlignment.LeftCenter: return new Vector2(rect.xMin, (rect.yMin + rect.yMax) * 0.5f);
                case SpriteAlignment.Center: return rect.center;
                case SpriteAlignment.RightCenter: return new Vector2(rect.xMax, (rect.yMin + rect.yMax) * 0.5f);
                case SpriteAlignment.BottomCenter: return new Vector2((rect.xMin + rect.xMax) * 0.5f, rect.yMax);
            }
            return new Vector2(rect.x, rect.y);
        }

        public static Rect Aligned(this Rect rect, SpriteAlignment point)
        {
            switch (point)
            {
                case SpriteAlignment.TopLeft: return rect;
                case SpriteAlignment.TopRight: return rect.OffsetX(-rect.width);
                case SpriteAlignment.BottomRight: return rect.Offset(-rect.width, -rect.height);
                case SpriteAlignment.BottomLeft: return rect.OffsetY(-rect.height);
                case SpriteAlignment.TopCenter: return rect.OffsetX(-rect.width * 0.5f);
                case SpriteAlignment.LeftCenter: return rect.OffsetY(-rect.height * 0.5f);
                case SpriteAlignment.Center: return rect.Offset(-rect.width * 0.5f, -rect.height * 0.5f);
                case SpriteAlignment.RightCenter: return rect.Offset(-rect.width, -rect.height * 0.5f);
                case SpriteAlignment.BottomCenter: return rect.Offset(-rect.width * 0.5f, -rect.height);
            }
            return rect;
        }



        public static Rect CenterSize(float x, float y, float w, float h)
        {
            return new Rect(x - w * 0.5f, y - h * 0.5f, w, h);
        }
    }


    #endregion



    // --------------------------------



    #region [Color Extensions]


    /// <summary>HSBColor: Hue/Saturation/Brightness/Alpha color model.</summary>
    /// http://wiki.unity3d.com/index.php?title=HSBColor
    [System.Serializable]
    public struct HSBColor
    {
        public float h;
        public float s;
        public float b;
        public float a;

        public HSBColor(float h, float s, float b, float a)
        {
            this.h = h;
            this.s = s;
            this.b = b;
            this.a = a;
        }

        public HSBColor(float h, float s, float b)
        {
            this.h = h;
            this.s = s;
            this.b = b;
            this.a = 1f;
        }

        public HSBColor(Color col)
        {
            HSBColor temp = FromColor(col);
            h = temp.h;
            s = temp.s;
            b = temp.b;
            a = temp.a;
        }

        public override string ToString()
        {
            return "H:" + h + " S:" + s + " B:" + b;
        }

        public Color ToColor()
        {
            return ToColor(this);
        }


        public static HSBColor FromColor(Color color)
        {
            HSBColor ret = new HSBColor(0f, 0f, 0f, color.a);

            float r = color.r;
            float g = color.g;
            float b = color.b;

            float max = Mathc.Max(r, Mathc.Max(g, b));

            if (max <= 0)
            {
                return ret;
            }

            float min = Mathc.Min(r, Mathc.Min(g, b));
            float dif = max - min;

            if (max > min)
            {
                if (g == max)
                {
                    ret.h = (b - r) / dif * 60f + 120f;
                }
                else if (b == max)
                {
                    ret.h = (r - g) / dif * 60f + 240f;
                }
                else if (b > g)
                {
                    ret.h = (g - b) / dif * 60f + 360f;
                }
                else
                {
                    ret.h = (g - b) / dif * 60f;
                }
                if (ret.h < 0)
                {
                    ret.h = ret.h + 360f;
                }
            }
            else
            {
                ret.h = 0;
            }

            ret.h *= 1f / 360f;
            ret.s = (dif / max) * 1f;
            ret.b = max;

            return ret;
        }

        public static Color ToColor(HSBColor hsbColor)
        {
            float r = hsbColor.b;
            float g = hsbColor.b;
            float b = hsbColor.b;

            if (hsbColor.s != 0)
            {
                float max = hsbColor.b;
                float dif = hsbColor.b * hsbColor.s;
                float min = hsbColor.b - dif;

                float h = hsbColor.h * 360f;

                if (h < 60f)
                {
                    r = max;
                    g = h * dif / 60f + min;
                    b = min;
                }
                else if (h < 120f)
                {
                    r = -(h - 120f) * dif / 60f + min;
                    g = max;
                    b = min;
                }
                else if (h < 180f)
                {
                    r = min;
                    g = max;
                    b = (h - 120f) * dif / 60f + min;
                }
                else if (h < 240f)
                {
                    r = min;
                    g = -(h - 240f) * dif / 60f + min;
                    b = max;
                }
                else if (h < 300f)
                {
                    r = (h - 240f) * dif / 60f + min;
                    g = min;
                    b = max;
                }
                else if (h <= 360f)
                {
                    r = max;
                    g = min;
                    b = -(h - 360f) * dif / 60 + min;
                }
                else
                {
                    r = 0;
                    g = 0;
                    b = 0;
                }
            }
            return new Color(Mathc.Clamp01(r), Mathc.Clamp01(g), Mathc.Clamp01(b), hsbColor.a);
        }


        public static HSBColor Lerp(HSBColor a, HSBColor b, float t)
        {
            float h, s;

            //check special case black (color.b==0): interpolate neither hue nor saturation!
            //check special case grey (color.s==0): don't interpolate hue!
            if (a.b == 0)
            {
                h = b.h;
                s = b.s;
            }
            else if (b.b == 0)
            {
                h = a.h;
                s = a.s;
            }
            else
            {
                if (a.s == 0)
                {
                    h = b.h;
                }
                else if (b.s == 0)
                {
                    h = a.h;
                }
                else
                {
                    // works around bug with LerpAngle
                    float angle = Interpolate.Angle(a.h * 360f, b.h * 360f, t);
                    while (angle < 0f)
                        angle += 360f;
                    while (angle > 360f)
                        angle -= 360f;
                    h = angle / 360f;
                }
                s = Interpolate.Linear(a.s, b.s, t);
            }
            return new HSBColor(h, s, Interpolate.Linear(a.b, b.b, t), Interpolate.Linear(a.a, b.a, t));
        }

        public static Color Slerp(Color a, Color b, float t)
        {
            return (HSBColor.Lerp(HSBColor.FromColor(a), HSBColor.FromColor(b), t)).ToColor();
        }
    }


    public static class ColorEx
    {
        public static Color Hue(float h01, float s = 1, float b = 1)
        {
            return HSBColor.ToColor(new HSBColor(h01, s, b));
        }
        public static Color Rainbow(int i, int cnt, float s = 1, float b = 1)
        {
            return ColorEx.Hue(i * (cnt == 0 ? 1f : (1f / cnt)), s, b);
        }
        public static Color PseudoRandom(/*int seed,*/ float s = 1, float b = 1)
        {
            //Random.seed = seed;
            return ColorEx.Hue(Random.value, s, b);
        }

        public static Color Gray(float b, float a = 1)
        {
            return new Color(b, b, b, a);
        }
        public static Color Gray(Color color, float a = 1)
        {
            return Gray(color.grayscale, a);
        }


        // ---- ColorExtensions ---------------------------------------------------


        public static float GetHue(this Color c)
        {
            HSBColor temp = HSBColor.FromColor(c);
            return temp.h;
        }

        public static Color HueShift(this Color c, float hueto1)
        {
            HSBColor temp = HSBColor.FromColor(c);
            temp.h = (temp.h + hueto1);
            if (temp.h > 1.0f) temp.h -= 1.0f;
            if (temp.h < 0.0f) temp.h += 1.0f;
            return HSBColor.ToColor(temp);
        }


        // Extended Colorx;  Author: Josh Koleszar
        #region [Colorx]

        public static HSBColor ToHSBColor(this Color col)
        {
            return HSBColor.FromColor(col);
        }

        public static Color HueSet(this Color c, float hue0to1)
        {
            HSBColor temp = HSBColor.FromColor(c);
            temp.h = hue0to1;
            return HSBColor.ToColor(temp);
        }

        public static Color Saturation(this Color c, float saturation0to1)
        {
            HSBColor temp = HSBColor.FromColor(c);
            temp.s = saturation0to1;
            return HSBColor.ToColor(temp);
        }

        public static Color Brightness(this Color c, float brightness0to1)
        {
            HSBColor temp = HSBColor.FromColor(c);
            temp.b = brightness0to1;
            return HSBColor.ToColor(temp);
        }

        #endregion


        public static Color Alpha(this Color c, float alpha0to1)
        {
            c.a = alpha0to1;
            return c;
        }
        public static Color AlphaMultiply(this Color c, float alpha0to1)
        {
            c.a = c.a * alpha0to1;
            return c;
        }

        public static Color Invert(this Color c)
        {
            Color32 cc = c;
            return (new Color32((byte)~cc.r, (byte)~cc.g, (byte)~cc.b, cc.a));
        }
        public static Color Invert(this Color c, float a)
        {
            Color32 cc = new Color(c.r, c.g, c.b, a);
            return (new Color32((byte)~cc.r, (byte)~cc.g, (byte)~cc.b, cc.a));
        }
        public static Color InvertHue(this Color c)
        {
            HSBColor temp = HSBColor.FromColor(c);
            temp.h += temp.h < 0.5f ? 0.5f : -0.5f;
            return HSBColor.ToColor(temp);
        }

        public static Color SB(this Color c, float saturation0to1, float brightness0to1)
        {
            HSBColor temp = HSBColor.FromColor(c);
            temp.s = saturation0to1;
            temp.b = brightness0to1;
            return HSBColor.ToColor(temp);
        }

        public static Color SBA(this Color c, float saturation0to1, float brightness0to1, float alpha)
        {
            HSBColor temp = HSBColor.FromColor(c);
            temp.s = saturation0to1;
            temp.b = brightness0to1;
            return HSBColor.ToColor(temp).Alpha(alpha);
        }


        // keeps gray
        public static Color ColorSaturate(this Color c, float saturation0to1)
        {
            HSBColor temp = HSBColor.FromColor(c);
            temp.s = saturation0to1 * Interpolate.Shape_08FFF(temp.s);
            return HSBColor.ToColor(temp);
        }

        // keeps gray
        public static Color ColorSB(this Color c, float saturation0to1, float brightness0to1)
        {
            HSBColor temp = HSBColor.FromColor(c);
            temp.s = saturation0to1 * Interpolate.Shape_08FFF(temp.s);
            temp.b = brightness0to1 * Interpolate.Shape_08FFF(temp.s);
            return HSBColor.ToColor(temp);
        }

        public static Color MultiplySB(this Color c, float saturation, float brightness)
        {
            HSBColor temp = HSBColor.FromColor(c);
            temp.s = Mathc.Clamp01(temp.s * saturation);
            temp.b = Mathc.Clamp01(temp.s * brightness);
            return HSBColor.ToColor(temp);
        }


        public static Color ModHSB(this Color c, RefAction<HSBColor> hsb)
        {
            HSBColor temp = HSBColor.FromColor(c);
            hsb(ref temp);
            return HSBColor.ToColor(temp);
        }
        public static Color ModHSB(this Color c, Func<HSBColor, HSBColor> hsb)
        {
            HSBColor temp = hsb(HSBColor.FromColor(c));
            return HSBColor.ToColor(temp);
        }


        public static float Sum(this Color c)
        {
            return (c.r + c.g + c.b);
        }


        public static float Min(this Color c)
        {
            return Mathc.Min(c.r, c.g, c.b);
        }
        public static float Max(this Color c)
        {
            return Mathc.Max(c.r, c.g, c.b);
        }
        public static float MinMaxDiff(this Color c)
        {
            return c.Max() - c.Min();
        }

        // Same: 0, Opposite: 3
        public static float Diff(this Color c, Color o)
        {
            return Mathc.Abs(c.r - o.r) + Mathc.Abs(c.g - o.g) + Mathc.Abs(c.b - o.b);
        }
        // Same: 0, Opposite: 3
        public static float Diff2(this Color c, Color o)
        {
            HSBColor a = HSBColor.FromColor(c), b = HSBColor.FromColor(o);
            return Mathc.Abs(Mathc.DeltaWrap(b.h - a.h) * 2) + Mathc.Abs(b.s - a.s) + Mathc.Abs(b.b - a.b);
        }


        public static Vector3 ToVector3(this Color c)
        {
            return new Vector3(c.r, c.g, c.b);
        }

    }


    #endregion [Color Extensions]



    // --------------------------------



    public static class UnityExtensions
    {

        #region [Transform]


        public static void Reset(this Transform tr)
        {
            tr.rotation = Quaternion.identity;
            tr.position = Vector3.zero;
        }

        public static void ResetLocal(this Transform tr)
        {
            tr.localRotation = Quaternion.identity;
            tr.localPosition = Vector3.zero;
        }

        public static void FullReset(this Transform tr)
        {
            tr.rotation = Quaternion.identity;
            tr.position = Vector3.zero;
            //tr.localScale = WorldToLocal(tr.parent, Vector3.one);
            tr.localScale = tr.InverseTransformPoint(tr.localScale);
        }

        public static void FullResetLocal(this Transform tr)
        {
            tr.localRotation = Quaternion.identity;
            tr.localPosition = Vector3.zero;
            tr.localScale = Vector3.one;
        }


        /// <summary>Transforms a vector from world space to local space,
        /// including being affected by scale.</summary>
        public static Vector3 WorldToLocal(this Transform tr, Vector3 pos)
        {
            return tr ? tr.InverseTransformPoint(pos) : pos;
        }

        /// <summary>Transforms a vector from world space to local space,
        /// including being affected by scale.</summary>
        public static Vector3 LocalToWorld(this Transform tr, Vector3 pos)
        {
            return tr ? tr.TransformPoint(pos) : pos;
        }

        /// <summary>Transforms a direction from world space to local space,
        /// Unaffected by scale. (returned vector has the same length)</summary>
        public static Vector3 WorldToLocal_Dir(this Transform tr, Vector3 dir)
        {
            return tr ? tr.InverseTransformDirection(dir) : dir;
        }

        /// <summary>Transforms a direction from local space to world space,
        /// Unaffected by scale. (returned vector has the same length)</summary>
        public static Vector3 LocalToWorld_Dir(this Transform tr, Vector3 dir)
        {
            return tr ? tr.TransformDirection(dir) : dir;
        }




        public static IEnumerable<Transform> ChildEnumerator(this Transform tr) { return tr.Cast<Transform>(); }

        public static List<Transform> ChildList(this Transform tr) { return tr.Cast<Transform>().ToList(); }

        public static void EnumerateChildren(this Transform tr, Action<Transform> action)
        {
            IEnumerator enumerator = tr.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var value = (Transform)enumerator.Current;
                action(value);
            }
        }
        public static void EnumerateChildren(this Transform tr, Action<Transform, int> action)
        {
            int index = -1;
            foreach (Transform child in tr)
            {
                checked { ++index; }
                action(child, index);
            }
        }
        public static Transform EnumerateChild(this Transform tr, int index)
        {
            foreach (Transform child in tr)
            {
                if (--index <= 0)
                    return child;
            }
            throw new InvalidOperationException("Invalid index!");
        }
        public static Transform EnumerateChildSafe(this Transform tr, ref int index)
        {
            foreach (Transform child in tr)
            {
                if (--index <= 0)
                    return child;
            }
            return default(Transform);
        }



        public const int RECURSIVE_DEPTH = 32;


        public static void InvokeRecursive(this Transform tr, int depth, Action<Transform> action)
        {
            action(tr);
            if (--depth < 0) return;
            foreach (Transform child in tr)
                InvokeRecursive(child, depth, action);
        }
        public static void InvokeRecursive(this Transform tr, Action<Transform> action)
        {
            InvokeRecursive(tr, RECURSIVE_DEPTH, action);
        }


        public static void IterateChildren(this Transform tr, int depth, Action<Transform> action)
        {
            if (--depth < 0) return;
            foreach (Transform child in tr)
                InvokeRecursive(child, depth, action);
        }
        public static void IterateChildren(this Transform tr, bool recursive, Action<Transform> action)
        {
            IterateChildren(tr, recursive ? RECURSIVE_DEPTH : 1, action);
        }



        public static void ForEachChild(this Transform tr, Action<Transform> action)
        {
            IterateChildren(tr, 1, action);
        }
        public static void ForMeAndChildren(this Transform tr, Action<Transform> action)
        {
            IterateChildren(tr, 1, action);
        }




        public static Transform NewChild(this Transform parent, string name)
        {
            GameObject go = new GameObject(name);
            Transform tr = go.transform; tr.parent = parent;
            tr.localPosition = Vector3.zero; tr.localRotation = Quaternion.identity;
            return tr;
        }

        public static Transform TransformUnsafe(this GameObject obj)
        {
            return obj != null ? obj.transform : null;
        }


        // negative depth excludes tr
        public static Bounds GetBoundsIncudingChildren(this Transform tr, int depth)
        {
            Bounds bounds = new Bounds();
            tr.InvokeRecursive(depth, child =>
            {
                if (child.GetComponent<Renderer>() != null)
                    bounds.Encapsulate(child.GetComponent<Renderer>().bounds);
            });
            return bounds;
        }


        #endregion [Transform]


        #region [Components]

        static public T GetOrAddComponent<T>(this Component child) where T : Component
        {
            T result = child.GetComponent<T>();
            //Debug.Log(string.Format("{0}: {1}.Component->{2}", child, typeof(T), (result == null ? "Add" : "Get")));
            if (result == null)
                result = child.gameObject.AddComponent<T>();
            return result;
        }

        /// <summary>False when added, else true</summary>
        static public bool GetOrAddComponent<T>(this Component child, out T variable) where T : Component
        {
            variable = child.GetComponent<T>();
            if (variable == null)
            {
                variable = child.gameObject.AddComponent<T>();
                return false;
            }
            return true;
        }

        public static T TryGetComponent<T>(this Component obj) where T : Component
        {
            return obj != null ? obj.GetComponent<T>() : null;
        }

        public static T TryGetComponentParent<T>(this Transform obj) where T : Component
        {
            return (obj != null && obj.parent != null) ? obj.parent.GetComponent<T>() : null;
        }

        #endregion [Components]


        #region [Matrix]

        public static Quaternion QuaternionFromMatrix(this Matrix4x4 m)
        {
            return Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1));
        }

        public static Vector3 PositionFromMatrix(this Matrix4x4 m)
        {
            Vector4 vector4Position = m.GetColumn(3);
            return new Vector3(vector4Position.x, vector4Position.y, vector4Position.z);
        }

        public static Matrix4x4 SetPositionToMatrix(this Matrix4x4 m, Vector3 pos)
        {
            Vector4 vector4Position = m.GetColumn(3);
            m.SetColumn(3, new Vector4(pos.x, pos.y, pos.z, vector4Position.w));
            return m;
        }

        #endregion [Matrix]



        public static void SetEmission(this ParticleSystem system, bool val)
        {
            //system.enableEmission = val;
            var em = system.emission;
            em.enabled = val;
        }
    }



    // --------------------------------


    #region [Misc]




    public static class IntConvert
    {
        //public const string Digits = "0123456789"; // 10
        public const string Alphas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // 26
        //public const string AlphaNumeric = Digits + Alphas; // 36

        /// <summary>
        /// Converts the given decimal number to the numeral system with the
        /// specified radix (in the range [2, 36]).
        /// </summary>
        /// <param name="decimalNumber">The number to convert.</param>
        /// <param name="radix">The radix of the destination numeral system (in the range [2, 36]).</param>
        /// <returns></returns>
        public static string ToArbitrarySystem(long decimalNumber)
        {
            const int radix = 26;
            const string baseChars = Alphas;
            const int bitsInLong = 64;

#if UNITY_EDITOR
            if (radix < 2 || radix > baseChars.Length)
                throw new ArgumentException("The radix must be >= 2 and <= " + baseChars.Length);
#endif

            if (decimalNumber == 0)
                return baseChars[0].ToString();

            int index = bitsInLong - 1;
            long currentNumber = Math.Abs(decimalNumber);
            char[] charArray = new char[bitsInLong];

            while (currentNumber != 0)
            {
                //int remainder = (int)(currentNumber % radix);
                charArray[index--] = baseChars[(int)(currentNumber % radix)];
                currentNumber = currentNumber / radix;
            }

            string result = new String(charArray, index + 1, bitsInLong - index - 1);
            return (decimalNumber < 0) ? ("-" + result) : result;
        }

        /// <summary>
        /// Converts the given decimal number to the numeral system with the
        /// specified radix (in the range [2, 36]).
        /// </summary>
        /// <param name="decimalNumber">The number to convert.</param>
        /// <param name="radix">The radix of the destination numeral system (in the range [2, 36]).</param>
        /// <returns></returns>
        public static string ToArbitrarySystem(uint decimalNumber)
        {
            const int radix = 26;
            const string baseChars = Alphas;
            const int bitsInUint = 32;

#if UNITY_EDITOR
            if (radix < 2 || radix > baseChars.Length)
                throw new ArgumentException("The radix must be >= 2 and <= " + baseChars.Length);
#endif

            if (decimalNumber == 0)
                return baseChars[0].ToString();

            int index = bitsInUint - 1;
            long currentNumber = Math.Abs(decimalNumber);
            char[] charArray = new char[bitsInUint];

            while (currentNumber != 0)
            {
                //int remainder = (int)(currentNumber % radix);
                charArray[index--] = baseChars[(int)(currentNumber % radix)];
                currentNumber = currentNumber / radix;
            }

            return new String(charArray, index + 1, bitsInUint - index - 1);
        }


#if EXCLUDE
        public static string IntToString(int value, char[] baseChars)
        {
            string result = string.Empty;
            int targetBase = baseChars.Length;

            do
            {
                result = baseChars[value % targetBase] + result;
                value = value / targetBase;
            }
            while (value > 0);

            return result;
        }

        /// <summary>
        /// An optimized method using an array as buffer instead of
        /// string concatenation. This is faster for return values having
        /// a length > 1.
        /// </summary>
        public static string IntToStringFast(int value, char[] baseChars)
        {
            // 32 is the worst cast buffer size for base 2 and int.MaxValue
            int i = 32;
            char[] buffer = new char[i];
            int targetBase = baseChars.Length;

            do
            {
                buffer[--i] = baseChars[value % targetBase];
                value = value / targetBase;
            }
            while (value > 0);

            char[] result = new char[32 - i];
            Array.Copy(buffer, i, result, 0, 32 - i);

            return new string(result);
        }
#endif
    }


    #endregion [Misc]


    // --------------------------------


    #region [Parsing]



    public class TextParser
    {
        private string _text;
        private int _pos;

        public string Text { get { return _text; } }
        public int Position { get { return _pos; } }
        public int Remaining { get { return _text.Length - _pos; } }
        public static char NullChar = (char)0;

        public TextParser()
        {
            Reset(null);
        }

        public TextParser(string text)
        {
            Reset(text);
        }

        /// <summary>
        /// Resets the current position to the start of the current document
        /// </summary>
        public void Reset()
        {
            _pos = 0;
        }

        /// <summary>
        /// Sets the current document and resets the current position to the start of it
        /// </summary>
        /// <param name="html"></param>
        public void Reset(string text)
        {
            _text = (text != null) ? text : String.Empty;
            _pos = 0;
        }

        /// <summary>
        /// Indicates if the current position is at the end of the current document
        /// </summary>
        public bool EndOfText
        {
            get { return (_pos >= _text.Length); }
        }

        /// <summary>
        /// Returns the character at the current position, or a null character if we're
        /// at the end of the document
        /// </summary>
        /// <returns>The character at the current position</returns>
        public char Peek()
        {
            return Peek(0);
        }

        /// <summary>
        /// Returns the character at the specified number of characters beyond the current
        /// position, or a null character if the specified position is at the end of the
        /// document
        /// </summary>
        /// <param name="ahead">The number of characters beyond the current position</param>
        /// <returns>The character at the specified position</returns>
        public char Peek(int ahead)
        {
            int pos = (_pos + ahead);
            if (pos < _text.Length)
                return _text[pos];
            return NullChar;
        }

        /// <summary>
        /// Extracts a substring from the specified position to the end of the text
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public string Substring(int start)
        {
            return Substring(start, _text.Length);
        }

        /// <summary>
        /// Extracts a substring from the specified range of the current text
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public string Substring(int start, int end)
        {
            return _text.Substring(start, end - start);
        }

        /// <summary>
        /// Moves the current position ahead one character
        /// </summary>
        public void MoveAhead()
        {
            MoveAhead(1);
        }

        /// <summary>
        /// Moves the current position ahead the specified number of characters
        /// </summary>
        /// <param name="ahead">The number of characters to move ahead</param>
        public void MoveAhead(int ahead)
        {
            _pos = Math.Min(_pos + ahead, _text.Length);
        }

        /// <summary>
        /// Moves to the next occurrence of the specified string
        /// </summary>
        /// <param name="s">String to find</param>
        /// <param name="ignoreCase">Indicates if case-insensitive comparisons are used</param>
        public void MoveTo(string s, bool ignoreCase = false)
        {
            _pos = _text.IndexOf(s, _pos,
                                 ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
            if (_pos < 0)
                _pos = _text.Length;
        }

        /// <summary>
        /// Moves to the next occurrence of the specified character
        /// </summary>
        /// <param name="c">Character to find</param>
        public void MoveTo(char c)
        {
            _pos = _text.IndexOf(c, _pos);
            if (_pos < 0)
                _pos = _text.Length;
        }

        /// <summary>
        /// Moves to the next occurrence of any one of the specified
        /// characters
        /// </summary>
        /// <param name="carr">Array of characters to find</param>
        public void MoveTo(char[] carr)
        {
            _pos = _text.IndexOfAny(carr, _pos);
            if (_pos < 0)
                _pos = _text.Length;
        }

        /// <summary>
        /// Moves the current position to the first character that is part of a newline
        /// </summary>
        public void MoveToEndOfLine()
        {
            char c = Peek();
            while (c != '\r' && c != '\n' && !EndOfText)
            {
                MoveAhead();
                c = Peek();
            }
        }

        /// <summary>
        /// Moves the current position to the next character that is not whitespace
        /// </summary>
        public void MovePastWhitespace()
        {
            while (Char.IsWhiteSpace(Peek()))
                MoveAhead();
        }
    }



    public class GenericTag
    {
        /// <summary>
        /// Name of this tag
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of this tag
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// True if this tag contained a trailing forward slash
        /// </summary>
        public bool TrailingSlash { get; set; }


        public int Start { get; set; }
        public int End { get; set; }
        public int ValueStart { get; set; }
        public int ValueEnd { get; set; }
    };


    public class GenericParser : TextParser
    {
        public GenericParser()
        {
        }

        public GenericParser(string str)
        : base(str)
        {
        }

        /// <summary>
        /// Parses the next tag that matches the specified tag name
        /// </summary>
        /// <param name="name">Name of the tags to parse ("*" = parse all tags)</param>
        /// <param name="value">Returns value of the
        /// specified tag or empty string if none found</param>
        /// <returns>True if a tag was parsed or false if the end of the string was reached</returns>
        public bool ParseNext(string name, out string value)
        {
            GenericTag tag;
            bool ret = ParseNext(name, out tag);
            value = tag.Value;
            return ret;
        }

        /// <summary>
        /// Parses the next tag that matches the specified tag name
        /// </summary>
        /// <param name="name">Name of the tags to parse ("*" = parse all tags)</param>
        /// <param name="tag">Returns information on the next occurrence of the
        /// specified tag or null if none found</param>
        /// <returns>True if a tag was parsed or false if the end of the string was reached</returns>
        public bool ParseNext(string name, out GenericTag tag)
        {
            // Must always set out parameter
            tag = null;
            bool result = false;
            int start = -1;

            // Nothing to do if no tag specified
            if (String.IsNullOrEmpty(name))
                return false;

            // Loop until match is found or no more tags
            MoveTo('<');
            while (!EndOfText)
            {
                start = this.Position;

                // Skip over opening '<'
                MoveAhead();

                // Examine first tag character
                char c = Peek();
                if (c == '/') // closing tag
                {
                    MoveAhead();

                    // Was this a tag requested by caller?
                    bool requested = result && (name == "*" ||
                                                String.Compare(ParseTagName(), name, StringComparison.InvariantCultureIgnoreCase) == 0);

                    // Skip over closing tags
                    MoveTo('>');
                    MoveAhead();

                    if (requested)
                    {
                        tag.End = this.Position;
                        return true;
                    }

                }
                else if (!result)
                {
                    // Parse tag
                    result = ParseTag(name, ref tag);
                    if (result) // requested tag was found
                    {
                        tag.Start = start;
                        if (tag.TrailingSlash)
                        {
                            tag.End = this.Position;
                            return true;
                        }
                    }
                }
                // Find next tag
                MoveTo('<');
            }
            // No more matching tags found
            return false;
        }

        /// <summary>
        /// Parses the contents of an generic tag. The current position should be at the first
        /// character following the tag's opening less-than character.
        ///
        /// Note: We parse to the end of the tag even if this tag was not requested by the
        /// caller. This ensures subsequent parsing takes place after this tag
        /// </summary>
        /// <param name="reqName">Name of the tag the caller is requesting, or "*" if caller
        /// is requesting all tags</param>
        /// <param name="tag">Returns information on this tag if it's one the caller is
        /// requesting</param>
        /// <returns>True if data is being returned for a tag requested by the caller
        /// or false otherwise</returns>
        protected bool ParseTag(string reqName, ref GenericTag tag)
        {
            bool requested = false;

            // Get name of this tag
            string name = ParseTagName();

            // Is this a tag requested by caller?
            if (reqName == "*" || String.Compare(name, reqName, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                // Yes
                requested = true;
                // Create new tag object
                tag = new GenericTag();
                tag.Name = name;
                tag.Value = "";
                tag.ValueStart = tag.ValueEnd = -1;
            }

            // Parse value
            MovePastWhitespace();
            while (Peek() != '>' && Peek() != NullChar)
            {
                if (Peek() == '/')
                {
                    // Handle trailing forward slash
                    if (requested)
                        tag.TrailingSlash = true;

                    MoveAhead();
                    MovePastWhitespace();
                }
                else
                {
                    // Parse value
                    MovePastWhitespace();
                    if (Peek() == '=')
                    {
                        MoveAhead();
                        MovePastWhitespace();

                        // Add value if requested tag
                        if (requested)
                        {
                            tag.ValueStart = this.Position;
                            tag.Value = ParseValue();
                            tag.ValueEnd = this.Position;
                        }

                        MovePastWhitespace();
                    }
                }
            }
            // Skip over closing '>'
            MoveAhead();

            return requested;
        }

        /// <summary>
        /// Parses a tag name. The current position should be the first character of the name
        /// </summary>
        /// <returns>Returns the parsed name string</returns>
        protected string ParseTagName()
        {
            int start = Position;
            while (!EndOfText && !Char.IsWhiteSpace(Peek()) && Peek() != '>' && Peek() != '=')
                MoveAhead();
            return Substring(start, Position);
        }

        /// <summary>
        /// Parses an tag value. The current position should be the first non-whitespace
        /// character following the equal sign.
        ///
        /// Note: We terminate the name or value if we encounter a new line. This seems to
        /// be the best way of handling errors such as values missing closing quotes, etc.
        /// </summary>
        /// <returns>Returns the parsed value string</returns>
        protected string ParseValue()
        {
            int start, end;
            char c = Peek();
            if (c == '"' || c == '\'')
            {
                // Move past opening quote
                MoveAhead();
                // Parse quoted value
                start = Position;
                MoveTo(new char[] { c, '\r', '\n' });
                end = Position;
                // Move past closing quote
                if (Peek() == c)
                    MoveAhead();
            }
            else
            {
                // Parse unquoted value
                start = Position;
                while (!EndOfText && !Char.IsWhiteSpace(c) && c != '>')
                {
                    MoveAhead();
                    c = Peek();
                }
                end = Position;
            }
            return Substring(start, end);
        }
    }




    #endregion [Parsing]


    // --------------------------------




#if UNITY_EDITOR

    public static class EditorUtil
    {

        /// <summary>
        /// Returns the first active loaded object of given type.
        /// </summary>
        public static T FindObjectOfType<T>() where T : UnityEngine.Object
        {
            return Object.FindObjectOfType<T>();
        }

        /// <summary>
        /// Returns a list of all active loaded objects of given type.
        /// </summary>
        public static T[] FindObjectsOfType<T>() where T : UnityEngine.Object
        {
            return Object.FindObjectsOfType<T>();
        }

        /// <summary>
        /// Returns first (or default) found visible object (instance)
        /// whose class is of given type or is derived from given type. (disabled or not)
        /// </summary>
        public static T FindInstanceOfTypeAll<T>() where T : UnityEngine.Object
        {
            return Resources.FindObjectsOfTypeAll<T>().FirstOrDefault(obj => obj.hideFlags == HideFlags.None && !UnityEditor.AssetDatabase.Contains(obj));
        }


        /// <summary>
        /// Returns all visible objects (instances)
        /// whose class is of given type or is derived from given type. (disabled or not)
        /// </summary>
        public static T[] FindInstancesOfTypeAll<T>() where T : UnityEngine.Object
        {
            return Resources.FindObjectsOfTypeAll<T>().Where(obj => obj.hideFlags == HideFlags.None && !UnityEditor.AssetDatabase.Contains(obj)).ToArray();
        }

    }

#endif // UNITY_EDITOR


}



