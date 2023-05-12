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
    public Vector3 gravityForce;
  
    //public float velocityCameraY;
    float gravityValue = 9.8f;

    public float velocityCameraX;
    public int forceJump = 100;
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
        //movimiento hacia adelante     
        //transform.Translate(0, 0, f.moverY(animator, velocityMovement));

        float mo = Input.GetAxis("Vertical");
        gravityForce.y -= gravityValue * Time.deltaTime;
        if (mo != 0) {
            move = transform.forward * f.moverY(animator, velocityMovement);
            controller.Move(move * Time.deltaTime);
            gravityForce.y -= gravityValue * Time.deltaTime;
            controller.Move(gravityForce * Time.deltaTime);
        }
        else if (controller.isGrounded)
        {

            animator.SetBool("caminar", false);
            couldJump = f.ableJump(animator);

        

        }
        else
        {
        
            couldJump = f.enableJump(animator);
            gravityForce.y -= gravityValue * Time.deltaTime;

            controller.Move(gravityForce * Time.deltaTime);

        }
      
        //rotacion de personaje
        controller.transform.Rotate(transform.up * Input.GetAxisRaw("Horizontal")*rotationVelocity*Time.deltaTime);

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            f.Jump(animator, forceJump, couldJump, twoJump);
            gravityForce.y += jumpVelocity ;
            controller.Move(gravityForce * Time.deltaTime);
        }



        //habilidad de saltar

        //this conditional is used to determinate de jump type







        //modos de movimiento de la camara
        //With this functión we can pin up the camera un a place

        f.focusCamera(focusCamera, cameraMain, velocityCameraX, transform);
        //aqui se oculta o no la rueda de actividades
        //With this code we can hide or show the activities wheel
        f.actvitiesWheel(cameraMain, wheelSkill);
        if (Input.GetKey(KeyCode.Q))
        {
            //llamado a la funcion de correr
            f.run(animator, velocityMovement);

            move = transform.forward *  velocityMovement*4;
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
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {            //llamado a la funcion de correr            
            f.Down(animator);            //CONTADOR DE TIEMPO JUGANDO...        
        }

        if (Input.GetKeyUp(KeyCode.E))
        {            //llamado a la funcion de correr           
            f.Aim(animator);            //CONTADOR DE TIEMPO JUGANDO...        
        }
        //funcion para mover personaje hacia adelante y atrás          
        // se verifica si está tocando el piso
      
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
        if (other.tag=="item"  )
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
