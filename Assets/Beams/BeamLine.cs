using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamLine : MonoBehaviour
{
    [SerializeField] GameObject[] topParts;
    [SerializeField] GameObject[] bottomParts;

    bool rotating = true;
    bool shrinking = true;
    
    float rotationSpeed;
    float shrinkingSpeed =0.004f;
    CapsuleCollider2D capsuleCollider;

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rotationSpeed = Random.Range(-40, 40f);
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
            //TODO think of a way for collider to shrink too.
            capsuleCollider.size = new Vector2(capsuleCollider.size.x, ((topParts[0].transform.localPosition.y - 0.15f) * 2f) + 0.45f);
            foreach (GameObject part in bottomParts)
            {
                part.transform.position = Vector3.MoveTowards(part.transform.position, gameObject.transform.localPosition, shrinkingSpeed);
            }
            foreach (GameObject part in topParts)
            {
                part.transform.position = Vector3.MoveTowards(part.transform.position, gameObject.transform.localPosition, shrinkingSpeed);
                if(part.transform.localPosition.y <= 0.15f || part.transform.localPosition.y >= 0.3f)
                {
                    shrinkingSpeed = shrinkingSpeed * -1;
                }
            }
        }
    }

    public void Rotate(bool shouldItRotate)
    {
        rotating = shouldItRotate;
    }
}
