using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootPower : MonoBehaviour
{
    MovementMain movementMain;
    //esto se crea para indicar cual de las habilidades esta activa
    public bool[] useSkill = new bool[2];
    public int activeSkill;
    GameObject bootTwo;
    public int forceDashBoot = 1;
    public bool couldMine = true;

    void Start()
    {

        movementMain = FindObjectOfType<MovementMain>();
        
        for(int i = 0; i < useSkill.Length; i++)
        {
            //se ponen todas la habilidades de las botas en verdadero
            useSkill[i] = true;
        }

        

    }

    void Update()
    {
        //verificacion de la habilidad que se esta usando (si no existe vuelve al principio)
        if(activeSkill < 0 || activeSkill >= 2)
        {
            activeSkill = 0;
        }

        //cambio de habilidad
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            activeSkill += 1;
        }
        

        
        //seleccion de habilidades
        if (activeSkill == 0 && useSkill[0] == true)
        {    //habilidad de botar minas

            movementMain.twoJump = false;
            if (Input.GetKeyDown(KeyCode.Z) && movementMain.couldJump)
            {
                
                //dash
                movementMain.GetComponent<Rigidbody>().AddRelativeForce(0, 0, movementMain.forceDash * forceDashBoot);
                //se crea la mina
                //verifica si este objeto puede crear una mina
                if (couldMine == true)
                {
                    StartCoroutine(WaitTime(0.125f, () => CreateMine(couldMine)));
                }

                useSkill[0] = false;
                StartCoroutine(WaitTime(1f, ()=> useSkill[0] = true)); //se espera un tiempo para poder crear la mina otra vez
                activeSkill = 0;
            }
        }
        if (movementMain.energyPlayer > 0)
        {
            if (activeSkill == 1)
            {   //habilidad de saltar mas alto

                movementMain.twoJump = true;
                activeSkill = 1;

            }
        }
        



    }

    public void CreateMine(bool couldMine)// creacion de las minas
    {
        //verifica si este objeto puede crear una mina
        GameObject mine;
        mine = Instantiate((GameObject)Resources.Load("Mine"), this.transform.position, Quaternion.identity);
        mine.GetComponent<SphereCollider>().enabled = false;
        StartCoroutine(WaitTime(0.2f, () => mine.GetComponent<SphereCollider>().enabled = true));
        Debug.Log("mina creada");
    }

    IEnumerator WaitTime(float tiempo, Action accion)
    {
        yield return new WaitForSecondsRealtime(tiempo);
        accion();
    }
}