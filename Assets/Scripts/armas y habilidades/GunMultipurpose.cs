using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GunMultipurpose : MonoBehaviour
{
    public int activeSkill = 0;
    public int forceBullet = 50;
    GameObject bulletActive;
    GameObject player;
    MovementMain playerCode;
    Quaternion portalPosition;
    int portalTime = 1;
    bool disp = true;
    void Start()
    {
        player = GameObject.Find("Player");
        
    }

    void Update()
    {
        //se cambia al modo de apuntar (de momento no se usa)


        //se cambia la habilidad actual
            
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            activeSkill += 1;
            //se verifica si el numero de el habilidad indicado es mayor al numero de habilidades, si lo es se reinicia el contador
            if (activeSkill >= 2)
            {
                activeSkill = 0;
            }

        }

       
        //se activa el arma
        if (Input.GetAxisRaw("Rt")>0 && disp==true)//con esto se dispara el arma para el que lo lea
        {
           
                //se selecciona la habilidad activa
                switch (activeSkill)
                {
                    //se disparan balas normales
                    case 0:

                        GameObject activeBullet = Instantiate((GameObject)Resources.Load("BulletNormal"), transform.position - transform.right * 0.5f, Quaternion.identity);
                        activeBullet.GetComponent<Rigidbody>().AddRelativeForce(-transform.right * forceBullet, ForceMode.Force);
                        Destroy(activeBullet, 15);

                        break;
                    //se crean portales con las balas
                    case 1:
                        //se verifica si existe una bala de portal, si existe no se puede activar el arma
                        if (!GameObject.Find("BulletPortal(Clone)"))
                        {
                            //comprobacion para que solo hayan dos portales a la vez
                            if (GameObject.Find("PortalOut") && GameObject.Find("PortalIn"))
                            {
                                GameObject.Destroy(GameObject.Find("PortalIn"));
                                GameObject.Destroy(GameObject.Find("PortalOut"));
                            }
                            //se crea la bala a usar
                            portalPosition = player.transform.rotation;
                            bulletActive = Instantiate((GameObject)Resources.Load("BulletPortal"), transform.position - transform.right * 0.5f, Quaternion.Euler(0, 0, 0));
                            bulletActive.GetComponent<Rigidbody>().AddRelativeForce(-transform.right * forceBullet / 8);
                            StartCoroutine(WaitTime(1.5f * portalTime, CreatePortal));
                        }
                        else
                        {
                            CreatePortal();
                        }



                        break;
                
                
            }
            disp = false;

        }
        else if(Input.GetAxisRaw("Rt") ==0) {disp = true; }
    }

    void CreatePortal()
    {
      

            GameObject portal = Instantiate((GameObject)Resources.Load("Portal"), bulletActive.transform.position, portalPosition);
        Debug.Log(bulletActive.transform.rotation.normalized);
        Destroy(bulletActive);
        
        
    }
    IEnumerator WaitTime(float tiempo, Action accion)
    {
        yield return new WaitForSecondsRealtime(tiempo);
        accion();
    }



}

