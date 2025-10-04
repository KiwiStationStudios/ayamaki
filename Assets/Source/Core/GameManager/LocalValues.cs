using System;
using System.Collections.Generic;
using UnityEngine;


namespace Ayamaki.Core.GameManager
{
    [CreateAssetMenu(fileName = "NewLocalValues", menuName = "Game/New Local Value")]
    public class LocalValues : ScriptableObject
    {
        [SerializeField] private List<ValueEntry> values = new();

        public Dictionary<string, ValueEntry> dict;

        void OnEnable()
        {
            BuildDict();
        }

        private void BuildDict()
        {
            dict = new Dictionary<string, ValueEntry>();
            foreach (var v in values)
            {
                if (!dict.ContainsKey(v.key))
                    dict.Add(v.key, v);
            }
        }

        public T Get<T>(string key, T defaultValue = default)
        {
            if (dict == null) BuildDict();

            if (dict.TryGetValue(key, out var entry))
                return (T)entry.GetValue();

            return defaultValue;
        }

        public void Set<T>(string key, T value)
        {
            if (dict == null) BuildDict();

            if (dict.TryGetValue(key, out var entry))
            {
                entry.SetValue(value);
            }
            else
            {
                var newEntry = new ValueEntry { key = key };
                if (value is int i) { newEntry.type = ValueEntry.ValueType.Int; newEntry.intValue = i; }
                else if (value is float f) { newEntry.type = ValueEntry.ValueType.Float; newEntry.floatValue = f; }
                else if (value is string s) { newEntry.type = ValueEntry.ValueType.String; newEntry.stringValue = s; }

                values.Add(newEntry);
                dict.Add(key, newEntry);
            }
        }
    }
}