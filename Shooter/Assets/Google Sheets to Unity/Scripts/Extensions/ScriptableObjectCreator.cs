using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectCreator : Editor
{
    [MenuItem("Assets/Create/Vasilisk Tools/Create this ScriptableObject")]
    public static void CreateThisScriptableObject()
    {
        var selections = Selection.objects;

        foreach(var selection in selections)
        {
            var selectedPath = AssetDatabase.GetAssetPath(selection);

            var directory = Path.GetDirectoryName(selectedPath);
            var fileName = Path.GetFileNameWithoutExtension(selectedPath);
            var newFilePath = Path.Combine(directory, $"{fileName}.asset");

            var assetPath = AssetDatabase.GenerateUniqueAssetPath(newFilePath);
            var so = ScriptableObject.CreateInstance((selection as MonoScript).GetClass());

            AssetDatabase.CreateAsset(so, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
