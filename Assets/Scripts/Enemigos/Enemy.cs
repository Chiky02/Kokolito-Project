using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public int lifeEnemy = 3;
    public int type = 1;
    MovementMain player;
    

    void Start()
    {
        player = FindObjectOfType<MovementMain>();
        type = Random.Range(1, 3); // se asigna un comportamiento aleatoriamente
        //InvokeRepeating("CambiarDireccion", 3, 3);

    }


    void Update()
    {

        transform.LookAt(player.transform); //ver hacia el jugador
        //GetComponent<Rigidbody>().velocity = transform.forward; //moverse hacia aadelante



    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "jugador")
        {
            player.lifePlayer--;
            if (player.lifePlayer == 0)
            {
                SceneManager.LoadScene(0);
            }

        }
        if (collision.collider.tag == "Bala")
        {
            Destroy(collision.collider.gameObject); //objeto con el que se colisiona
            lifeEnemy -= 1;
            if(lifeEnemy == 0)
            {
                Destroy(gameObject);
                player.numberEnemy -= 1;
                if(player.numberEnemy == 0)
                {
                    SceneManager.LoadScene(0);
                }
            }

        }
    }
}
