using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelFlash : MonoBehaviour
{
    [SerializeField] float transitionSpeed = 1f;
    [SerializeField] GameObject center;
    [SerializeField] GameObject triangle;
    [SerializeField] float triangleMaxSize;

    bool once = true;

	public void EndLevel()
    {
        if (once)
        {
            StartCoroutine(GrowObject(triangle));
            StartCoroutine(GrowObject(center));
            once = false;
        }
    }

    IEnumerator GrowObject(GameObject myObject)
    {
        myObject.SetActive(true);
        myObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        while(myObject.transform.localScale.x < triangleMaxSize)
        {
            myObject.transform.localScale += new Vector3((Time.deltaTime * transitionSpeed), (Time.deltaTime * transitionSpeed), (Time.deltaTime * transitionSpeed));
            yield return new WaitForEndOfFrame();
        }
    }
}
