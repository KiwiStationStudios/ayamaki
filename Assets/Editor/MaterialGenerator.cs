using UnityEngine;
using UnityEditor;
using System.IO;

public class AutoMaterialGenerator : EditorWindow
{
    string texturesFolder = "Assets/Textures";
    string outputFolder = "Assets/Materials";
    public Shader defaultShader;

    [MenuItem("Tools/Auto Material Generator")]
    public static void ShowWindow()
    {
        GetWindow<AutoMaterialGenerator>("Auto Material Generator");
    }

    void OnGUI()
    {
        GUILayout.Label("Material Generator", EditorStyles.boldLabel);

        texturesFolder = EditorGUILayout.TextField("Textures Folder", texturesFolder);
        outputFolder = EditorGUILayout.TextField("Output Folder", outputFolder);

        if (GUILayout.Button("Generate Materials"))
        {
            GenerateMaterials();
        }
    }

    void GenerateMaterials()
    {
        defaultShader = Shader.Find("Universal Render Pipeline/Lit");

        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        string[] texturePaths = Directory.GetFiles(texturesFolder, "*.png", SearchOption.AllDirectories);

        foreach (var path in texturePaths)
        {
            string assetPath = path.Replace("\\", "/");
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);

            if (texture == null) continue;

            string matName = Path.GetFileNameWithoutExtension(assetPath);
            string matPath = $"{outputFolder}/{matName}.mat";

            if (File.Exists(matPath))
                continue; // já existe, pula

            Material mat = new Material(defaultShader);
            mat.mainTexture = texture;

            AssetDatabase.CreateAsset(mat, matPath);
            Debug.Log($"✅ Criado: {matName}.mat");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("✨ Materiais gerados com sucesso!");
    }
}
