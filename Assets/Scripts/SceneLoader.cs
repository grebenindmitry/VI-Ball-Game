using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{ 
    public void StartLoadingScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    //Load a scene asynchronously
    private static IEnumerator LoadSceneAsync(string sceneName)
    {
        var loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);

        while (!loadSceneAsync.isDone)
        {
            yield return null;
        }
    }
}
