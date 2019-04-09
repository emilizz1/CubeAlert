using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LostCondition : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Text lostConditionText;
    [SerializeField] Image fadeIn;
    [SerializeField] Text levelsCompleted;
    [SerializeField] Text score;

    bool fullyShowing = false;

    private void Awake()
    {
        Time.timeScale = 1f;
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
        Time.timeScale = 0.01f;
        lostConditionText.text = condition;
        levelsCompleted.text = "Levels completed: " + FindObjectOfType<ScoreCounter>().GetLevelsCompleted().ToString();
        score.text = "Score: " + FindObjectOfType<ScoreCounter>().GetScore().ToString();
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
            fadeIn.color = new Color(0f, 0f, 0f, 0f);
            while (fadeIn.color.a < 0.75f)
            {
                fadeIn.color = new Color(0f, 0f, 0f, fadeIn.color.a + 0.015f);
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
