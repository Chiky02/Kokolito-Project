using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementMain: MonoBehaviour
{
    Functions f= new Functions();
    //aquí está el movimeinto básico del pesonaje
    public int moneyPlayer = 0;
    public int energyPlayer = 0;
    public int lifePlayer = 5;//variable privada para evitar hackeo 
    public GameObject wheelSkill;   
    public GameObject cameraMain;

    public float velocityMovement;//variable privada o protected
    public float rotationVelocity;
    public float jumpVelocity;

    public Vector3 move;
    public Vector3 gravity;

    //public float velocityCameraY;
    public float gravityValue = 1.8f;
    float gravityVelocity;
    [NonSerialized] private float gravityMultiplier = 0.81f;
    public float velocityCameraX;
   public int forceDash = 300;//variable usada en otro lugar
    public bool couldJump = true;
    public bool twoJump = false;
    public bool focusCamera = false;
    public int weaponOption = 1; // arma actual
    //float timeActual = 0;
    //int timeSeconds;
    //public int numberEnemy = 4;
    public float numberEnemy=4;//variable usada en otro lugar
    CharacterController controller;
    Animator animator;

    //objeto que bloquea el paso
    public GameObject block;

    //habilidades
    // public int opcionBotas = 0;

    void Start()
    {
        animator = GetComponent<Animator>();//obtencion del arbol de animaciones
        controller = GetComponent<CharacterController>();
        
    }
    void Update()
    {
       

        controller.transform.Rotate(transform.up * Input.GetAxis("HorizontalR") * rotationVelocity * Time.deltaTime);
        //movimiento hacia adelante     
        //transform.Translate(0, 0, f.moverY(animator, velocityMovement));


        /*    gravityForce.y -= gravityValue * Time.deltaTime;*/
        //apliacando gravedad

        if (Input.GetButtonDown("A"))
        {
            f.Jump(animator, couldJump, twoJump);
            gravityVelocity = jumpVelocity;
           // controller.Move(move * velocityMovement * Time.deltaTime);
        }
        else
        { }
        applyGravity();
            applyMovement();


        /*  controller.Move(gravityForce * Time.deltaTime);*/


        //animator.SetBool("caminar", false);
        // couldJump = f.ableJump(animator);

        //couldJump = f.enableJump(animator);
        //gravityForce.y -= gravityValue * Time.deltaTime;

        //controller.Move(gravityForce * Time.deltaTime);

        //rotacion de personaje
        //var targetAngle = MathF.Atan2(move.x, move.y) * Mathf.Rad2Deg;

       // transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        // 
        //Debug.Log(Input.GetAxis("HorizontalR"));


        //habilidad de saltar

        //this conditional is used to determinate de jump type

        //modos de movimiento de la camara
        //With this functión we can pin up the camera un a place

        f.focusCamera(focusCamera, cameraMain, velocityCameraX, transform);
        //aqui se oculta o no la rueda de actividades
        //With this code we can hide or show the activities wheel
        f.actvitiesWheel(cameraMain, wheelSkill);
        if (Input.GetButton("correr"))
        {
            //llamado a la funcion de correr
            f.run(animator, velocityMovement);

            move = transform.forward * velocityMovement * 4;
            controller.Move(move * Time.deltaTime);

            //CONTADOR DE TIEMPO JUGANDO...
        }
        else
        {
            animator.SetBool("correr", false);//esto se corregirá luego

        }
        //apuntar
        //cosas que tal vez no vayan aqúí pero se ponen igual :v


        //Destruir bloqueo
        if (!GameObject.Find("enemigo"))
        {
            Destroy(block);
        }
        /*  timeActual += Time.deltaTime;
          timeSeconds = (int)timeActual;*/
        //Hacer una funcion counter time
       
        if (Input.GetButtonDown("B"))
        {            //llamado a la funcion de correr            
            f.Down(animator);            //CONTADOR DE TIEMPO JUGANDO...        
        }

        if (Input.GetAxis("Lt") >0)
        {            //llamado a la funcion de correr           
            f.Aim(animator);            //CONTADOR DE TIEMPO JUGANDO...        
        }
        else
        {
            
        }
        //funcion para mover personaje hacia adelante y atrás          
        // se verifica si está tocando el piso
         }

    private float applyGravity()
    {
        if (controller.isGrounded && gravityVelocity <0f)
        {
            gravityVelocity = -1f;
            animator.SetBool("saltar", false);
        }
        else
        {
            gravityVelocity -= (gravityValue) * gravityMultiplier*Time.deltaTime;
        }
        return gravityVelocity;


    }
    private void applyMovement()
    {
        move  = transform.forward * velocityMovement *f.moverY(animator,velocityMovement);
        gravity = new Vector3(0f, applyGravity(), Input.GetAxis("Horizontal"));
        // move = transform.forward * f.moverY(animator, velocityMovement);

        if (move.x == 0)
        {
            animator.SetBool("caminar", false);

        }
        else
        {

            

        }
        controller.Move(move * velocityMovement *Time.deltaTime);
        controller.Move(gravity * Time.deltaTime);
    }
    private void OnCollisionStay(Collision collision)
    {
        couldJump = f.ableJump(animator);

    }
    private void OnCollisionExit(Collision other)
    {
        couldJump = f.enableJump(animator);
    }


    private void OnTriggerEnter(Collider other)
    {
        //obtiene monedas
        if (other.tag == "item")
        {
            Debug.Log("SIRVE");
            Destroy(other.gameObject);
            moneyPlayer++;
            if (moneyPlayer == 1)
            {

                StartCoroutine(WaitTime(3, () => SceneManager.LoadScene("SampleScene 2")));
            }
            GetComponent<AudioSource>().Play();
        }

        //obtiene kokolito
        if (other.tag == "item2")
        {

            Destroy(other.gameObject);
            energyPlayer++;

        }


    }
   
  
   

    //Clase cortinas
    IEnumerator WaitTime(float tiempo, Action accion)
    {
        yield return new WaitForSecondsRealtime(tiempo);
        accion();
    }
}
