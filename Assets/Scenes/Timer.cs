using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool playing = true;

    [SerializeField] float playingTime;
    [SerializeField] float levelModificator;

    float currentTime;
    Image image;

    void Start()
    {
        playingTime -= levelModificator * FindObjectOfType<LevelHolder>().currentLevel;
        currentTime = 0f;
        image = GetComponent<Image>();
        image.fillAmount = 0f;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        float fillingNeeded = currentTime / playingTime;
        image.fillAmount = Mathf.Lerp(0, 1, fillingNeeded);

        if (currentTime >= playingTime && playing)
        {
            FindObjectOfType<LoadScene>().mLoadScene(1);
        }
    }
}
