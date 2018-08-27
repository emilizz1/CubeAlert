using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShooter : MonoBehaviour
{
    [SerializeField] GameObject[] bullets;
    [SerializeField] Transform spawnLocation;
    [SerializeField] float fireRate;
    [SerializeField] Vector2 throwForce = new Vector2(3f,0f);
    
    Ammo ammo;
    bool shooting = false;
    int side = 1;
    GameObject bullet;
    PolygonCollider2D collider;

    void Start ()
    {
        collider = GetComponentInChildren<SpriteRenderer>().gameObject.AddComponent<PolygonCollider2D>();
        bullet = bullets[Random.Range(0, bullets.Length)];
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
        Figure target = null;
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
        }
    }

    public void StartedShooting(bool isItInner)
    {
        if(shooting == false)
        {
            shooting = true;
            if (isItInner)
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

    public void StartHiding()
    {
        if(transform.position.x < 0)
        {
            side = 1;
        }
        else
        {
            side = -1;
        }
        collider.enabled = false;
    }

    public void StopHiding()
    {
        collider.enabled = true;
    }
}
