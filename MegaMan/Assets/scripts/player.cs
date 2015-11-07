using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

    //Movimento
	public  float       velocity;           
	public  float       enterTime = 0.4f;   //Tempo de duração do efeito de entrada

    //Animação
	public  Transform   character;          //Personagem jogavel
	private Animator    anime;              //Animação do personagem

    //Pulo
	private bool        grounded;           //Verifica de está na terra
    public  float       force;              //Força do pulo
	public  float       jumpTime;           //Tempo do pulo
	public  float       jumpDelay = 0.7f;   //Tempo da animação
	public  Transform   ground;             //Obj abaixo do personagem

    // Disparar bola de energia
	public  GameObject[]  shotgun;            // Obj energia
    private Vector2     shotposition;       // Posição do disparo
	private bool shotting;
	private bool flaming;
	private float shotTime = 0;
	public GameObject sabre;


	// Use this for initialization
	void Start () {
	
        // Carrega a animação
		anime = character.GetComponent<Animator> ();
    }
	
	// Update is called once per frame
	void Update () {

        // Verifica tempo da animação de entrada
		if (enterTime > 0) {
		
			enterTime -= Time.deltaTime;
			anime.SetFloat ("enter", enterTime);
		}
        
        // Controla movimento
		Move ();

		// Controla o disparo
		Shoting ();
        	
	}

    // Controla caminhada e pulo
	void Move () {

        // Verifica se player estrá no chão
		grounded = Physics2D.Linecast (this.transform.position, ground.position, 1 << LayerMask.NameToLayer ("Cenary"));
        // Verifica se está correndo
        anime.SetFloat ("run", Mathf.Abs (Input.GetAxis ("Horizontal")));
        // Verifica se está caindo
		anime.SetFloat ("force", Mathf.Abs (this.GetComponent<Rigidbody2D>().velocity.y));

        // Verifica se seta direita é pressionada
		if (Input.GetAxisRaw ("Horizontal") > 0) {

			transform.Translate (Vector2.right * velocity * Time.deltaTime);
			transform.eulerAngles = new Vector2(0,0);
        }

        // Verifica se seta esquerda é pressionada
        if (Input.GetAxisRaw ("Horizontal") < 0) {
			
			transform.Translate (Vector2.right * velocity * Time.deltaTime);
			transform.eulerAngles = new Vector2 (0,180);
        }

        // Verifica se espaço é pressionado
        if (Input.GetButtonDown ("Jump") && grounded) {

			GetComponent<Rigidbody2D>().AddForce (force * transform.up);
			anime.SetTrigger ("jump");
			jumpTime = jumpDelay;
		}

        // Verifica se voltou para o chão
		if (grounded) {
		
			anime.SetTrigger ("ground");
		}
		
        // Verifica o tempo de pulo
		if (jumpTime > -0.1)
			jumpTime -= Time.deltaTime;

	}

	void OnCollisionEnter2D (Collision2D colisor) {
		
		if (colisor.gameObject.tag == "Enimie") {

			// Destroi personagem
			Destroy(gameObject);
			/*
			// reinicia posiçao
			transform.position = new Vector3 (0f, -0.9f, 0f);
			transform.eulerAngles = new Vector2(0,0);

			//Cria novo personagem
			Instantiate(gameObject, transform.position, transform.rotation);
			*/
		}
	}

	void Shoting (){

		// Atualiza a animaçao
		anime.SetBool ("shot", (shotting || flaming));

		shotposition = transform.position;
		
		if (transform.eulerAngles.y == 0)
			shotposition.x += 0.15f;
		else
			shotposition.x -= 0.15f;
		
		// Dispara o tiro
		if (Input.GetKeyDown ("left ctrl") && !flaming) {
			
			shotting = true;

		}
		if (Input.GetKeyUp("left ctrl") && shotting) {
			
			if (shotTime < 1){
				
				Instantiate(shotgun[1], shotposition, transform.rotation);
			}
			else {
				
				Instantiate(shotgun[2], shotposition, transform.rotation);
			}
			
			shotTime = 0;
			shotting = false;
		}

		// Dispara o sabre de fogo
		if (Input.GetKeyDown ("left alt") && !shotting) {
			
			flaming = true;

			sabre = (GameObject) Instantiate(shotgun[0], shotposition, transform.rotation);

		}
		if (Input.GetKeyUp("left alt") && flaming) {
			
			//sabre.; 

			flaming = false;
		}

		if (shotting == true)
			shotTime += Time.deltaTime;

	}
}
