using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamLine : MonoBehaviour
{
    [SerializeField] GameObject[] topParts;
    [SerializeField] GameObject[] bottomParts;

    bool rotating = true;
    bool shrinking = true;
    bool moving = true;
    
    float rotationSpeed;
    float shrinkSpeed;
    float moveSpeed;
    CapsuleCollider2D capsuleCollider;
    Vector2 startingPos;
    Vector2 targetPos;
    Vector2 currentTarget;

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rotationSpeed = Random.Range(-40, 40f);
        shrinkSpeed = Random.Range(0.003f, 0.004f);
        transform.Rotate(new Vector3(0f, 0f, 45 * Random.Range(0, 3)));
        moveSpeed = Random.Range(0.005f, 0.01f);
    }

    void Update()
    {
        if (rotating)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        if (shrinking)
        {
            Shrink();
        }
        if (moving)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, moveSpeed);
        if (Mathf.Abs(transform.position.x - currentTarget.x) < 0.05f && Mathf.Abs(transform.position.y - currentTarget.y) < 0.05f)
        {
            if (currentTarget == targetPos)
            {
                currentTarget = startingPos;
            }
            else
            {
                currentTarget = targetPos;
            }
        }
    }

    private void Shrink()
    {
        capsuleCollider.size = new Vector2(capsuleCollider.size.x, ((topParts[0].transform.localPosition.y - 0.15f) * 2f) + 0.45f);
        foreach (GameObject part in bottomParts)
        {
            part.transform.position = Vector3.MoveTowards(part.transform.position, gameObject.transform.localPosition, shrinkSpeed);
        }
        foreach (GameObject part in topParts)
        {
            part.transform.position = Vector3.MoveTowards(part.transform.position, gameObject.transform.localPosition, shrinkSpeed);
            if (part.transform.localPosition.y <= 0.15f || part.transform.localPosition.y >= 0.3f)
            {
                shrinkSpeed = shrinkSpeed * -1;
            }
        }
    }

    public void Rotate(bool shouldItRotate)
    {
        rotating = shouldItRotate;
    }

    public void Shrinking(bool shouldItShrink)
    {
        shrinking = shouldItShrink;
    }

    public void Moving(bool shouldItMove, Vector2 target)
    {
        moving = shouldItMove;
        targetPos = target;
        startingPos = transform.position;
        currentTarget = targetPos;
    }
}
