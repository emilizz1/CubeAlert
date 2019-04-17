using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamLine : MonoBehaviour
{
    [SerializeField] GameObject[] topParts;
    [SerializeField] GameObject[] bottomParts;

    bool rotating = false;
    bool shrinking = false;
    bool moving = false;
    
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
        transform.Rotate(new Vector3(0f, 0f, 45 * Random.Range(0, 3)));
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
            if (part.transform.localPosition.y <= 0.135f || part.transform.localPosition.y >= 0.37f)
            {
                shrinkSpeed = shrinkSpeed * -1;
            }
        }
    }

    public void Rotate(bool shouldItRotate)
    {
        rotationSpeed = Random.Range(-40, 40f);
        rotating = shouldItRotate;
    }

    public void Shrinking(bool shouldItShrink)
    {
        shrinkSpeed = Random.Range(0.0055f, 0.0065f);
        shrinking = shouldItShrink;
    }

    public void Moving(bool shouldItMove, Vector2 target)
    {
        moveSpeed = Random.Range(0.025f, 0.045f);
        moving = shouldItMove;
        targetPos = target;
        startingPos = transform.position;
        currentTarget = targetPos;
    }
}
