using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    [SerializeField] int levelBulletAmount = 350;
    [SerializeField] bool tutorial = false;

    Image image;
    float maxPortalAmmo = 0;

    void Start()
    {
        levelBulletAmount += 10 * FindObjectOfType<LevelHolder>().currentLevel;
        image = GetComponent<Image>();
    }

    void Update()
    {
        CheckIfPossibleToFinish();
    }

    void CheckIfPossibleToFinish()
    {
        int currentlyNeeded = 0;
        int currentlyHaving = levelBulletAmount;
        foreach(LifePoints portal in FindObjectsOfType<LifePoints>())
        {
            currentlyNeeded += portal.GetCurrentLifePoints();
        }
        foreach(Star figure in FindObjectsOfType<Star>())
        {
            currentlyHaving += figure.GetBulletAmount();
        }
        UpdateImage(currentlyNeeded);
        if (currentlyHaving < currentlyNeeded)
        {
            FindObjectOfType<LostCondition>().GiveLostCondition("Out of Stardust");
            FindObjectOfType<LoadScene>().mLoadScene(2);
        }
    }

    public bool IsThereLevelAmmo(int amount)
    {
        if(levelBulletAmount - amount >= 0)
        {
            levelBulletAmount -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
   
    void UpdateImage(float needed)
    {
        if (!tutorial)
        {
            float fillAmount = 1 - needed / maxPortalAmmo;
            image.fillAmount = Mathf.Lerp(0, 1, fillAmount);
            image.color = Color.Lerp(Color.red, Color.green, image.fillAmount);
        }
    }

    public void AddMaxPortalAmmo(int amount)
    {
        maxPortalAmmo += amount;
    }
}
