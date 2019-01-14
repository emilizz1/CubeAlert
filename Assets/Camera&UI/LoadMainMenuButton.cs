using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMainMenuButton : MonoBehaviour
{
    public void LoadMainMenu()
    {
        if (FindObjectOfType<LostCondition>().GetFullyShowing())
        {
            FindObjectOfType<LoadScene>().mLoadScene(0);
        }
    }
}
