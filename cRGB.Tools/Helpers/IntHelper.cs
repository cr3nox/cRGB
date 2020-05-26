#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;
using System.Linq;

namespace cRGB.Tools.Helpers
{
    public class IntHelper
    {
        /// <summary>
        /// 1,3,5-10,12 -> 1,3,5,6,7,8,9,10,12
        /// </summary>
        /// <param name="integerString"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetPageNumbersFromString(string integerString)
        {
            foreach (var s in integerString.Split(','))
            {
                // try and get the number
                if (int.TryParse(s, out var num))
                {
                    yield return num;
                    continue; // skip the rest
                }

                // otherwise we might have a range
                // split on the range delimiter
                var subs = s.Split('-');

                // now see if we can parse a start and end
                if (subs.Length <= 1 || !int.TryParse(subs[0], out var start) || !int.TryParse(subs[1], out var end) ||
                    end < start) continue;
                // create a range between the two values
                var rangeLength = end - start + 1;
                foreach (var i in Enumerable.Range(start, rangeLength))
                {
                    yield return i;
                }
            }
        }
    }
}