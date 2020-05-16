#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using Caliburn.Micro;

namespace cRGB.Modules.Common.Extensions
{
    public static class BindableCollectionExtensions
    {
        public static void RemoveAll<T>(this BindableCollection<T> collection,
            Func<T, bool> condition)
        {
            for (var i = collection.Count - 1; i >= 0; i--)
            {
                if (condition(collection[i]))
                {
                    collection.RemoveAt(i);
                }
            }
        }
    }
}