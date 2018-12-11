using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCompleted : MonoBehaviour
{
    bool isCompleted = false;

    void Awake()
    {
        var numOfBackgroundThemes = FindObjectsOfType<TutorialCompleted>().Length;
        if (numOfBackgroundThemes > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool GetIsTutorialCompleted()
    {
        return isCompleted;
    }

    public void TutorialFinished()
    {
        isCompleted = true;
    }
}
