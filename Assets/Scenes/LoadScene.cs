using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    int currentScene;

    void Awake()
    {
        var numOfBackgroundThemes = FindObjectsOfType<LoadScene>().Length;
        if (numOfBackgroundThemes > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if(currentScene != SceneManager.GetActiveScene().buildIndex)
        {
            StartCoroutine(GetComponent<StartEndLevelCanvas>().CanvasDisapearring());
            currentScene = SceneManager.GetActiveScene().buildIndex;
        }
    }

    public void mLoadScene(int scene)
    {
        //preparing to load a scene
        StartCoroutine(GetComponent<StartEndLevelCanvas>().CanvasApearring(scene));
    }
}
