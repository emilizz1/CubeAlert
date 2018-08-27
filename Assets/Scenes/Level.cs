using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    int currenLevel = 1;
    Text text;
    
	void Start ()
    {
        text = GetComponent<Text>();
        text.text = " Level " + currenLevel.ToString();
	}
}
