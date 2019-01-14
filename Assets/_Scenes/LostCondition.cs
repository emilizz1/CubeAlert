using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LostCondition : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Text lostConditionText;
    [SerializeField] Image fadeIn;
    [SerializeField] bool puzzleLevel = false;

    bool fullyShowing = false;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (puzzleLevel)
        {
            if(!CheckIfPossibleToWin())
            {
                GiveLostCondition("Out of Stars");
            }
        }
    }

    bool CheckIfPossibleToWin()
    {
        int blackHoleLife = FindObjectOfType<LifePoints>().GetCurrentLifePoints();
        int starsLife = 0;
        foreach(Star star in FindObjectsOfType<Star>())
        {
            starsLife += star.GetBulletAmount();
        }
        return blackHoleLife <= starsLife;
    }

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
        if (!fullyShowing)
        {
            fadeIn.color = new Color(1f, 1f, 1f, 0f);
            while (fadeIn.color.a < 1)
            {
                fadeIn.color = new Color(1f, 1f, 1f, fadeIn.color.a + 0.015f);
                yield return new WaitForSecondsRealtime(0.01f);
            }
            fullyShowing = true;
        }
    }

    public bool GetFullyShowing()
    {
        return fullyShowing;
    }
}
