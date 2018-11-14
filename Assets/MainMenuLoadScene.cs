using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLoadScene : MonoBehaviour
{
    [SerializeField] int tutorialSceneNumber = 2;
    [SerializeField] int mainGameSceneNumber = 1;

    public void mLoadScene()
    {
        if (FindObjectOfType<TutorialCompleted>().GetIsTutorialCompleted())
        {
            FindObjectOfType<LoadScene>().mLoadScene(mainGameSceneNumber);
        }
        else
        {
            FindObjectOfType<LoadScene>().mLoadScene(tutorialSceneNumber);
        }
    }
}
