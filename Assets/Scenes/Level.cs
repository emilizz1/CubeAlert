using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    BlackHoleSpawner portalSpawner;
    LevelHolder levelHolder;
    bool nextLevel = false;

    void Start ()
    {
        portalSpawner = FindObjectOfType<BlackHoleSpawner>();
        levelHolder = FindObjectOfType<LevelHolder>();
        UpdateText();
    }

    void Update()
    {
        CheckIfLevelEnded();
    }

    void CheckIfLevelEnded()
    {
        if (portalSpawner.GetSpawningFinished() && FindObjectsOfType<BlackHole>().Length <= 0)
        {
            FindObjectOfType<EndLevelFlash>().EndLevel();
            if (!nextLevel)
            {
                Invoke("startLoadingNextScene", 3f);
                levelHolder.currentLevel++;
                nextLevel = true;
                FindObjectOfType<Timer>().playing = false;
            }
        }
    }

    void startLoadingNextScene()
    {
        FindObjectOfType<LoadScene>().mLoadScene(0);
    }

    void UpdateText()
    {
        GetComponent<Text>().text = levelHolder.currentLevel.ToString();
    }
}
