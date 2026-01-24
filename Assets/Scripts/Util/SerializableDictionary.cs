using System;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
    {
        public Dictionary<TKey, TValue> data = new Dictionary<TKey, TValue>();
        
        [SerializeField] private List<TKey> keys = new List<TKey>();
        [SerializeField] private List<TValue> values = new List<TValue>();

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();

            foreach (var keyValuePair in data)
            {
                keys.Add(keyValuePair.Key);
                values.Add(keyValuePair.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            data.Clear();
            for (int i = 0; i < Math.Min(keys.Count, values.Count); i++)
            {
                data[keys[i]] = values[i];
            }
        }
    }
}