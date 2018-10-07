using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DreamStarGen.Algorithms;

public class Star : MonoBehaviour
{
    [Range(0.1f, 1f)] [SerializeField] float starRadiusIncrease = 0.3f;
    [SerializeField] float loopingTimer = 1f;
    [SerializeField] GameObject[] centerParticles;

    bool startedExploding = false;
    bool beenHit = false;

    float rotationSpeed;
    int bulletAmount;
    DreamStarGen.DreamStarGenerator star;
    float lastTimeLooped = 0f;
    CircleCollider2D myCollider;

    void Start()
    {
        rotationSpeed = Random.Range(-40, 40f);
        star = GetComponent<DreamStarGen.DreamStarGenerator>();
        myCollider = GetComponent<CircleCollider2D>();
        Instantiate(centerParticles[Random.Range(0, centerParticles.Length)], transform.position, Quaternion.identity, transform);
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
        if (Time.time - lastTimeLooped >= loopingTimer)
        {
            lastTimeLooped = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DestroyFigure(bool quick)
    {
        startedExploding = true;
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(myCollider);
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
        if (myCollider)
        {
            myCollider.radius = star.Radius;
        }
    }
}
