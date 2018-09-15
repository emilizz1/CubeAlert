using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DreamStarGen.Algorithms;

public class Portal : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float minDistance;
    
    Vector3 pos;
    LifePoints lifePoints;
    Vector3 targetPos;

    void Start()
    {
        pos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 60f));
        lifePoints = FindObjectOfType<LifePoints>();
        CircleCollider2D circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.radius = 3.2f;
        GetNewTargetPos();
    }

    void Update()
    {
        transform.position =  Vector3.MoveTowards(transform.position, targetPos, moveSpeed);
        if(Vector3.Distance(transform.position, targetPos) < minDistance)
        {
            GetNewTargetPos();
        }
        UpdateSizeFromAmmo();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Figure>())
        {
            var figure = collision.gameObject.GetComponent<Figure>();
            StartCoroutine(AbsorbingFigure(figure));
            figure.DestroyFigure(false);
        }
        else if (collision.gameObject.GetComponent<Comet>())
        {
            lifePoints.RemoveLife();
            collision.gameObject.GetComponent<Comet>().RocketHit();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

    IEnumerator AbsorbingFigure(Figure figure)
    {
        int absorbedBullets = figure.GetBulletAmount();
        while (figure)
        {
            figure.transform.localPosition = Vector2.MoveTowards(figure.transform.localPosition, transform.position, 0.5f);
            if(absorbedBullets > 0)
            {
                lifePoints.RemoveLife();
                absorbedBullets--;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    void UpdateSizeFromAmmo()
    {
        float lifePer = lifePoints.GetLifePercentage();
        float currentSize = 1f + (lifePer * 0.4f);
        transform.localScale = new Vector2(currentSize, currentSize);
    }

    public void GetNewTargetPos()
    {
        float minX = pos.x * 0.3f;
        float maxX = pos.x * -0.7f;
        float minY = pos.y * 0.3f;
        float maxY = pos.y * -0.7f;
        targetPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
}
