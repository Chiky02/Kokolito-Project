using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions 
{
    bool downn = false;
    Animator_Define anim=new Animator_Define();
    //Forward an back movement

    bool aimming =false;
   public float moverY(Animator animator, float velocityMovement)
    {
        float f = Input.GetAxis("Vertical");//deteccion de la palanca vertical

        if (f > 0)//para mover hacia adelante flecha arriba
        {

            anim.moveY(animator);
            return f * velocityMovement;
        }
        else if (f < 0)//para mover hacia atr�s flecha abajo
        {
            anim.moveY(animator);
            return (f * velocityMovement);
        }


        else
        {            
            return 0;
        }



    }

    public void Aim(Animator animator)
    {
        if (aimming)
        {
            aimming = false;
            anim.NoAimxd(animator);

        }

        else { anim.Aimxd(animator); aimming = true; }

    }
    public void Down(Animator animator)
    {
        if (downn)
        {
            downn = false;
            anim.NoDonwxd(animator);

        }

        else { anim.Downxd(animator); downn = true; }

    }

    public void Jump(Animator animator,bool couldJump, bool twoJump)
    {

        enableJump( animator);


            if (couldJump)
            {
                if (twoJump)
                {
                    //aqui hay doble salto
                
                  

                }
                else
                {
                    //aqui solo hay uno
                  
                }
            }
        
    }

    public void focusCamera(bool focusCamera, GameObject cameraMain, float velocityCameraX, Transform transform)
    {

        if (focusCamera == true)
        {
            //camara para apuntar

            //cameraMain.transform.parent.parent.Rotate(0, Input.GetAxis("Mouse X") * velocityCameraX, 0);

        }
        else
        {
            //camara normal
//cameraMain.transform.parent.Rotate(0, Input.GetAxis("Mouse X") * velocityCameraX, 0);

            if (Input.GetKey(KeyCode.N))
            {
                Debug.Log("Funciona   " + cameraMain.transform.parent.rotation.eulerAngles.y);
                float xd = cameraMain.transform.parent.rotation.eulerAngles.y;


               transform.Rotate(0, cameraMain.transform.parent.rotation.eulerAngles.y, 0);
                cameraMain.transform.parent.parent.Rotate(0, 0, 0);


            }
        }


    }

   public void actvitiesWheel(GameObject cameraMain, GameObject wheelSkill)



    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {

            //se activa la rueda de habilidades
            wheelSkill.SetActive(true);
            cameraMain.transform.parent.localPosition += new Vector3(0, 0, 0.9f);

        }
        else if (Input.GetKeyUp(KeyCode.Joystick1Button5))
        {
            //se desactiva la rueda de habilidades
            wheelSkill.SetActive(false);
            cameraMain.transform.parent.localPosition -= new Vector3(0, 0, 0.9f);
        }

    }

    public void run(Animator animator, float velocityMovement) {
   
            anim.run(animator);
            

      

    }
    public bool ableJump( Animator animator) {

        anim.jumpping(animator);
        return true;
    
    }

    public bool enableJump( Animator animator)
    {

        anim.notJumping(animator);
        return false;

    }

}
