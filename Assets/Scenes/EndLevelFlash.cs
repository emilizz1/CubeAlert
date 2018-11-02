using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelFlash : MonoBehaviour
{
    [SerializeField] float transitionSpeed = 1f;
    [SerializeField] GameObject triangle;
    [SerializeField] float triangleMaxSize;
    [SerializeField] GameObject whiteCanvas;

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
        triangle.SetActive(true);
        triangle.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        while(triangle.transform.localScale.x < triangleMaxSize)
        {
            triangle.transform.localScale += new Vector3((Time.deltaTime * transitionSpeed), (Time.deltaTime * transitionSpeed), (Time.deltaTime * transitionSpeed));
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(CanvasApearring());
    }

    IEnumerator CanvasApearring()
    {
        whiteCanvas.SetActive(true);
        whiteCanvas.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        while(whiteCanvas.GetComponent<Image>().color.a < 1)
        {
            whiteCanvas.GetComponent<Image>().color = new Color(1f, 1f, 1f, whiteCanvas.GetComponent<Image>().color.a + Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
