using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostCondition : MonoBehaviour
{
    string lostCondition;

    private void Awake()
    {
        int numOfLostConditions = FindObjectsOfType<LostCondition>().Length;
        if (numOfLostConditions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void GiveLostCondition(string condition)
    {
        lostCondition = condition;
    }

    public string GetLostCondition()
    {
        return lostCondition;
    }
}
