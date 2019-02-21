﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMainMenuButton : MonoBehaviour
{
    public void LoadMainMenu()
    {
        FindObjectOfType<LoadScene>().mLoadScene(0);
    }

    public void LoadLostScene()
    {
        print("prepare");
        if (FindObjectOfType<LostCondition>().GetFullyShowing())
        {
            print("loading");
            FindObjectOfType<LoadScene>().mLoadSameScene();
        }
    }
}
