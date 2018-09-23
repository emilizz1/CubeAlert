﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int levelBulletAmount = 350;

    void Start()
    {
        levelBulletAmount += 10 * FindObjectOfType<LevelHolder>().currentLevel;
    }

    void Update()
    {
        CheckIfPossibleToFinish();
    }

    void CheckIfPossibleToFinish()
    {
        int currentlyNeeded = 0;
        int currentlyHaving = levelBulletAmount;
        foreach(LifePoints portal in FindObjectsOfType<LifePoints>())
        {
            currentlyNeeded += portal.GetCurrentLifePoints();
        }
        foreach(Star figure in FindObjectsOfType<Star>())
        {
            currentlyHaving += figure.GetBulletAmount();
        }
        if(currentlyHaving < currentlyNeeded)
        {
            print("Cant fill all the blackholes");
            FindObjectOfType<LoadScene>().mLoadScene(1);
        }
    }

    public bool IsThereLevelAmmo(int amount)
    {
        if(levelBulletAmount - amount >= 0)
        {
            levelBulletAmount -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
}
