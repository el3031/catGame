using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class pancam : MonoBehaviour {

	float ydir = 0f;
	public GameObject player;

	//for our GUIText object and our score
	public Text gui;
	float playerScore = 0;

	//this function updates our guitext object
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
			if (player.transform.position.x > startScroll) {

				if (player.transform.position.y < Camera.main.orthographicSize / 2)
				{
					ydir = -Camera.main.orthographicSize / 4;
				}
				else if (player.transform.position.y > Camera.main.orthographicSize * 3 / 2)
				{
					ydir = Camera.main.orthographicSize / 4;
				}
				else
				{
					ydir = 0;
				}

				//panSpeed is how much we pan in the x-direction every time
				float panSpeed = 0.01f;
				transform.position = new Vector3 (transform.position.x + panSpeed, transform.position.y + ydir, -10);
			}
		}
	}
}