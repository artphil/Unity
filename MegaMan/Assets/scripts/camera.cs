using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

	public Transform player;
	public float smooth = 0.5f;
	private Vector2 velocity;

	// Use this for initialization
	void Start () {
	
		velocity = new Vector2 (0.5f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 newPosition2D = Vector2.zero;

		newPosition2D.x = Mathf.SmoothDamp (transform.position.x, player.position.x, ref velocity.x, smooth);
		newPosition2D.y = Mathf.SmoothDamp (transform.position.y, player.position.y + 1, ref velocity.y, smooth);
       
		Vector3 newPosition = new Vector3 (newPosition2D.x, transform.position.y, transform.position.z);

        if (newPosition.x > 0)
		transform.position = Vector3.Slerp (transform.position, newPosition, Time.time);
	}
}
