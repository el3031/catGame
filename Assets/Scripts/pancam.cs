using UnityEngine;
using System.Collections;

public class pancam : MonoBehaviour {

	float ydir = 0f;
	public GameObject player;

	// Use this for initialization

	// Update is called once per frame
	void Update () {
		//check that player exists and then proceed. otherwise we get an error when player dies
		if (player) {
			//if player has passed the x position of startScroll then start moving camera forward with a randomish Y position
			double startScroll = -5.5;
			if (player.transform.position.x > startScroll) {

				float randy = 0f;
				randy = Random.Range (0f, 100f);
				if (randy < 20) {
					ydir = ydir + .005f;
				} else if (randy > 20 && randy < 40) {
					ydir = ydir - .005f;
				} else if (randy > 80) {
					ydir = 0f;
				}

				//panSpeed is how much we pan in the x-direction every time
				float panSpeed = 0.01f;
				transform.position = new Vector3 (transform.position.x + panSpeed, transform.position.y + ydir, -10);
			}
		}
	}
}