using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePoints : MonoBehaviour
{
    [SerializeField] int minLifePoints = 10;
    [SerializeField] int maxLifePoints = 15;
    
    int currentLife;
    GameObject lifeNumber;
    Text lifeNumberText;

    void Start()
    {
        currentLife = Random.Range(minLifePoints, maxLifePoints) + 10 * FindObjectOfType<LevelHolder>().currentLevel;
        lifeNumber = FindObjectOfType<BlackholeNumber>().GetNumber();
        lifeNumberText = lifeNumber.GetComponent<Text>();
        lifeNumberText.color = Color.white;
    }

    void Update()
    {
        UpdateLife();
    }

    public void RemoveLife(int amount = 1)
    {
        currentLife -= amount;
        UpdateLife();
        if (currentLife <= 0)
        {
            DestroyPortal();
        }
    }

    public int GetCurrentLifePoints()
    {
        return currentLife;
    }

    void UpdateLife()
    {
        lifeNumberText.text = currentLife.ToString();
        lifeNumber.transform.position = transform.position;
    }

    void DestroyPortal()
    {
        Destroy(lifeNumber);
        Destroy(gameObject);
    }
}
