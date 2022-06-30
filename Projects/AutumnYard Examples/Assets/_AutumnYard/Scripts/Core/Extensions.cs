using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;

namespace AutumnYard
{
    public static class Extensions
    {
        #region MonoBehaviour extensions

        public static bool GetComponentIfNull<T>(this Component self, ref T what, bool lookInChildren = true, bool throwException = false) where T : class
        {
            if (what != null) return true;
            if (what == null) what = self.GetComponent<T>();
            if (what == null && lookInChildren) what = self.GetComponentInChildren<T>(true);
            if (what == null && throwException) throw new NullReferenceException($"Couldn't find component of type {typeof(T)} in {self.gameObject.name}");
            return what == null;
        }

        public static bool GetOrAddComponent<T>(this Component self, ref T what) where T : Component
        {
            if (what != null) return true;
            if (what == null) what = self.GetComponent<T>();
            if (what == null) what = self.gameObject.AddComponent<T>();
            return what == null;
        }

        public static bool FindComponent<T>(this Component go, string where, ref T what) where T : Component
        {
            if (what != null) return true;
            if (what == null) what = GameObject.Find(where).GetComponent<T>();
            return what == null;
        }

        public static GameObject[] GetChildren(this Component self, bool includeInactive = false)
        {
            return self
                .GetComponentsInChildren<Transform>(includeInactive)
                .Where(c => c != self.transform)
                .Select(c => c.gameObject)
                .ToArray()
            ;
        }
        #endregion


        #region Other Unity scripts

        public static bool Contains(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
        public static int[] GetIncludedLayers(this LayerMask mask)
        {
            List<int> layers = new List<int>();
            for (int i = 0; i < 32; i++)
            {
                var contains = mask.Contains(i);
                if (contains)
                    layers.Add(i);
            }
            return layers.ToArray();
        }
        public static bool[] GetLayersAsBool(this LayerMask mask)
        {
            bool[] boolArray = new bool[32];
            for (int i = 0; i < 32; i++)
                boolArray[i] = mask.Contains(i);
            return boolArray;
        }

        public static void DoSelect(this UnityEngine.UI.Selectable go)
        {
            go.Select();
            //go.OnSelect(null);
        }

        #endregion // Other Unity scripts


        #region Primitive extensions

        public static void LimitMax(this float value, float check)
        {
            value = value > check ? check : value;
        }

        public static void LimitMin(this float value, float check)
        {
            value = value < check ? check : value;
        }

        #endregion


        #region Coroutine extensions

        /// <summary>
        /// Runs the Callback at the end of the current frame, after GUI rendering
        /// </summary>
        /// <param name="self"></param>
        /// <param name="Callback">What to do at the end of this frame</param>
        /// <returns></returns>
        public static Coroutine WaitForEndOfFrame(this MonoBehaviour self, UnityAction Callback)
        {
            return self.StartCoroutine(EndOfFrameCoroutine(Callback));
            static IEnumerator EndOfFrameCoroutine(UnityAction Callback)
            {
                yield return new WaitForEndOfFrame();
                Callback?.Invoke();
            }
        }

        /// <summary>
        /// Runs the Callback after a given number of seconds, after the Update completes
        /// </summary>
        /// <param name="self"></param>
        /// <param name="seconds"></param>
        /// <param name="Callback"></param>
        /// <returns></returns>
        public static Coroutine WaitForSeconds(this MonoBehaviour self, float seconds, UnityAction Callback)
        {
            return self.StartCoroutine(InSecondsCoroutine(seconds, Callback));

            static IEnumerator InSecondsCoroutine(float seconds, UnityAction Callback)
            {
                yield return new WaitForSeconds(seconds);
                Callback?.Invoke();
            }
        }
        public static async void Invoke(this Action action, float delay)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
#if UNITY_EDITOR
            if (!EditorApplication.isPlayingOrWillChangePlaymode &&
                EditorApplication.isPlaying)
            {
                return;
            }
#endif
            action?.Invoke();
        }

        #endregion

        // TODO: Refactor&Ordering. Llevarme las extensiones a distintos ficheros
        #region Enum extensions
        public static TEnum[] GetValues<TEnum>() where TEnum : Enum
        {
            return (TEnum[])Enum.GetValues(typeof(TEnum));
        }

