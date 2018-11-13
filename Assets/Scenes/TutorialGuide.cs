using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuide : MonoBehaviour
{
    [SerializeField] float maxSize = 2f;
    [SerializeField] float minSize = 1f;
    [SerializeField] float changeSpeed = 0.1f;
    [SerializeField] GameObject arrow;
    [SerializeField] float minArrowSize = 0.65f;
    [SerializeField] float maxArrowSize = 1.15f;

    bool gettingBigger = true;

    Transform currentlyMoving;

    void Start()
    {
        currentlyMoving = transform;
    }

    void Update()
    {
        if (gettingBigger)
        {
            currentlyMoving.localScale =  Vector3.MoveTowards(currentlyMoving.localScale, new Vector3(maxSize, maxSize, maxSize), changeSpeed);
            if(currentlyMoving.localScale.x == maxSize)
            {
                gettingBigger = false;
            }
        }
        else
        {
            currentlyMoving.localScale = Vector3.MoveTowards(currentlyMoving.localScale, new Vector3(minSize, minSize, minSize), changeSpeed);
            if (currentlyMoving.localScale.x == minSize)
            {
                gettingBigger = true;
            }
        }

        if(FindObjectOfType<BlackHole>() == null)
        {
            arrow.SetActive(false);
            FindObjectOfType<EndLevelFlash>().EndLevel();
            FindObjectOfType<TutorialCompleted>().TutorialFinished();
            Invoke("StartLoadingScene", 2f);
        }
    }

    void StartLoadingScene()
    {
        FindObjectOfType<LoadScene>().mLoadScene(1);
    }

    public void Tapped()
    {
        arrow.SetActive(true);
        Destroy(GetComponent<SpriteRenderer>());
        currentlyMoving = arrow.transform;
        minSize = minArrowSize;
        maxSize = maxArrowSize;
    }
}
