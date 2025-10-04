using System;
using UnityEngine;

namespace Ayamaki.Core.GameManager
{
    [Serializable]
    public class ValueEntry
    {
        public string key;
        public ValueType type;
        public int intValue;
        public float floatValue;
        public string stringValue;

        public enum ValueType { Int, Float, String }

        public object GetValue()
        {
            return type switch
            {
                ValueType.Int => intValue,
                ValueType.Float => floatValue,
                ValueType.String => stringValue,
                _ => null
            };
        }

        public void SetValue(object value)
        {
            switch (type)
            {
                case ValueType.Int:
                    intValue = Convert.ToInt32(value);
                    break;
                case ValueType.Float:
                    floatValue = Convert.ToSingle(value);
                    break;
                case ValueType.String:
                    stringValue = Convert.ToString(value);
                    break;
            }
        }
    }
}