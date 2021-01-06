using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class pancam : MonoBehaviour {

	private float ydir = 0f;
	public GameObject player;
	public GameObject street;
	private float playerMaxX;

	//for our GUIText object and our score
	public Text gui;
	private float playerScore = 0;

	//this function updates our guitext object
	void Start()
	{
		playerMaxX = player.transform.position.x;
	}
	
	void OnGUI(){
		gui.text = "Score: " + ((int)(playerScore * 10)).ToString ();
	}
	//this is generic function we can call to increase the score by an amount
	public void increaseScore(int amount){
		playerScore += amount;
	}

	// Update is called once per frame
	void Update () {
		//check that player exists and then proceed. otherwise we get an error when player dies
		if (player) {
			//if player has passed the x position of startScroll then start moving camera forward with a randomish Y position
			double startScroll = -5.5;

			Debug.Log("playerMaxX " + playerMaxX);
			if (player.transform.position.x > playerMaxX)
			{
				playerScore += (player.transform.position.x - playerMaxX);
				playerMaxX = player.transform.position.x;
				Debug.Log(playerScore);
				//gui.text = "Score: " + ((int)(playerScore * 10)).ToString ();
			}

			if (player.transform.position.x > startScroll) {
				
				//move up the camera if we are above the 3/4ths mark, move down if we are below the 1/4th mark of the screen
				if (player.transform.position.y > (transform.position.y + transform.position.y + Camera.main.orthographicSize) / 2)
				{
					ydir = Camera.main.orthographicSize / 4;
				}
				else if (player.transform.position.y < (transform.position.y + transform.position.y - Camera.main.orthographicSize) / 2)
				{
					ydir = -Camera.main.orthographicSize / 4;
				}
				else
				{
					ydir = 0;
				}

				//panSpeed is how much we pan in the x-direction every time
				float panSpeed = 1f;

				float newY = transform.position.y + ydir;
				if (newY < street.transform.position.y)
				{
					newY = street.transform.position.y;
				}
				//MoveTowards for smoother camera transitions 
				Vector3 newVec = new Vector3 (player.transform.position.x + 5.5f, newY, transform.position.z);
				float speed = player.GetComponent<Rigidbody2D>().velocity.magnitude;
				float step = Mathf.Abs(speed * Time.deltaTime);
				transform.position = Vector3.MoveTowards(transform.position, newVec, step);
			}
		}
	}

	void onDisable()
	{
		PlayerPrefs.SetInt("Score", (int) playerScore);
	}
}