using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {
    public enum Scene
    {
        MainMenu,
        Game,
        Loading
    }
    public static void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
    public static IEnumerator LoadSceneAsync(Scene scene)
    {
        SceneManager.LoadScene(Scene.Loading.ToString());

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene.ToString());
        asyncLoad.allowSceneActivation = false; 

        yield return new WaitForSeconds(2f); 
        while (!asyncLoad.isDone)
        {
            asyncLoad.allowSceneActivation = true; 
            yield return null;
        }

        SceneManager.UnloadSceneAsync(Scene.Loading.ToString());
    }
}
