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
    [SerializeField] GameObject comet;
    [SerializeField] GameObject upgrade;

    bool gettingBigger = true;
    bool once = true;

    Stage currentStage = Stage.followingComet;

    enum Stage
    {
        followingComet, 
        followingUpgrade,
        starTap,
        bhArrow
    }

    Transform currentlyMoving;

    void Start()
    {
        Time.timeScale = 1f;
        currentlyMoving = transform;
        comet.GetComponent<Comet>().GiveStartingRotation(FaceThePortal(comet.transform.position));
    }

    void Update()
    {
        switch (currentStage)
        {
            case (Stage.followingComet):
                if (comet == null)
                {
                    currentStage = Stage.followingUpgrade;
                    upgrade.GetComponent<Rigidbody2D>().AddForce(new Vector2(-200f, -200f));
                }
                else
                {
                    FollowingObj(comet.transform.position);
                }
                break;
            case (Stage.followingUpgrade):
                if(upgrade == null)
                {
                    transform.position = new Vector2(-11f, -1.5f);
                    currentStage = Stage.starTap;
                }
                else
                {
                    FollowingObj(upgrade.transform.position);
                }
                break;
            case (Stage.starTap):
                if (Input.GetMouseButtonDown(0))
                {
                    currentStage = Stage.bhArrow;
                    arrow.SetActive(true);
                    Destroy(GetComponent<SpriteRenderer>());
                    currentlyMoving = arrow.transform;
                    minSize = minArrowSize;
                    maxSize = maxArrowSize;
                }
                break;
            case (Stage.bhArrow):
                if (FindObjectsOfType<BlackHole>().Length == 0)
                {
                    if (once)
                    {
                        arrow.SetActive(false);
                        FindObjectOfType<EndLevelFlash>().EndLevel();
                        FindObjectOfType<TutorialCompleted>().TutorialFinished();
                        StartLoadingScene();
                        once = false;
                    }
                }
                break;
        }
        Pulsating();
    }

    void FollowingObj(Vector3 objToFollow)
    {
        transform.position = objToFollow + new Vector3(0f, -2.5f, 0f);
    }

    private void Pulsating()
    {
        if (gettingBigger)
        {
            currentlyMoving.localScale = Vector3.MoveTowards(currentlyMoving.localScale, new Vector3(maxSize, maxSize, maxSize), changeSpeed);
            if (currentlyMoving.localScale.x == maxSize)
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
    }

    void StartLoadingScene()
    {
        FindObjectOfType<LoadScene>().mLoadScene(2);
    }

    Quaternion FaceThePortal(Vector2 myObject)
    {
        if (FindObjectOfType<BlackHole>() == null)
        {
            return Quaternion.identity;
        }
        Vector3 targ = FindObjectOfType<BlackHole>().transform.position;
        Vector3 myPos = myObject;
        targ.z = 0f;
        targ.x = targ.x - myPos.x;
        targ.y = targ.y - myPos.y;
        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, angle - 90f);
    }
}
