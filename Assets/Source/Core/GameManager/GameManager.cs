using Ayamaki.Core.GameAPI;
using UnityEngine;

namespace Ayamaki.Core.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("Local Values (ScriptableObject original)")]
        [SerializeField] private LocalValues localValuesAsset;

        // Cópia em memória para esta cena
        private LocalValues localValuesInstance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                //
                LuaAPI.StartAPI();
                // Cria a cópia temporária
                if (localValuesAsset != null)
                {
                    localValuesInstance = Instantiate(localValuesAsset);
                    localValuesInstance.name = localValuesAsset.name + "_RuntimeCopy";
                }
                return;
            }

            Destroy(this);
        }

        public LocalValues localValues => localValuesInstance;

        // Métodos para facilitar acesso
        public T GetLocal<T>(string key, T defaultValue = default)
        {
            return Instance != null ? localValuesInstance.Get(key, defaultValue) : defaultValue;
        }
            

        public void SetLocal<T>(string key, T value)
        {
            if (localValuesInstance != null)
                localValuesInstance.Set(key, value);
        }

        [ContextMenu("Print Local Values")]
        private void PrintLocalValues()
        {
            foreach (var entry in localValues.dict)
            {
                Debug.Log($"{entry.Key} = {entry.Value}");
            }
        }
    }

}