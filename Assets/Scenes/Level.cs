using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    EndLevelPush endLevelPush;
    PortalSpawner portalSpawner;
    LevelHolder levelHolder;
    bool nextLevel = false;

    void Start ()
    {
        endLevelPush = FindObjectOfType<EndLevelPush>();
        portalSpawner = FindObjectOfType<PortalSpawner>();
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
            endLevelPush.Explode();
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
        GetComponent<Text>().text = " Level " + levelHolder.currentLevel.ToString();
    }
}
