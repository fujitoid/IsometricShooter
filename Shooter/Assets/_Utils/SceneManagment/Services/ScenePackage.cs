using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScenePackage : ScriptableObject
{
#if UNITY_EDITOR
    [Space] [SerializeField] private List<SceneAsset> _sceneAssets;
#endif
    [SerializeField] private ScenePackageType _type;


    public ScenePackageType Type => _type;

#if UNITY_EDITOR
    public IReadOnlyList<SceneAsset> SceneAssets => _sceneAssets;
#endif
}
