using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ammo : MonoBehaviour
{
    [SerializeField] float startingAmmo = 50;
    [SerializeField] float bulletsNeeded = 250;
    [SerializeField] Text currentAmmoText;
    [SerializeField] Text neededAmmoText;

    float currentAmmo;
    Portal portal;
    Level level;

    void Start ()
    {
        portal = FindObjectOfType<Portal>();
        level = FindObjectOfType<Level>();
        currentAmmo = startingAmmo;
        UpdateAmmo();
        AddColors(currentAmmoText);
        AddColors(neededAmmoText);
	}

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        UpdateAmmo();
        CheckForFullAmmo();
    }

    void UpdateAmmo()
    {
        currentAmmoText.text = currentAmmo.ToString();
        neededAmmoText.text = bulletsNeeded.ToString();
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

    void AddColors(Text addingColor)
    {
        switch (Random.Range(0, 5))
        {
            case 0:
                addingColor.color = Color.blue;
                break;
            case 1:
                addingColor.color = Color.cyan;
                break;
            case 2:
                addingColor.color = Color.green;
                break;
            case 3:
                addingColor.color = Color.magenta;
                break;
            case 4:
                addingColor.color = Color.red;
                break;
            case 5:
                addingColor.color = Color.yellow;
                break;
        }
    }

    public float GetAmmoPercantage()
    {
        return currentAmmo / bulletsNeeded;
    }

    void CheckForFullAmmo()
    {
        if(currentAmmo >= bulletsNeeded)
        {
            print("Full Ammo");
        }
    }
}
