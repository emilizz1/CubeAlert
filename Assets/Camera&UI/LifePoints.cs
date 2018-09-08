using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifePoints : MonoBehaviour
{
    [SerializeField] int startingLife = 3;
    
    int currentLife;
    CameraShaker cameraShaker;
    GameObject lifeNumber;
    Text lifeNumberText;

    void Start()
    {
        cameraShaker = FindObjectOfType<CameraShaker>();
        currentLife = startingLife;
        lifeNumber = FindObjectOfType<FigureNumbers>().GetFigureNumber();
        lifeNumberText = lifeNumber.GetComponent<Text>();
        lifeNumberText.color = Color.white;
    }

    void Update()
    {
        UpdateLife();
    }

    public void AddLife()
    {
        currentLife++;
    }

    public void RemoveLife()
    {
        currentLife--;
        UpdateLife();
        cameraShaker.AddShakeDuration(1f);
        if (currentLife <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    void UpdateLife()
    {
        lifeNumberText.text = currentLife.ToString();
        lifeNumber.transform.position = transform.position;
    }
}