        public static int GetEnumLength<T>() where T : Enum
        {
            Type enumType = typeof(T);

            if (enumType.IsEnum)
            {
                return Enum.GetValues(enumType).Length;
            }

            return -1;
        }

        public static int GetLength(this Type enumType)
        {
            if (enumType.IsEnum)
            {
                return Enum.GetValues(enumType).Length;
            }

            return -1;
        }

        public static string GetNames(this Type enumType)
        {
            if (enumType.IsEnum)
            {
                var names = Enum.GetNames(enumType);
                System.Text.StringBuilder sb = new System.Text.StringBuilder($"Enum {enumType.Name}: ");
                for (int i = 0; i < names.Length; i++)
                {
                    sb.Append(names[i]);
                    if (i != names.Length - 1) sb.Append(", ");
                }
                return sb.ToString();
            }

            return string.Empty;
        }

        #region Enum parsing and conversion

        public static TOut Convert<TOut>(this string input, bool throwError = true)
          where TOut : struct, Enum
        {
            TOut thingie = default;
            try
            {
                Enum.TryParse(input, out thingie);
            }
            catch (Exception)
            {
                if (throwError)
                {
                    Debug.LogErrorFormat($"No encuentro el valor {input} en el enumerado {typeof(TOut)}.");
                }
            }
            return thingie;
        }

        public static TOut Convert<TIn, TOut>(this TIn input, bool throwError = true) where TIn : System.Enum where TOut : struct
        {
            // CUIDADIN: Pongo el constraint de T2 como 'struct' porque es nullable, y TryParse me exije un nullable
            // Preferiria poner 'System.Enum', pero es non-nullable
            if (System.Enum.TryParse(input.ToString(), false, out TOut output))
            {
                return output;
            }
            else
            {
                if (throwError)
                {
                    Debug.LogErrorFormat($"No encuentro el valor {input} del enumerado {typeof(TIn)} en el enumerado {typeof(TOut)}.");
                }
                return default(TOut);
            }
        }

        public static bool Convert<TOut>(this string input, out TOut output, bool throwError = true)
          where TOut : struct, Enum
        {
            output = default(TOut);
            try
            {
                return Enum.TryParse(input, false, out output);
            }
            catch (Exception)
            {
                if (throwError)
                {
                    Debug.LogErrorFormat($"No encuentro el valor {input} en el enumerado {typeof(TOut)}.");
                }
                return false;
            }
        }

        public static bool Convert<TIn, TOut>(this TIn input, out TOut output, bool throwError = true) where TIn : System.Enum where TOut : struct
        {
            // CUIDADIN: Pongo el constraint de T2 como 'struct' porque es nullable, y TryParse me exije un nullable
            // Preferiria poner 'System.Enum', pero es non-nullable
            output = default(TOut);

            if (System.Enum.TryParse(input.ToString(), false, out output))
            {
                return true;
            }
            else
            {
                if (throwError)
                {
                    Debug.LogErrorFormat($"No encuentro el valor {input} del enumerado {typeof(TIn)} en el enumerado {typeof(TOut)}.");
                }
                return false;
            }
        }

        #endregion

        #endregion


        #region Vector extensions

        public static void Deconstruct(this Vector2 self, out float x, out float y)
        {
            x = self.x;
            y = self.y;
        }

        public static void Deconstruct(this Vector3 self, out float x, out float y)
        {
            x = self.x;
            y = self.y;
        }

        public static void Deconstruct(this Vector3 self, out float x, out float y, out float z)
        {
            x = self.x;
            y = self.y;
            z = self.z;
        }

        #endregion


        #region Collection extensions

        public static bool IsNullOrEmpty<T>(this List<T> list) => list == null || list.Count == 0;
        public static bool IsNullOrEmpty<T>(this T[] array) => array == null || array.Length == 0;

        public static void For<T>(this List<T> list, System.Action<T> action)
        {
            if (list == null || list.Count <= 0)
                return;

            for (int i = 0; i < list.Count; i++)
            {
                action.Invoke(list[i]);
            }
        }
        public static void For<T>(this T[] array, System.Action<T> action)
        {
            if (array == null || array.Length == 0)
                return;

            for (int i = 0; i < array.Length; i++)
            {
                action.Invoke(array[i]);
            }
        }

