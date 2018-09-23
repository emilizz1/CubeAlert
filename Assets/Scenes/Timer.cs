using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool playing = true;

    [SerializeField] float currentTime;
    [SerializeField] float levelModificator;

    Text timerText;

	void Start ()
    {
        currentTime -= levelModificator * FindObjectOfType<LevelHolder>().currentLevel;
        timerText = GetComponent<Text>();
        AddColor();
        timerText.text = currentTime.ToString();
        StartCoroutine(Countdown());
	}

    IEnumerator Countdown()
    {
        while (currentTime > 0 && playing)
        {
            yield return new WaitForSecondsRealtime(1f);
            currentTime--;
            timerText.text = currentTime.ToString();
        }
        if (playing)
        {
            FindObjectOfType<LoadScene>().mLoadScene(1);
        }
    }

    void AddColor()
    {
        switch (Random.Range(0, 5))
        {
            case (0):
                timerText.color = Color.blue;
                break;
            case (1):
                timerText.color = Color.cyan;
                break;
            case (2):
                timerText.color = Color.green;
                break;
            case (3):
                timerText.color = Color.magenta;
                break;
            case (4):
                timerText.color = Color.red;
                break;
            case (5):
                timerText.color = Color.yellow;
                break;
        }
    }
}
