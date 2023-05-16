using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Animation : MonoBehaviour
{
    Animator animatorEnemy;
    public Enemy2 enemy;
    public Vector3 Destino;

    // Start is called before the first frame update
    void Start()
    {
        animatorEnemy = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Destino != enemy.destination)
        {
            animatorEnemy.SetBool("Running", true);
        }
        else
        {
            animatorEnemy.SetBool("Running", false);
        }

        if (enemy.playerInAttackRange == true)
        {
            animatorEnemy.SetBool("Attacking", true);
        }
        else
        {
            animatorEnemy.SetBool("Attacking", false);
        }
    }
}