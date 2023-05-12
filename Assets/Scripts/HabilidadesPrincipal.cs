using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadesPrincipal : MonoBehaviour
{
    MovementMain movementMain;
    // Start is called before the first frame update
    void Start()
    {
        movementMain = FindObjectOfType<MovementMain>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (movementMain.couldJump)
            {
                movementMain.couldJump = false;
                //se da fuerza en el eje y
                GetComponent<Rigidbody>().AddForce(0, movementMain.forceJump, 0);
            }
            else
            {
                Debug.Log("No es posible saltar");
            }

        }
    }
}
