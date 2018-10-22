using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuide : MonoBehaviour
{
    [SerializeField] float maxSize = 2f;
    [SerializeField] float minSize = 1f;
    [SerializeField] float changeSpeed = 0.1f;

    bool gettingBigger = true;

    void Update()
    {
        if (gettingBigger)
        {
            transform.localScale =  Vector3.MoveTowards(transform.localScale, new Vector3(maxSize, maxSize, maxSize), changeSpeed);
            if(transform.localScale.x == maxSize)
            {
                gettingBigger = false;
            }
        }
        else
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(minSize, minSize, minSize), changeSpeed);
            if (transform.localScale.x == minSize)
            {
                gettingBigger = true;
            }
        }

        if(FindObjectOfType<BlackHole>() == null)
        {
            FindObjectOfType<EndLevelFlash>().EndLevel();
            Invoke("StartLoadingScene", 2f);
        }
    }

    void StartLoadingScene()
    {
        FindObjectOfType<LoadScene>().mLoadScene(0);
    }

    public void Tapped()
    {
        Destroy(GetComponent<SpriteRenderer>());
    }
}
