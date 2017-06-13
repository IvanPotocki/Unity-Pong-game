using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    public string sceneToLoad;

    public int secTillSceneLoad;

    void Start()
    {
        Invoke("OpenNextScene", secTillSceneLoad);
    }

    // Update is called once per frame
    void OpenNextScene()
    {
        // Load defined scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
