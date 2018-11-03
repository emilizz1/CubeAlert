using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    int extraTime, extraDamage, extraTaps;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    public void tapped()
    {
        GiveBonus();
        Destroy(gameObject);
    }

    void GiveBonus()
    {
        if (extraTime > 0)
        {
            FindObjectOfType<Timer>().AddTime(extraTime);
        }
        else if (extraDamage > 0)
        {
            FindObjectOfType<Ammo>().DamageDealt(-extraDamage);
        }
        else if (extraTaps > 0)
        {
            FindObjectOfType<TapNumber>().AddTaps(extraTaps);
        }
    }

    public void AssignBonuses(int extraTime, int extraDamage, int extraTaps)
    {
        this.extraTime = extraTime;
        this.extraDamage = extraDamage;
        this.extraTaps = extraTaps;
    }
}
