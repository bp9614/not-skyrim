using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GodActions : NetworkBehaviour {

    public int speed;
    public GameObject hazard;
	public Camera theCam;

    // Update is called once per frame
    void Update() 
	{
        
        if (Input.touchCount != 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                Vector3 touchPostion = theCam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, transform.position.y));
                NetworkServer.Spawn((GameObject)Instantiate(hazard, new Vector3(touchPostion.x, 10, touchPostion.z), transform.rotation));
            }
        }

//		if (Input.GetMouseButtonDown(0)) {
//			Vector2 touch = Input.mousePosition;
//			Vector3 touchPostion = theCam.ScreenToWorldPoint(new Vector3(touch.x, touch.y, transform.position.y));
//			NetworkServer.Spawn((GameObject)Instantiate(hazard, new Vector3(touchPostion.x, 10, touchPostion.z), transform.rotation));
			
//		}
    }
}
