using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoadScene : MonoBehaviour
{
    [SerializeField] int tutorialSceneNumber = 2;
    [SerializeField] int mainGameSceneNumber = 1;

    public void mLoadScene()
    {
        if (FindObjectOfType<TutorialCompleted>().GetIsTutorialCompleted())
        {
            SceneManager.LoadScene(mainGameSceneNumber);
        }
        else
        {
            SceneManager.LoadScene(tutorialSceneNumber);
        }
    }
}
