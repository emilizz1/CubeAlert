using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    HeadShooter head;

	void Start ()
    {
        head = FindObjectOfType<HeadShooter>(); 
	}

    void Update()
    {
        //if(Input.touchCount > 0)
        //{
        //    Touch touch = Input.touches[0];
        //    var touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 60f));
        //    var headPos = transform.position;
        //    var headRot = transform.rotation;
        //    if (touchPos.y > -23 && touchPos.x > pos.x * 0.7 && touchPos.x < -pos.x * 0.7)
        //    {
        //        headPos.x = touchPos.x;
        //        headPos.y = touchPos.y;
        //        transform.position = headPos;
        //        head.StopHiding();
        //        head.StartedShooting(true);
        //    }
        //    else if (touchPos.x > pos.x * 0.7 )
        //    {
        //        head.StartHiding();
        //        headPos.x = -headStartinhX;
        //        headPos.y = touchPos.y;
        //        headRot = Quaternion.Euler(0f, 0f, 0f);
        //        transform.position = headPos;
        //        transform.rotation = headRot;
        //        head.StartedShooting(false);
        //    }
        //    else if(touchPos.x < -pos.x * 0.7)
        //    {
        //        head.StartHiding();
        //        headPos.x = headStartinhX;
        //        headPos.y = touchPos.y;
        //        headRot = Quaternion.Euler(0f, 0f, 180f);
        //        transform.position = headPos;
        //        transform.rotation = headRot;
        //        head.StartedShooting(false);
        //    }
        //    else
        //    {
        //        head.StartedShooting(false);
        //    }
        //}
        //else
        //{
        //    head.StopShooting();
        //}

        if (Input.GetMouseButtonDown(0))
        {
            var touchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 60f));
            var headPos = head.transform.position;
            headPos.x = touchPos.x;
            headPos.y = touchPos.y;
            head.transform.position = headPos;
            head.Explode();
        }
    }
}
