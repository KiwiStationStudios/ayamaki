#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

public static class ImporterRuntimeCheck
{
    [MenuItem("Tools/Debug/Check Importer For Extension")]
    static void Check()
    {
        // MUDA AQUI pra extensão problemática
        string ext = "lua";

        // cria arquivo temporário
        string path = $"Assets/__importer_test__.{ext}";
        File.WriteAllText(path, "test");

        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

        var importer = AssetImporter.GetAtPath(path);

        Debug.Log(
            importer != null
            ? $"Extensão .{ext} está usando importer: {importer.GetType().FullName}"
            : $"Nenhum importer para .{ext}"
        );

        AssetDatabase.DeleteAsset(path);
    }
}
#endif
