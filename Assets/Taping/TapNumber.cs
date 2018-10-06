using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapNumber : MonoBehaviour
{
    [SerializeField] int maxNumberOfTaps = 100;

    Image image;
    float numberOfTaps;

    void Start()
    {
        image = GetComponent<Image>();
        numberOfTaps = maxNumberOfTaps;
        UpdateImage();
        
    }

    public void RemoveATap()
    {
        numberOfTaps--;
        UpdateImage();
        if (numberOfTaps <= 0)
        {
            FindObjectOfType<LoadScene>().mLoadScene(1);
        }
    }

    void UpdateImage()
    {
        float fillAmount = 1 - numberOfTaps / maxNumberOfTaps;
        image.fillAmount = Mathf.Lerp(0, 1, fillAmount);
        image.color = Color.Lerp(Color.green, Color.red , image.fillAmount);
    }
}
