using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public int currenLevel = 1;
    Text text;
    EndLevelPush endLevelPush;
    PortalSpawner portalSpawner;
    bool nextLevel = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start ()
    {
        text = GetComponent<Text>();
        text.text = " Level " + currenLevel.ToString();
        endLevelPush = FindObjectOfType<EndLevelPush>();
        portalSpawner = FindObjectOfType<PortalSpawner>();
        UpdateText();
        CheckCurrentLevel();
        if (FindObjectsOfType<Level>().Length > 1)
        {
            Destroy(gameObject);
        }
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
                currenLevel++;
                nextLevel = true;
            }
        }
    }

    void startLoadingNextScene()
    {
        FindObjectOfType<LoadScene>().mLoadScene(0);
    }

    public void UpdateText()
    {
        text.text = " Level " + currenLevel.ToString();
    }

    void CheckCurrentLevel()
    {
        int highiestLevel = 0;
        foreach(Level level in FindObjectsOfType<Level>())
        {
            if(highiestLevel < level.currenLevel)
            {
                highiestLevel = level.currenLevel;
            }
        }
        foreach (Level level in FindObjectsOfType<Level>())
        {
            level.currenLevel = highiestLevel;
        }
    }
}
