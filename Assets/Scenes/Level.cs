using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    Text text;
    EndLevelPush endLevelPush;
    PortalSpawner portalSpawner;
    LevelHolder levelHolder;
    bool nextLevel = false;

    void Start ()
    {
        text = GetComponent<Text>();
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
        if (portalSpawner.GetSpawningFinished() && FindObjectsOfType<Portal>().Length <= 0)
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

    public void UpdateText()
    {
        text.text = " Level " + levelHolder.currentLevel.ToString();
    }
}
