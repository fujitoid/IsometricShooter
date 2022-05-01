using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSceneLoader : MonoBehaviour
{
    [SerializeField] private ScenePackageType _type;

    private void Start()
    {
        if (SceneManager.Instance)
        {
            SceneManager.Instance.LoadScenePackage(_type);
        }
    }
}
