using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class whichGodPower : NetworkBehaviour {

	public PlayerMove pMove;

	public Text  dbTimer;
	public Text invTimer;
	public Text slowTimer;
	public Text fourTimer;

	public float speedReduce;

	public string currentPower;
	public string newPower;

	public float boulderCD;
	public float invertCD;
	public float slowCD;

	public float boulderTime;
	public float invertTime;
	public float slowTime;

	public float slowEffect;
	public float invertEffect;

	public float slowEnd;
	public float invertEnd;

	public GameObject hazard;
	public Camera theCam;

	public GameObject[] players;

	public string currentEffect;

	// Use this for initialization
	void Start () 
	{
		fourTimer.enabled = false;
		speedReduce = 0.25f;
		currentPower = "none";
		boulderCD = 1.0f;
		invertCD = 10.0f;
		slowCD = 10.0f;

		slowEffect = 10.0f;
		invertEffect = 10.0f;

	}

	// Update is called once per frame
	void Update () 
	{
		if (boulderTime - Time.time > 0) {
			dbTimer.enabled = true;
			dbTimer.text = System.Convert.ToInt32(boulderTime - Time.time).ToString();
		} else {
			dbTimer.enabled = false;
		}
			
		if (invertTime - Time.time > 0) {
			invTimer.enabled = true;
			invTimer.text = System.Convert.ToInt32(invertTime - Time.time).ToString();
		} else {
			invTimer.enabled = false;
		}

		if (slowTime - Time.time > 0) {
			slowTimer.enabled = true;
			slowTimer.text = System.Convert.ToInt32(slowTime - Time.time).ToString();
		} else {
			slowTimer.enabled = false;
		}



		players = GameObject.FindGameObjectsWithTag ("Player");

		if (Input.GetMouseButtonDown (0)) 
		{
			if (EventSystem.current.currentSelectedGameObject != null) 
			{
				
				newPower = EventSystem.current.currentSelectedGameObject.name;
				print (newPower);
				if (newPower == currentPower) 
				{
					currentPower = "none";
				} else 
				{
					currentPower = newPower;
				}

			} else if (currentPower != "none") 
			{

				if (currentPower == "Drop Boulder" && Time.time >= boulderTime) 
				{
					Touch touch = Input.GetTouch(0);
					if (touch.phase == TouchPhase.Began) 
					{
						Vector3 touchPostion = theCam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, transform.position.y));
						NetworkServer.Spawn((GameObject)Instantiate(hazard, new Vector3(touchPostion.x, 10, touchPostion.z), transform.rotation));
					}

					boulderTime = Time.time + boulderCD;


				} else if (currentPower == "Slow player" && Time.time >= slowTime) 
				{
					slowTime = Time.time + slowCD;
					for (int i = 0; i < players.Length; i++) 
					{
						pMove = players [i].GetComponent<PlayerMove> ();
						pMove.moveSpeed = 2;
					}
					currentEffect = "slow";
					slowEnd = Time.time + slowEffect;

				} else if (currentPower == "Invert Keys" && Time.time >= invertTime) 
				{
					invertTime = Time.time + invertCD;
					for (int i = 0; i < players.Length; i++) 
					{
						pMove = players [i].GetComponent<PlayerMove> ();
						pMove.invert = -1;
					}
					currentEffect = "invert";
					invertEnd = Time.time + invertEffect;
				}

			}

		}
			
			
		if (currentEffect != "none") 
		{

			if (currentEffect == "invert" && Time.time >= invertEnd) 
			{
				for (int i = 0; i < players.Length; i++) 
				{
					pMove = players [i].GetComponent<PlayerMove> ();
					pMove.invert = 1;
				}

				currentEffect = "none";
			} else if (currentEffect == "slow" && Time.time >= slowEnd) 
			{
				for (int i = 0; i < players.Length; i++) 
				{
					pMove = players [i].GetComponent<PlayerMove> ();
					pMove.moveSpeed = 10;
				}
				currentEffect = "none";
			}

		}




	}
}
