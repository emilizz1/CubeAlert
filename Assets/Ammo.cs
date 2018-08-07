using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ammo : MonoBehaviour
{
    [SerializeField] float startingAmmo = 50;
    [SerializeField] float BulletsNeeded = 250;

    float currentAmmo;
    Text text;
    Scrollbar scrollbar;

    void Start ()
    {

        scrollbar = FindObjectOfType<Scrollbar>();
        text = GetComponent<Text>();
        currentAmmo = startingAmmo;
        UpdateAmmo();
	}

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        UpdateAmmo();
    }

    void UpdateAmmo()
    {
        text.text = currentAmmo.ToString() + "/" + BulletsNeeded.ToString();
        UpdateProgressBar();
    }
    
    public bool IsThereAmmo()
    {
        if(currentAmmo >0)
        {
            currentAmmo -= 1;
            UpdateAmmo();
            return true;
        }
        else
        {
            return false;
        }
    }

    void UpdateProgressBar()
    {
        scrollbar.size = currentAmmo / BulletsNeeded;
        if (scrollbar.size == 1)
        {
            SceneManager.LoadScene(2);
        }
    }
}
