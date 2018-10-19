using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostCondition : MonoBehaviour
{
    string lostCondition;

    private void Awake()
    {
        if(FindObjectsOfType<LostCondition>().Length > 0)
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
