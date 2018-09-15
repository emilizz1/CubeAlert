using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCometSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] comets;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;
    [SerializeField] float startSpeed;
    [SerializeField] float distanceFromPortalCenter = 7.5f; 

    bool playing = true;
    GameObject myObject;

    void Start()
    {
        StartCoroutine(SpawnComet());
    }

    IEnumerator SpawnComet()
    {
        while (playing)
        {
            yield return new WaitForSecondsRealtime(Random.Range(minSpawnTime, maxSpawnTime));
            GameObject rocketToSpawn = comets[Random.Range(0, comets.Length)];
             myObject = Instantiate(rocketToSpawn, GetCometSpawnPos(), Quaternion.identity, transform);
            LookAwayFromPortal();
            TurnOffCollider();
            myObject.GetComponent<Comet>().DidItPass();
            myObject.GetComponent<Rigidbody2D>().AddForce(myObject.transform.up * startSpeed, ForceMode2D.Impulse);
        }
    }

    Vector2 GetCometSpawnPos()
    {
        return new Vector2(Random.Range(-1f, 1f) + transform.position.x, Random.Range(-1f, 1f) + transform.position.y);
    }

    void LookAwayFromPortal()
    {
        Vector3 targ = transform.position;
        Vector3 myPos = myObject.transform.position;
        targ.z = 0f;
        targ.x = targ.x - myPos.x;
        targ.y = targ.y - myPos.y;
        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        myObject.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
        myObject.GetComponent<Comet>().GiveStartingRotation(Quaternion.Euler(0f, 0f, angle + 90f));
    }

    void TurnOffCollider()
    {
        myObject.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(TurnOnCollider());
    }

    IEnumerator TurnOnCollider()
    {
        while (myObject != null && Vector2.Distance(myObject.transform.position, transform.position) < distanceFromPortalCenter)
        {
            yield return new WaitForSeconds(0.1f);
        }
        if (myObject != null)
        {
            myObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
