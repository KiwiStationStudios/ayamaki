using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ayamaki.Core
{
    [ExecuteInEditMode]
    public class GameManager : MonoBehaviour
    {
        public enum StorageHolder
        {
            SHARED,
            LOCAL
        }

        [SerializeField] internal ValueField[] sharedValues;
        [SerializeField] internal ValueField[] sceneValues;

        public Dictionary<string, string> globalvalues = new();
        public Dictionary<string, string> localValues = new();

        void Start()
        {
            // force a re-index on the dictionaries //
            localValues.Clear();
            globalvalues.Clear();

            for (int i = 0; i < sceneValues.Length; i++)
            {
                ValueField item = sceneValues[i];

                // ignore if the key already exist
                if (localValues[item.name] != null)
                    return;

                localValues.Add(item.name, item.value);
            }

            SceneManager.sceneUnloaded += OnSceneLeave;
        }

        private string GetValue(StorageHolder store, string key)
        {
            if (store == StorageHolder.SHARED)
                if (globalvalues[key] != null)
                    return globalvalues[key];
                else if (store == StorageHolder.LOCAL)
                    if (localValues[key] != null)
                        return localValues[key];
            return "";
        }

        private void SetValue(StorageHolder store, string key, string value)
        {
            if (store == StorageHolder.SHARED)
                if (globalvalues[key] != null)
                    globalvalues[key] = value;
                else if (store == StorageHolder.LOCAL)
                    if (localValues[key] != null)
                        localValues[key] = value;
        }

        public string GetLocalValue(string key)
        {
            return GetValue(StorageHolder.LOCAL, key);
        }

        public string GetSharedValue(string key)
        {
            return GetValue(StorageHolder.SHARED, key);
        }

        public void SetSharedValue(string key, string value)
        {
            SetValue(StorageHolder.SHARED, key, value);
        }

        public void SetLocalValue(string key, string value)
        {
            SetValue(StorageHolder.LOCAL, key, value);
        }

        void OnSceneLeave(Scene scene)
        {
            localValues.Clear();

            Array.Clear(sceneValues, 0, sceneValues.Length - 1);

        }
    }

    [Serializable]
    public class ValueField
    {
        public string name = "";
        public string value = "";
    }
}
