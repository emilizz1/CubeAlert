using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LostCondition : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Text lostConditionText;
    [SerializeField] Image fadeIn;

    public void GiveLostCondition(string condition)
    {
        canvas.SetActive(true);
        Time.timeScale = 0f;
        lostConditionText.text = condition;
        StartCoroutine(FadeInBackground());
        foreach(BlackHole bh in FindObjectsOfType<BlackHole>())
        {
            bh.SetAlive(false);
        }
    }

    IEnumerator FadeInBackground()
    {
        fadeIn.color = new Color(1f, 1f, 1f, 0f);
        while (fadeIn.color.a < 1)
        {
            fadeIn.color = new Color(1f, 1f, 1f, fadeIn.color.a + 0.015f);
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }
}
