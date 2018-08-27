using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifePoints : MonoBehaviour
{
    [SerializeField] int startingLife = 3;
    [SerializeField] Image[] hearts;
    
    int currentLife;
    CameraShaker cameraShaker;

    void Start()
    {
        cameraShaker = FindObjectOfType<CameraShaker>();
        currentLife = startingLife;
        UpdateHearts();
    }

    public void AddLife()
    {
        currentLife++;
        UpdateHearts();
    }

    public void RemoveLife()
    {
        currentLife--;
        UpdateHearts();
        cameraShaker.AddShakeDuration(1f);
        if (currentLife <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < currentLife)
            {
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }
}
