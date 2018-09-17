using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePoints : MonoBehaviour
{
    [SerializeField] int minLifePoints = 10;
    [SerializeField] int maxLifePoints = 15;
    
    int currentLife;
    int startingLifePoints;
    GameObject lifeNumber;
    Text lifeNumberText;

    void Start()
    {
        startingLifePoints = Random.Range(minLifePoints, maxLifePoints) + 10 * FindObjectOfType<LevelHolder>().currentLevel;
        currentLife = startingLifePoints;
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
        if (currentLife <= 0)
        {
            DestroyPortal();
        }
    }

    void UpdateLife()
    {
        lifeNumberText.text = currentLife.ToString();
        lifeNumber.transform.position = transform.position;
    }

    void DestroyPortal()
    {
        Destroy(lifeNumber); //add some kind animation
        Destroy(gameObject);
    }

    public float GetLifePercentage()
    {
        return currentLife / startingLifePoints;
    }

    public int GetCurrentLifePoints()
    {
        return currentLife;
    }
}
