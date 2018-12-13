using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLoadScene : MonoBehaviour
{
    [SerializeField] int tutorialSceneNumber = 2;
    [SerializeField] int firstLevelNumber = 1;

    void Start()
    {
        if (FindObjectOfType<LevelHolder>())
        {
            FindObjectOfType<LevelHolder>().currentLevel = 1;
        }
    }

    public void mLoadScene()
    {
        if (FindObjectOfType<TutorialCompleted>().GetIsTutorialCompleted())
        {
            FindObjectOfType<LoadScene>().mLoadScene(firstLevelNumber);
        }
        else
        {
            FindObjectOfType<LoadScene>().mLoadScene(tutorialSceneNumber);
        }
    }
}
