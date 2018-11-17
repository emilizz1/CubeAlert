using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMainMenuButton : MonoBehaviour
{
    public void LoadMainMenu()
    {
        FindObjectOfType<LoadScene>().mLoadScene(0);
    }
}
