using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectNumber : MonoBehaviour
{
    [SerializeField] GameObject text;

    public GameObject GetFigureNumber()
    {
        return Instantiate(text, new Vector3(100f,100f,100f), Quaternion.identity, transform);
    }
}
