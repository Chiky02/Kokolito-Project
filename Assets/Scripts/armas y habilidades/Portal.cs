using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Portal : MonoBehaviour
{
    public GameObject player;
    //este es el otro portal
    public GameObject otherPortal;
    //este es el punto de la base del portal
    public GameObject portalCamera;
    RenderTexture tempTexture;
    public Transform playerCamera;
    private bool objectInPortal;
    Vector3 objectFromPortal;
    float dotProduct;
    private void Awake()
    {
        tempTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
    }
    void Start()
    {
        player = GameObject.Find("Player");
        playerCamera = GameObject.Find("MainCamera").transform;
        
        //se asigna el tipo de portal
        if (GameObject.Find("PortalIn"))
        {
            //el portal es de salida
            GetComponent<SpriteRenderer>().color = new Color(0.82f, 0.13f, 1);
            GetComponent<ParticleSystem>().startColor = GetComponent<SpriteRenderer>().color;
            name = "PortalOut";
            //viewMaterial = 
        }
        else
        {
            //el portal es de entrada
            GetComponent<SpriteRenderer>().color = new Color(0.23f, 0.77f, 1);
            GetComponent<ParticleSystem>().startColor = GetComponent<SpriteRenderer>().color;
            name = "PortalIn";
        }
        

    }

    void Update()
    {

        try{ 
        //se asigna cual es el portal opuesto
        if (name == "PortalIn")
        {
            //el portal actual es el de entrada y el otro portal es de salida
            otherPortal = GameObject.Find("PortalOut");
        }
        else
        {
            //el portal actual es de salida y el otro portal es de entrada
            otherPortal = GameObject.Find("PortalIn");

           
            
        }

        //se toma la camara del portal opuesto
        portalCamera = otherPortal.transform.GetChild(2).gameObject;
        //se asigna dentro del material un render texture
        transform.GetChild(1).GetComponent<Renderer>().material.mainTexture = tempTexture;
        //este mismo render texture se pone en el objetivo de la camara del portal opuesto
        otherPortal.transform.GetChild(2).GetComponent<Camera>().targetTexture = tempTexture;

        //se calcula la posicion de la camara con respecto al portal actual
        Vector3 cameraFromPortal = transform.InverseTransformPoint(playerCamera.transform.position);


        //se mueve la camara del portal opuesto
        otherPortal.transform.GetChild(2).localPosition = new Vector3(-cameraFromPortal.x, cameraFromPortal.y, -cameraFromPortal.z);

        //se rota guarda la rotacion de la camara
        Quaternion rotationCamera = Quaternion.Inverse(transform.rotation) * playerCamera.transform.rotation;
        // se aplica la rotacion de la camara 
        otherPortal.transform.GetChild(2).localEulerAngles = new Vector3(rotationCamera.eulerAngles.x, rotationCamera.eulerAngles.y + 180, rotationCamera.eulerAngles.z);

        if (objectInPortal == true)
        {
            //la distancia entre el objeto y el portal
        }
        }
        catch(Exception e)
        {
            Debug.Log("Error 13");


        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Enemy")
        {
            objectFromPortal = transform.GetChild(0).InverseTransformPoint(player.transform.position);

            //dotProduct = Vector3.Dot(transform.up, objectFromPortal);

            objectInPortal = true;
            if (objectInPortal == true)
            {
                //hay que cambiar de euler a quaternion
                Debug.Log(objectFromPortal + " (antes)");

                Vector3 rotationObject = other.transform.rotation.eulerAngles - transform.rotation.eulerAngles;

                //el *0.15f es la escala del portal
                other.transform.position = new Vector3(otherPortal.transform.position.x + objectFromPortal.x * 0.15f, otherPortal.transform.GetChild(0).position.y + objectFromPortal.y * 0.15f, otherPortal.transform.position.z);
                other.transform.eulerAngles = new Vector3(0, otherPortal.transform.rotation.eulerAngles.y, 0);
                other.transform.Translate(new Vector3(0, 0, -0.8f));
                other.transform.eulerAngles += new Vector3(rotationObject.x, rotationObject.y + 180, rotationObject.z);
                //if(other.GetComponent<Rigidbody>());

                //rotationObject.y += 180;
                //other.transform.rotation = new Quaternion(0, otherPortal.transform.rotation.y, 0, 0);





                Debug.Log(objectFromPortal + " (despues)");
            }
            //se hace el producto punto
            objectInPortal = false;

            //se verifica si el objeto esta al frente el portal
            if (dotProduct > 0)
            {
            }
        }
        

    }

}
