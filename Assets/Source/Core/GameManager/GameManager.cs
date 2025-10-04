using Ayamaki.Core.GameManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Local Values (ScriptableObject original)")]
    [SerializeField] private LocalValues localValuesAsset;

    // Cópia em memória para esta cena
    private LocalValues localValuesInstance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Cria a cópia temporária
        if (localValuesAsset != null)
        {
            localValuesInstance = Instantiate(localValuesAsset);
            localValuesInstance.name = localValuesAsset.name + "_RuntimeCopy";
        }
    }

    public LocalValues localValues => localValuesInstance;

    // Métodos para facilitar acesso
    public T GetLocal<T>(string key, T defaultValue = default) =>
        localValuesInstance != null ? localValuesInstance.Get<T>(key, defaultValue) : defaultValue;

    public void SetLocal<T>(string key, T value)
    {
        if (localValuesInstance != null)
            localValuesInstance.Set<T>(key, value);
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