        public static T GetRandom<T>(this T[] array)
        {
            if (array == null)
                throw new InvalidOperationException("Trying to access uninitialized array.");

            if (array.Length <= 0)
                throw new InvalidOperationException("Trying to access empty array.");

            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        public static int GetNextIndex<T>(this T[] self, int current)
        {
            current++;
            if (current >= self.Length) current = 0;
            return current;
        }

        public static int GetPreviousIndex<T>(this T[] self, int current)
        {
            current--;
            if (current < 0) current = self.Length - 1;
            return current;
        }

        // Intento 1 con enumerados
        //public static T EnumIterateNext<T>(this T[] enumerat, T current) where T : Enum
        //{
        //  var values = Enum.GetValues(typeof(T));
        //  int length = values.Length;
        //  int intCurrent = current.Convert<T, int>();
        //  intCurrent++;
        //  if (intCurrent >= length) intCurrent = 0;
        //  return (T)Enum.GetValues(typeof(T)).GetValue(intCurrent);
        //}

        // Intento 2
        //public static int EnumIterateNext<T>(this T[] enumerat, T current) where T : Enum
        //{
        //  var values = Enum.GetValues(typeof(T));
        //  int length = values.Length;
        //  int intCurrent = current.Convert<T, int>();
        //  intCurrent++;
        //  if (intCurrent >= length) intCurrent = 0;
        //  return intCurrent;
        //}

        // Intento 3
        //public static T EnumIterateNext<T>(T current) where T:Enum
        //{
        //  var values = Enum.GetValues(typeof(T));
        //  int length = values.Length;
        //  int intCurrent = current.Convert<T, int>();
        //  intCurrent++;
        //  if (intCurrent >= length) intCurrent = 0;
        //  return (T)values.GetValue(intCurrent);
        //}

        // Intento 4 con enumerados
        //public static T EnumIterateNext<T>(T current) where T: Enum, IComparable, IFormattable, IConvertible
        //{
        //  int value = (int)current;
        //  if ((int)current++ >= Enum.GetValues(typeof(Enum)).Length - 1) current = 0;
        //  return current;
        //}

        // por fin!
        public static T EnumNext<T>(this T current) where T : Enum
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(string.Format("Argument {0} is not an Enum", typeof(T).FullName));

            T[] array = (T[])Enum.GetValues(current.GetType());
            int j = Array.IndexOf<T>(array, current) + 1;
            return (j == array.Length) ? array[0] : array[j];
        }

        public static void EnumNext<T>(this T current, bool throwError = true) where T : Enum
        {
            if (!typeof(T).IsEnum && throwError) throw new ArgumentException(string.Format("Argument {0} is not an Enum", typeof(T).FullName));

            T[] array = (T[])Enum.GetValues(current.GetType());
            int j = Array.IndexOf<T>(array, current) + 1;
            current = (j == array.Length) ? array[0] : array[j];
        }

        public static T EnumPrevious<T>(this T current) where T : Enum
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(string.Format("Argument {0} is not an Enum", typeof(T).FullName));

            T[] array = (T[])Enum.GetValues(current.GetType());
            int j = Array.IndexOf<T>(array, current) - 1;
            return (j < 0) ? array[array.Length - 1] : array[j];
        }

        //public static T IterateNext<T>(this T enumerat, T current) where T : Enum
        //{
        //  var values = Enum.GetValues(typeof(T));
        //  int length = values.Length;
        //  int intCurrent = current.Convert<T,int>() ;
        //  intCurrent++;
        //  if (intCurrent >= length) intCurrent = 0;
        //  return (T)values.GetValue(intCurrent);
        //}

        //public static T IteratePrevious<T>(this T enumerat, int current) where T : Enum
        //{
        //  var values = Enum.GetValues(typeof(T));
        //  int length = values.Length;
        //  current--;
        //  if (current < 0) current = length - 1;
        //  return (T)values.GetValue(current);
        //}

        #endregion

        #region DateTime

        public static string OnlyDate(this DateTime self)
        {
            return self.ToString("yyyyMMdd");
        }
        public static string OnlyTime(this DateTime self)
        {
            return self.ToString("HHmmss");
        }
        public static string DateAndTime(this DateTime self)
        {
            return self.ToString("yyyyMMddHHmmss");
        }
        #endregion

    }
}
