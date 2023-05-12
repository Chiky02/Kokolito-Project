using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootLogic : MonoBehaviour
{
     MovementMain AccionesPrincipal=new MovementMain(); 
   
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        AccionesPrincipal.couldJump = true;
    }
    private void OnCollisionExit(Collision other)
    {
        AccionesPrincipal.couldJump = false;
    }
}
