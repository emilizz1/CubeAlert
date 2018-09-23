using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapNumber : MonoBehaviour
{
    [SerializeField] int numberOfTaps = 100;

    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        AddColor();
        UpdateText();
    }

    public void RemoveATap()
    {
        numberOfTaps--;
        UpdateText();
        if (numberOfTaps <= 0)
        {
            print("Out of Taps");
            FindObjectOfType<LoadScene>().mLoadScene(1);
        }
    }

    void UpdateText()
    {
        text.text = numberOfTaps.ToString();
    }

    void AddColor()
    {
        switch (Random.Range(0, 5))
        {
            case (0):
                text.color = Color.blue;
                break;
            case (1):
                text.color = Color.cyan;
                break;
            case (2):
                text.color = Color.green;
                break;
            case (3):
                text.color = Color.magenta;
                break;
            case (4):
                text.color = Color.red;
                break;
            case (5):
                text.color = Color.yellow;
                break;
        }
    }
}
