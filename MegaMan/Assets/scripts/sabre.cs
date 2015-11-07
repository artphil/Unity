using UnityEngine;
using System.Collections;

public class sabre : MonoBehaviour {
	
	public Transform player;
	private Vector2 newPosition;

	// Use this for initialization
	void Start () {

		Destroy (gameObject, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		/*
		newPosition = player.position;

		if (player.eulerAngles.y == 0) {
			transform.eulerAngles.y = 180;
			newPosition.x += 0.15f;
		} else {
			transform.eulerAngles.y = 0;
			newPosition.x -= 0.15f;
		}

		transform.position = new Vector3 (newPosition.x, newPosition.y, transform.position.z);
		*/

	}

	void DestroySabre (){

		Destroy (gameObject);
	}
}
