using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LostConditionText : MonoBehaviour
{
    void Start()
    {
        Text text = GetComponent<Text>();
        text.text = FindObjectOfType<LostCondition>().GetLostCondition();
    }
}
