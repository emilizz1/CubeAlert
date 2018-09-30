using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelFlash : MonoBehaviour
{
	public void EndLevel()
    {
        GetComponent<CanvasGroup>().alpha = 1f;
    }
}
