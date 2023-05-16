using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cofre : MonoBehaviour
{
    int contador = 1;
    public GameObject objeto;
    public GameObject objeto2;
    SphereCollider este;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("Hay contacto"+ collision.transform.tag);

        if (collision.transform.tag == "Player" )
        {
            print("Hay contacto");

            objeto.transform.Rotate(-90, 0, 0);
            
            contador++;

            

        }

    
    }

}
