using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : SingletoneMonoBehavior<SceneManager>
{
    [SerializeField] private List<ScenePackage> _scenePackages;

    public event Action StartLoadScene;
    public event Action FinishLoadScene;

    private Coroutine _loadRoutine;

    public void LoadScenePackage(ScenePackageType type)
    {
        if(_loadRoutine != null)
            StopCoroutine(_loadRoutine);

        _loadRoutine = StartCoroutine(LoadScenesRoutine(type));
    }

    private IEnumerator LoadScenesRoutine(ScenePackageType type)
    {
        StartLoadScene?.Invoke();
        
        var currentPackage = _scenePackages.FirstOrDefault(x => x.Type == type);

        if (currentPackage == default || currentPackage.SceneAssets.Count <= 0)
        {
            FinishLoadScene?.Invoke();
            yield break;
        }

        var firstOperation =
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(currentPackage.SceneAssets[0].name, LoadSceneMode.Single);

        if (currentPackage.SceneAssets.Count <= 1)
        {
            FinishLoadScene?.Invoke();
            yield break;
        }

        var operations = new List<AsyncOperation>();

        for (int i = 1; i < currentPackage.SceneAssets.Count; i++)
        {
            operations
                .Add(UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(currentPackage.SceneAssets[i].name
                    , LoadSceneMode.Additive));
        }

        operations.Add(firstOperation);
        yield return new WaitUntil(() => operations.All(x => x.isDone));
        FinishLoadScene?.Invoke();
    }
}
