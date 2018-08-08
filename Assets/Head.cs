using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform spawnLocation;
    [SerializeField] float fireRate;
    [SerializeField] Vector2 throwForce = new Vector2(3f,0f);

    float startingX;
    LifePoints lifePoints;
    Ammo ammo;
    bool shooting = false;
    bool currentlyInner = false;
    int side = 1;
    Figure target;

    void Start ()
    {
        lifePoints = FindObjectOfType<LifePoints>();
        ammo = FindObjectOfType<Ammo>();
    }

    IEnumerator Shooting()
    {
        while (shooting)
        {
            if (ammo.IsThereAmmo())
            {
                GameObject createdBullet = Instantiate(bullet, spawnLocation.position, Quaternion.identity);
                createdBullet.GetComponent<Rigidbody2D>().AddForce(throwForce * side, ForceMode2D.Impulse);
                Destroy(createdBullet, 12f);
            }
            yield return new WaitForSecondsRealtime(fireRate);
        }
    }

    IEnumerator SeekingShooting()
    {
        while (shooting)
        {
            if (ammo.IsThereAmmo())
            {
                GameObject createdBullet = Instantiate(bullet, spawnLocation.position, Quaternion.identity);
                foreach(Figure figure in FindObjectsOfType<Figure>())
                {
                    if(target == null || Vector3.Distance(figure.transform.position, transform.position) < Vector3.Distance(target.transform.position, transform.position))
                    {
                        target = figure;
                    }
                }
                createdBullet.GetComponent<Bullet>().GetTarget(target.transform);
                Destroy(createdBullet, 12f);
                Vector3 diff = transform.position - target.transform.position;
                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, Quaternion.identity.y, rot_z + 180);
                if (transform.position.x > 0)
                {
                    transform.Rotate(0f, 180f, 180f);
                }
                yield return new WaitForSecondsRealtime(fireRate);
            }
            if (!currentlyInner)
            {
                StartCoroutine(Shooting());
            }
        }
    }

    public void StartedShooting()
    {
        if(shooting == false)
        {
            shooting = true;
            if (currentlyInner)
            {
                StartCoroutine(SeekingShooting());
            }
            else
            {
                StartCoroutine(Shooting());
            }
        }
    }

    public void StopShooting()
    {
        shooting = false;
    }

    public void CheckForChangedSides(float x, bool isItInner)
    {
        float headX = transform.position.x;
        ChangeInnerOuter(isItInner);
        if(headX > 0 && x < 0 || headX < 0 && x > 0)
        {
            ChangeSide();
        }
    }

    void ChangeInnerOuter(bool isItInner)
    {
        if(currentlyInner != isItInner)
        {
            currentlyInner = !currentlyInner;
            if (currentlyInner)
            {
                var headPos = transform.position;
                headPos.x = headPos.x + (5f * side);
                transform.position = headPos;
                var spawnPos = spawnLocation.localPosition;
                spawnPos.x = spawnPos.x - 1.3f;
                spawnLocation.localPosition = spawnPos;
            }
            else
            {
                var headPos = transform.position;
                headPos.x = headPos.x + (-5f * side);
                transform.position = headPos;
                var spawnPos = spawnLocation.localPosition;
                spawnPos.x = spawnPos.x + 1.3f;
                spawnLocation.localPosition = spawnPos;
            }
        }
    }

    void ChangeSide()
    {
        print("hap[p");
        side = -side;
        transform.Rotate(0f, 180f, 0f);
        var pos = transform.position;
        pos.x = -pos.x;
        transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Figure>())
        {
            lifePoints.RemoveLife();
            collision.gameObject.GetComponent<Figure>().DestroyFigure();
        }
    }

    public void GiveStartingX(float startX)
    {
        startingX = startX;
    }
}
