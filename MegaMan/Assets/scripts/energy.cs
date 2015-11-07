using UnityEngine;
using System.Collections;

public class energy : MonoBehaviour {

    private float timeLife;
    public float MaxTimeLife;
    public float velocity;

    // Use this for initialization
    void Start () {

        timeLife = 0;
	}
	
	// Update is called once per frame
	void Update () {

        timeLife += Time.deltaTime;
        if (timeLife > MaxTimeLife) {

            Destroy(gameObject);
        }

        transform.Translate(Vector2.right * velocity * Time.deltaTime);

    }
    // Destroi objeto ao tocar inimigo
	void OnCollisionEnter2D (Collision2D colisor) {

        if (colisor.gameObject.tag == "Enimie") {

            Destroy(gameObject);
        }
	}
}
