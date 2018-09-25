using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DreamStarGen.Algorithms;

public class BlackHole : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float minDistance;
    [SerializeField] DreamStarGen.DreamStarGenerator back;
    
    Vector3 pos;
    CameraShaker cameraShaker;
    LifePoints lifePoints;
    Vector3 targetPos;
    DreamStarGen.DreamStarGenerator blackHole;
    CircleCollider2D circleCollider;
    BlackholeDamageNumber damageNumber;

    void Start()
    {
        blackHole = GetComponent<DreamStarGen.DreamStarGenerator>();
        pos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 60f));
        lifePoints = GetComponent<LifePoints>();
        circleCollider = gameObject.AddComponent<CircleCollider2D>();
        cameraShaker = FindObjectOfType<CameraShaker>();
        damageNumber = FindObjectOfType<BlackholeDamageNumber>();
        GetNewTargetPos();
    }

    void Update()
    {
        transform.position =  Vector3.MoveTowards(transform.position, targetPos, moveSpeed);
        if(Vector3.Distance(transform.position, targetPos) < minDistance)
        {
            GetNewTargetPos();
        }
        UpdateSizeFromLifePoints();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Star>())
        {
            var figure = collision.gameObject.GetComponent<Star>();
            StartCoroutine(AbsorbingFigure(figure));
            figure.DestroyFigure(false);
        }
        else if (collision.gameObject.GetComponent<Comet>())
        {
            GameObject numberInstance = Instantiate(damageNumber.GetNumber(), collision.GetContact(0).point, Quaternion.identity, damageNumber.transform);
            numberInstance.GetComponent<Text>().text = "1";
            lifePoints.RemoveLife(-1);
            cameraShaker.AddShakeDuration(0.2f);
            collision.gameObject.GetComponent<Comet>().RocketHit();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

    IEnumerator AbsorbingFigure(Star figure)
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

    void UpdateSizeFromLifePoints()
    {
        float currentSize = 0.85f + (lifePoints.GetCurrentLifePoints() * 0.04f);
        circleCollider.radius = 1.35f + (lifePoints.GetCurrentLifePoints() * 0.025f);
        blackHole.Width = currentSize;
        back.Width = currentSize + 0.5f;
        blackHole._GenerateStar();
        back._GenerateStar();
    }

    void GetNewTargetPos()
    {
        float minX = pos.x * 0.3f;
        float maxX = pos.x * -0.7f;
        float minY = pos.y * 0.3f;
        float maxY = pos.y * -0.7f;
        targetPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
}
