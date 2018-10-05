using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelFlash : MonoBehaviour
{
    [SerializeField] float transitionSpeed = 1f;

	public void EndLevel()
    {
        GetComponent<CanvasGroup>().alpha += Time.deltaTime * transitionSpeed;
    }
}
