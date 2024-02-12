using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace RoachCoach
{

    public static class Extentions
    {
        public static T RandomElement<T>(this T[] values)
        {
            return values[Random.Range(0, values.Length)];
        }

        public static T RandomElement<T>(this List<T> values)
        {
            return values[Random.Range(0, values.Count)];
        }
        public static KeyValuePair<T, L> RandomElement<T, L>(this Dictionary<T, L> values)
        {

            return values.ElementAt(Random.Range(0, values.Count));
        }
    }

}
