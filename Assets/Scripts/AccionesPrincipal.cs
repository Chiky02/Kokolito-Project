using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionesPrincipal : MonoBehaviour
{


    /*
     Es importante tener en cuenta que:
    *Este codigo no esta ordenado de la mejor manera
    *falta optimización 
    *algunas funciones no se utilizand ya que son de la parte de habilidades de Nicolas
    *acá se resalta el codigo para animación y movimientos básicos necesario
     */

    public int vidaJugador = 5;
  

    public GameObject camara;
    public float velocidadMovimientoPrincipal;
    public float velocidadCamaraY;
    public float velocidadCamaraX=5f;
    public int fuerzaSalto = 300;
    public int fuerzaDash = 300;
    public bool poderSaltar = true;

    public bool dobleSalto = false;
    public bool enfoqueCamara = false;

    int tiempoSegundos;
    public int numeroEnemigos = 4;
    Quaternion rotacionCamaraInicial;

    Animator animator;


    //habilidades
    public int opcionBotas = 0;


    void Start()
    {

    }

    void Update() { 

        animator = GetComponent<Animator>();//obtencion del arbol de animaciones
        

        //activaci�n de las habilidades de salto
    
        //movimiento hacia adelante
        transform.Translate(0, 0, moverY());

        //rotacion de personaje
        transform.Rotate(0, Input.GetAxisRaw("Horizontal")*3f, 0);




        //funcion para mover personaje hacia adelante y atrás
        float moverY() {
            float f = Input.GetAxis("Vertical");//detección de la palanca vertical
        

            if (f>0)//para mover hacia adelante flecha arriba
            {
                animator.SetBool("caminar", true);
                animator.SetBool("Reposo", false);
                return f * velocidadMovimientoPrincipal;
            }
            else if (f< 0)//para mover hacia atrás flecha abajo
            {
                animator.SetBool("caminar", true);
                animator.SetBool("Reposo", false);
                return (f * velocidadMovimientoPrincipal);
            }


            else
            {
                animator.SetBool("caminar", false);
                return 0;


            }

        }//para correr hacia adelante 
        if (Input.GetKey(KeyCode.E))
        {
            animator.SetBool("correr", true);
            animator.SetBool("Reposo", false);
            transform.Translate(0,0,2* velocidadMovimientoPrincipal);
        
        }
        else
        {
            animator.SetBool("correr", false);
           

        }

            //modos de movimiento de la camara
            if (enfoqueCamara == true)
        {
            //camara para apuntar

            camara.transform.parent.parent.Rotate(0, Input.GetAxis("Mouse X") * velocidadCamaraX, 0);
        }
        else
        {
            //camara normal
            camara.transform.parent.Rotate(0, Input.GetAxis("Mouse X") * velocidadCamaraX, 0);
        }
        

        //saltar


        if (poderSaltar)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
             
                if (dobleSalto)
                {
                    GetComponent<Rigidbody>().AddForce(0, fuerzaSalto * 2, 0);
                  
                }
                else
                {
                    GetComponent<Rigidbody>().AddForce(0, fuerzaSalto, 0);
                 
                }
            }
        }



        

    }


    //configuracion de saltar
    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        poderSaltar = true;
        animator.SetBool("Reposo", true);
        animator.SetBool("saltar", false);

    }
    private void OnCollisionExit(Collision other)
    {
        poderSaltar = false;
        animator.SetBool("Reposo", false);
        animator.SetBool("saltar", true);
   
    }


  

}
