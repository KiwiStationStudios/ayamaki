#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Ayamaki.Core.GameManager;

public class LocalValuesDebugger : EditorWindow
{
    [MenuItem("Debug/Local Values")]
    static void OpenWindow() => GetWindow<LocalValuesDebugger>("Local Values");

    private void OnGUI()
    {
        if (GameManager.Instance == null)
        {
            GUILayout.Label("No GameManager or LocalValues found.");
            return;
        }

        foreach (var entry in GameManager.Instance.localValues.dict)
        {
            switch (entry.Value.type)
            {
                case ValueEntry.ValueType.Int:
                    GUILayout.Label($"(Int){entry.Key} : {entry.Value.intValue}");
                    break;
                case ValueEntry.ValueType.Float:
                    GUILayout.Label($"(Float){entry.Key} : {entry.Value.floatValue}");
                    break;
                case ValueEntry.ValueType.String:
                    GUILayout.Label($"(String){entry.Key} : {entry.Value.stringValue}");
                    break;
            }
        }
    }
}
#endif
