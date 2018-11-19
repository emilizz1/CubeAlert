using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelFlash : MonoBehaviour
{
    [SerializeField] float transitionSpeed = 1f;
    [SerializeField] GameObject triangle;
    [SerializeField] float triangleMaxSize;
    [SerializeField] float cameraZoomSpeed = 1f;

    bool once = true;

	public void EndLevel()
    {
        if (once)
        {
            StartCoroutine(TriangleGrowing());
            once = false;
        }
    }

    IEnumerator TriangleGrowing()
    {
        var camera = Camera.main;
        triangle.SetActive(true);
        triangle.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        while(triangle.transform.localScale.x < triangleMaxSize)
        {
            //camera.fieldOfView -= Time.deltaTime * cameraZoomSpeed;
            triangle.transform.localScale += new Vector3((Time.deltaTime * transitionSpeed), (Time.deltaTime * transitionSpeed), (Time.deltaTime * transitionSpeed));
            yield return new WaitForEndOfFrame();
        }
    }
}
