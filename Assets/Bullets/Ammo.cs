using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ammo : MonoBehaviour
{
    [SerializeField] float levelBulletAmount = 350f;

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
