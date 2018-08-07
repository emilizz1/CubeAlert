using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Exploder2D;

public class Figure : MonoBehaviour
{
    bool startedExploding = false;
    bool beenHit = false;
    float rotationSpeed;
    int bulletAmount;
    SpriteRenderer sprite;
    GameObject figureNumber;
    public Exploder2DObject exploder;

    void Start()
    {
        exploder = Exploder2D.Utils.Exploder2DSingleton.Exploder2DInstance;
        rotationSpeed = Random.Range(-40, 40f);
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f), 0.8f);
        bulletAmount = Random.Range(5, 20);
        figureNumber = FindObjectOfType<FigureNumbers>().GetFigureNumber();
    }

    void Update()
    {
        if (!beenHit && gameObject != null)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            UpdateFigureNumber();
        }
        else if(bulletAmount <= 0 && !startedExploding)
        {
            DestroyFigure();
        }
        if (!startedExploding && gameObject!=null)
        {
            UpdateFigureNumber();
        }
    }

    public void DestroyFigure()
    {
        DestroyFigureNumber();
        Exploder2DUtils.SetActive(exploder.gameObject, true);
        exploder.transform.position = Exploder2DUtils.GetCentroid(gameObject);
        exploder.Radius = 1f;
        exploder.Explode();
        startedExploding = true;
    }

    void UpdateFigureNumber()
    {
        if (figureNumber != null)
        {
            figureNumber.transform.position = transform.position;
            figureNumber.transform.rotation = transform.rotation;
            figureNumber.GetComponent<Text>().text = bulletAmount.ToString();
        }
    }

    public void DestroyFigureNumber()
    {
        Destroy(figureNumber);
    }

    public int GetBulletAmount()
    {
        return bulletAmount;
    }

    public void RemoveAmmo()
    {
        beenHit = true;
        bulletAmount--;
    }
}
