using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Figure : MonoBehaviour
{
    bool startedExploding = false;
    bool beenHit = false;
    float rotationSpeed;
    int bulletAmount;
    GameObject figureNumber;

    void Start()
    {
        rotationSpeed = Random.Range(-40, 40f);
        bulletAmount = Random.Range(3, 15);
        figureNumber = FindObjectOfType<FigureNumbers>().GetFigureNumber();
    }

    void Update()
    {
        if (!beenHit && gameObject != null)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            UpdateFigureNumber();
        }
        else if (bulletAmount <= 0 && !startedExploding)
        {
            DestroyFigure(true);
        }
        if (!startedExploding && gameObject != null)
        {
            UpdateFigureNumber();
        }
    }

    public void DestroyFigure(bool quick)
    {
        DestroyFigureNumber();
        startedExploding = true;
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<CircleCollider2D>());
        if (quick)
        {
            StartCoroutine(ShrinkingStar(0.1f));
        }
        else
        {
            StartCoroutine(ShrinkingStar(0.01f));
        }
    }

    IEnumerator ShrinkingStar(float shrinkingSpeed)
    {
        while (startedExploding)
        {
            transform.localScale = Vector2.MoveTowards(transform.localScale, new Vector2(0.01f, 0.01f), shrinkingSpeed);
            if(transform.localScale.x < 0.05f || transform.localScale.y < 0.05f)
            {
                Destroy(gameObject);
            }
            yield return new WaitForFixedUpdate();
        }
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
