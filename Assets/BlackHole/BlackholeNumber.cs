using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackholeNumber : MonoBehaviour
{
    [SerializeField] GameObject text;

    public GameObject GetNumber()
    {
        return Instantiate(text, new Vector3(100f,100f,100f), Quaternion.identity, transform);
    }
}
