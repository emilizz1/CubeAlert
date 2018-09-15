using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DreamStarGen.Algorithms;

public class Figure : MonoBehaviour
{
    [Range(0.1f, 1f)] [SerializeField] float starRadiusIncrease = 0.3f;
    [SerializeField] float loopingTimer = 1f;

    bool startedExploding = false;
    bool beenHit = false;
    float rotationSpeed;
    int bulletAmount;
    DreamStarGen.DreamStarGenerator star;
    float lastTimeLooped = 0f;
    CircleCollider2D collider;

    void Start()
    {
        rotationSpeed = Random.Range(-40, 40f);
        star=GetComponent<DreamStarGen.DreamStarGenerator>();
        collider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (!beenHit && gameObject != null)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            UpdateFigureLifes();
        }
        else if (bulletAmount <= 0 && !startedExploding)
        {
            DestroyFigure(true);
        }
        if (!startedExploding && gameObject != null)
        {
            UpdateFigureLifes();
        }
    }

    public void DestroyFigure(bool quick)
    {
        startedExploding = true;
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(collider);
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

    void UpdateFigureLifes()
    {
        star.Radius = (bulletAmount * starRadiusIncrease) + starRadiusIncrease;
        star.Width = star.Radius;
        if (collider)
        {
            collider.radius = star.Radius;
        }
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

    public void GiveBulletsAmount(int bullets)
    {
        bulletAmount = bullets;
    }

    public bool ShouldItLoop()
    {
        if(Time.time - lastTimeLooped >= loopingTimer)
        {
            lastTimeLooped = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }
}
