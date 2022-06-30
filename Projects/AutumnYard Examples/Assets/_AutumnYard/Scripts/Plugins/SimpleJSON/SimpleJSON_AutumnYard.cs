using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleJSON
{
    public partial class JSONObject : JSONNode
    {
        public JSONObject() { }

        public JSONObject(Dictionary<string, int> dict)
        {
            foreach (var item in dict)
            {
                m_Dict.Add(item.Key, item.Value);
            }
        }

        public JSONObject(string name, int value)
        {
            m_Dict.Add(name, value);
        }

        public JSONObject(string[] names, int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                m_Dict.Add(names[i], array[i]);
            }
        }
    }
}