using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    [SerializeField] int damageToLose = 100;
    [SerializeField] bool tutorial = false;

    Image image;
    float maxDamageToLose;

    void Start()
    {
        image = GetComponent<Image>();
        maxDamageToLose = damageToLose;
    }

    void Update()
    {
        CheckIfPossibleToFinish();
    }

    void CheckIfPossibleToFinish()
    {
        UpdateImage();
        if (damageToLose <= 0)
        {
            FindObjectOfType<LostCondition>().GiveLostCondition("Too much damage taken");
        }
    }
   
    void UpdateImage()
    {
        if (!tutorial)
        {
            float fillAmount = 1 - damageToLose / maxDamageToLose;
            image.fillAmount = Mathf.Lerp(0, 1, fillAmount);
            //image.color = Color.Lerp(Color.green, Color.red, image.fillAmount);
        }
    }

    public void DamageDealt(int damage)
    {
        damageToLose -= damage;
        GetComponentInChildren<Text>().text = " Damage +" + damage.ToString();
        Invoke("RemoveAddedText", 1f);
    }

    void RemoveAddedText()
    {
        GetComponentInChildren<Text>().text = " Damage";
    }
}
