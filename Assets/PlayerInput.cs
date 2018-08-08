using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Head head;
    Vector3 pos;

	void Start ()
    {
        head = FindObjectOfType<Head>();
        pos = Camera.main.ViewportToWorldPoint(new Vector3(0f,0f,60f));
        var headPos = head.transform.position;
        headPos.x = pos.x - 2.5f;
        head.transform.position = headPos;
        head.GiveStartingX(head.transform.position.x);
	}
	
	void Update ()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            var touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 60f));
            if (touchPos.x > (pos.x * 7) / 10 && touchPos.x < (-pos.x * 7) / 10)
            {
                head.CheckForChangedSides(touchPos.x, true);
            }
            else
            {
                head.CheckForChangedSides(touchPos.x, false);
            }
            if(touchPos.y > -23)
            {
                var headPos = head.transform.position;
                headPos.y = touchPos.y;
                head.transform.position = headPos;
            }
            head.StartedShooting();
        }
        else
        {
            head.StopShooting();
        }

        if (Input.GetMouseButtonDown(0))
        {
            var touchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 60f));
            if (touchPos.x > pos.x / 2 && touchPos.x < -pos.x / 2)
            {
                head.CheckForChangedSides(touchPos.x, true);
            }
            else
            {
                head.CheckForChangedSides(touchPos.x, false);
            }
            head.StartedShooting();
        }
    }
}
