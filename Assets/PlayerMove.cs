using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerMove : NetworkBehaviour {

	GameObject theCamera;

	[SyncVar]
	public int moveSpeed;

	[SyncVar]
	public int invert;

	public float rotateSpeed;

	public string status;

	public int lives;

	GameObject health;
	GameObject death;
	GameObject victory;

	// Use this for initialization
	void Start () 
	{

		lives = 4;

		moveSpeed = 5;
		invert = 1;
		rotateSpeed = 50.0f;
		health = GameObject.Find ("health");

		health.transform.position = new Vector3(transform.position.x - 0.34f, transform.position.y + 0.61f, transform.position.z);
		health.transform.parent = transform;

		health.GetComponent<TextMesh>().text = lives.ToString();
		theCamera = GameObject.Find ("Main Camera");
		if (!isLocalPlayer) {
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
		} else 
		{
			float hangBack = 3.0f;
			theCamera.transform.position = new Vector3 (transform.position.x - Vector3.forward.x*hangBack, transform.position.y + 1.0f, transform.position.z - Vector3.forward.z * hangBack);
			theCamera.transform.forward = transform.forward;
			theCamera.transform.parent = transform;
		}
			
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.gameObject.tag == "Finish") 
		{
			print ("Hello");
			Application.LoadLevel (5);
		}else if (collision.collider.gameObject.tag == "Boulder") {

			lives = lives - 1;

			if (lives == 0) {
				Application.LoadLevel (4);
			}


		} else if (collision.collider.gameObject.tag == "Ground" && status == "jumping") {
			status = "nothing";

		}


	}

	// Update is called once per frame
	void Update () 
	{
		if (!isLocalPlayer)
			return;
		
		health.GetComponent<TextMesh>().text = lives.ToString();
		print (lives);

		var x = Input.GetAxis ("Horizontal") * 0.1f;
		var z = Input.GetAxis ("Vertical") * 0.1f;


		if (Input.GetKey ("right"))
			transform.Rotate (0.0f, Time.deltaTime * rotateSpeed *invert, 0.0f);
		else if (Input.GetKey ("left"))
			transform.Rotate (0.0f, -1.0f * Time.deltaTime * rotateSpeed*invert, 0.0f);

		if (Input.GetKey ("up"))
			transform.Translate (Vector3.forward * Time.deltaTime * moveSpeed*invert);
		else if (Input.GetKey ("down"))
			transform.Translate (Vector3.forward * Time.deltaTime * moveSpeed * -1.0f*invert);


		
	}

	void FixedUpdate()
	{
		if (!isLocalPlayer)
			return;

		if (Input.GetKey ("space") && status != "jumping") 
		{
			GetComponent<Rigidbody>().AddForce (Vector3.up*300.0f);
			status = "jumping";
		}
			
	}
}
