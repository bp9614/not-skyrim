using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class otherMove : NetworkBehaviour
{
    public float speed;
    public bool invert;
    public int lives;
    public Text liveText;

	// Use this for initialization
	void Start () 
	{
        speed = 1;
        lives = 4;
        invert = false;
        liveText.text = "# of lives: " + lives.ToString();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isLocalPlayer)
			return;

        if (!invert)
        {
            var z = Input.GetAxis("Horizontal") * 0.1f * speed;
            var x = Input.GetAxis("Vertical") * 0.1f * speed;
            transform.Translate(x, 0, -z);
        }
        else
        {
            var z = Input.GetAxis("Horizontal") * 0.1f * speed;
            var x = Input.GetAxis("Vertical") * 0.1f * speed;
            transform.Translate(-x, 0, z);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Power Up"))
        {
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Boulder"))
        {
            lives--;
            liveText.text = "# of lives: " + lives.ToString();
        }
    }
}