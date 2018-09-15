using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    int currenLevel = 1;
    Text text;
    EndLevelPush endLevelPush;
    PortalSpawner portalSpawner;
    bool nextLevel = false;
    
	void Start ()
    {
        text = GetComponent<Text>();
        text.text = " Level " + currenLevel.ToString();
        endLevelPush = FindObjectOfType<EndLevelPush>();
        portalSpawner = FindObjectOfType<PortalSpawner>();
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
                nextLevel = true;
            }
        }
    }

    void startLoadingNextScene()
    {
        FindObjectOfType<LoadScene>().mLoadScene(2);
    }

    public void NextLevel()
    {
        currenLevel++;
        text.text = " Level " + currenLevel.ToString();
    }
}
