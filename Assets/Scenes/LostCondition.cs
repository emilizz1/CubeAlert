using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LostCondition : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Text lostConditionText;

    public void GiveLostCondition(string condition)
    {
        canvas.SetActive(true);
        Time.timeScale = 0f;
        lostConditionText.text = condition;

        foreach(BlackHole bh in FindObjectsOfType<BlackHole>())
        {
            bh.SetAlive(false);
        }
    }
}
