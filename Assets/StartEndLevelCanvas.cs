using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartEndLevelCanvas : MonoBehaviour {
    
    [SerializeField] GameObject whiteCanvas;

    public IEnumerator CanvasApearring(int scene)
    {
        whiteCanvas.SetActive(true);
        while (whiteCanvas.GetComponent<Image>().color.a < 1)
        {
            whiteCanvas.GetComponent<Image>().color = new Color(1f, 1f, 1f, whiteCanvas.GetComponent<Image>().color.a + Time.unscaledDeltaTime);
            print("before end of frame");
            yield return new WaitForEndOfFrame();
            print("after end of frame");
        }
        print("before realtime");
        yield return new WaitForSecondsRealtime(0.1f);
        print("after realtime");
        SceneManager.LoadScene(scene);
    }

    public IEnumerator CanvasDisapearring()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        while (whiteCanvas.GetComponent<Image>().color.a > 0)
        {
            whiteCanvas.GetComponent<Image>().color = new Color(1f, 1f, 1f, whiteCanvas.GetComponent<Image>().color.a - Time.unscaledDeltaTime);
            yield return new WaitForEndOfFrame();
        }
        whiteCanvas.SetActive(false);
    }
}
