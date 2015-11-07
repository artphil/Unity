using UnityEngine;
using System.Collections;

public class enimie : MonoBehaviour
{

    // Movimento
    public float velocity;              // Velocidade do inimigo
    public bool forRigth = true;        // Indica se direção é a direita
    public float runDelay;              // Tempo que caminha prara cada direção
    private float runningTime;          // Tempo de caminhada
    private bool running;               // Indica se está andando

    //Animação
    public Transform character;          //Personagem jogavel
    private Animator anime;              //Animação do personagem

	public int life;

    // Use this for initialization
    void Start()
    {
        // Carrega a animação
        anime = character.GetComponent<Animator>();
        runningTime = 0;
        running = false;
		life = 10;

    }

    // Update is called once per frame
    void Update()
    {

        runningTime += Time.deltaTime;
        if (runningTime > runDelay)
        {
            runningTime = 0;
            if (running)
            {               
                anime.SetTrigger("stop");
                running = false;
            }
            else {

                forRigth = !forRigth;
                anime.SetTrigger("runing");
                running = true;
            }
        }

        if (forRigth && running)  {

            transform.Translate(Vector2.right * velocity * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (!forRigth && running)  {

            transform.Translate(Vector2.right * velocity * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
        }

		if (life <= 0) {

			Destroy (gameObject);
		}

    }

	void OnCollisionEnter2D (Collision2D colisor) {
		
		if (colisor.gameObject.tag == "Power1") {
			
			life -= 1;
		}
		else if (colisor.gameObject.tag == "Power2") {
			
			life -= 5;
		}
	}
}
